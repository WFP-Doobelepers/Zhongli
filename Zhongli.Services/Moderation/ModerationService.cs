using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.WebSocket;
using Hangfire;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Zhongli.Data;
using Zhongli.Data.Models.Moderation.Infractions;
using Zhongli.Data.Models.Moderation.Infractions.Reprimands;
using Zhongli.Services.Utilities;

namespace Zhongli.Services.Moderation
{
    public class ModerationService
    {
        private readonly DiscordSocketClient _client;
        private readonly ZhongliContext _db;
        private readonly ModerationLoggingService _logging;
        private readonly IServiceScopeFactory _scope;

        public ModerationService(DiscordSocketClient client, ZhongliContext db,
            ModerationLoggingService logging,
            IServiceScopeFactory scope)
        {
            _client = client;
            _db     = db;

            _logging = logging;
            _scope   = scope;
        }

        public static async Task ConfigureMuteRoleAsync(IGuild guild, IRole? role)
        {
            role ??= guild.Roles.FirstOrDefault(r => r.Name == "Muted");
            role ??= await guild.CreateRoleAsync("Muted", isMentionable: false);

            var permissions = new OverwritePermissions(
                addReactions: PermValue.Deny,
                sendMessages: PermValue.Deny,
                speak: PermValue.Deny,
                stream: PermValue.Deny);

            foreach (var channel in await guild.GetChannelsAsync())
            {
                await channel.AddPermissionOverwriteAsync(role, permissions);
            }
        }

        private async Task ExpireMuteAsync(Mute mute, CancellationToken cancellationToken)
        {
            var guildEntity = await _db.Guilds.FindByIdAsync(mute.GuildId, cancellationToken);
            var user = _client.GetGuild(mute.GuildId).GetUser(mute.UserId);

            if (guildEntity?.MuteRoleId is not null)
                await user.RemoveRoleAsync(guildEntity.MuteRoleId.Value);

            if (user.VoiceChannel is not null)
                await user.ModifyAsync(u => u.Mute = false);

            await ExpireReprimandAsync(mute, cancellationToken);
        }

        private async Task ExpireBanAsync(Ban ban, CancellationToken cancellationToken)
        {
            var guild = _client.GetGuild(ban.GuildId);
            var user = guild.GetUser(ban.UserId);

            await guild.RemoveBanAsync(user);
            if (user.VoiceChannel is not null)
                await user.ModifyAsync(u => u.Mute = false);

            await ExpireReprimandAsync(ban, cancellationToken);
        }

        public async Task ExpireReprimandAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var reprimand = await _db.Set<ReprimandAction>().AsAsyncEnumerable().OfType<IExpire>()
                .FirstAsync(e => e.Id == id, cancellationToken);

            switch (reprimand)
            {
                case Mute mute:
                    await ExpireMuteAsync(mute, cancellationToken);
                    break;
                case Ban ban:
                    await ExpireBanAsync(ban, cancellationToken);
                    break;
            }
        }

        public async Task<Mute?> TryMuteAsync(TimeSpan? length, ReprimandDetails details,
            CancellationToken cancellationToken = default)
        {
            var activeMute = await _db.MuteHistory
                .FirstOrDefaultAsync(m => m.IsActive()
                        && m.UserId == details.User.Id
                        && m.GuildId == details.User.Guild.Id,
                    cancellationToken);

            var user = details.User;
            var guildEntity = await _db.Guilds.FindByIdAsync(user.Guild.Id, cancellationToken);

            var muteRole = guildEntity?.MuteRoleId;
            if (muteRole is null)
                return null;

            await user.AddRoleAsync(muteRole.Value);
            if (user.VoiceChannel is not null)
                await user.ModifyAsync(u => u.Mute = true);

            if (activeMute is not null)
                return null;

            var mute = _db.Add(new Mute(length, details)).Entity;
            await _db.SaveChangesAsync(cancellationToken);

            var timeLeft = mute.TimeLeft();
            if (timeLeft is not null)
            {
                BackgroundJob.Schedule(() => ExpireReprimandAsync(mute.Id, cancellationToken),
                    timeLeft!.Value);
            }

            await _logging.PublishReprimandAsync(mute, details, cancellationToken);
            return mute;
        }

        public async Task<WarningResult> WarnAsync(uint amount, ReprimandDetails details,
            CancellationToken cancellationToken = default)
        {
            var warning = new Warning(amount, details);

            _db.Add(warning);
            await _db.SaveChangesAsync(cancellationToken);

            var request = new ReprimandRequest<Warning, WarningResult>(details, warning);
            return await PublishReprimandAsync(request, details, cancellationToken);
        }

        private async Task<T> PublishReprimandAsync<T>(IRequest<T> request, ReprimandDetails details,
            CancellationToken cancellationToken) where T : ReprimandResult
        {
            var mediator = _scope.CreateScope().ServiceProvider.GetRequiredService<IMediator>();
            var result = await mediator.Send(request, cancellationToken);
            await _logging.PublishReprimandAsync(result, details, cancellationToken);

            return result;
        }

        public async Task<NoticeResult> NoticeAsync(ReprimandDetails details,
            CancellationToken cancellationToken = default)
        {
            var notice = new Notice(details);

            _db.Add(notice);
            await _db.SaveChangesAsync(cancellationToken);

            var request = new ReprimandRequest<Notice, NoticeResult>(details, notice);
            return await PublishReprimandAsync(request, details, cancellationToken);
        }

        public async Task NoteAsync(ReprimandDetails details,
            CancellationToken cancellationToken = default)
        {
            var note = _db.Add(new Note(details)).Entity;
            await _db.SaveChangesAsync(cancellationToken);

            await _logging.PublishReprimandAsync(note, details, cancellationToken);
        }

        public async Task<Kick?> TryKickAsync(ReprimandDetails details,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var user = details.User;
                await user.KickAsync(details.Reason);

                var kick = _db.Add(new Kick(details)).Entity;
                await _db.SaveChangesAsync(cancellationToken);

                await _logging.PublishReprimandAsync(kick, details, cancellationToken);
                return kick;
            }
            catch (HttpException e)
            {
                if (e.HttpCode == HttpStatusCode.Forbidden)
                    return null;

                throw;
            }
        }

        public async Task<Ban?> TryBanAsync(uint? deleteDays, TimeSpan? length, ReprimandDetails details,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var user = details.User;
                var days = deleteDays ?? 1;

                await user.BanAsync((int) days, details.Reason);

                var ban = _db.Add(new Ban(days, length, details)).Entity;
                await _db.SaveChangesAsync(cancellationToken);

                var timeLeft = ban.TimeLeft();
                if (timeLeft is not null)
                {
                    BackgroundJob.Schedule(() => ExpireReprimandAsync(ban.Id, cancellationToken),
                        timeLeft!.Value);
                }

                await _logging.PublishReprimandAsync(ban, details, cancellationToken);
                return ban;
            }
            catch (HttpException e)
            {
                if (e.HttpCode == HttpStatusCode.Forbidden)
                    return null;

                throw;
            }
        }

        public Task HideReprimandAsync(ReprimandAction reprimand, ModifiedReprimand details,
            CancellationToken cancellationToken = default)
            => UpdateReprimandAsync(reprimand, details, ReprimandStatus.Hidden, cancellationToken);

        public async Task DeleteReprimandAsync(ReprimandAction reprimand, ModifiedReprimand details,
            CancellationToken cancellationToken = default)
        {
            _db.Remove(reprimand.Action);
            if (reprimand.ModifiedAction is not null)
                _db.Remove(reprimand.ModifiedAction);

            _db.Remove(reprimand);
            await _db.SaveChangesAsync(cancellationToken);

            await UpdateReprimandAsync(reprimand, details, ReprimandStatus.Expired, cancellationToken);
        }

        private async Task ExpireReprimandAsync<T>(T reprimand, CancellationToken cancellationToken)
            where T : ReprimandAction, IExpire
        {
            reprimand.EndedAt = DateTimeOffset.Now;

            var guild = _client.GetGuild(reprimand.GuildId);
            var user = guild.GetUser(reprimand.UserId);
            var moderator = guild.CurrentUser;
            var details = new ModifiedReprimand(user, moderator, ModerationSource.Expiry, "[Reprimand Expired]");

            await UpdateReprimandAsync(reprimand, details, ReprimandStatus.Expired, cancellationToken);
        }

        public Task UpdateReprimandAsync(ReprimandAction reprimand, ModifiedReprimand details,
            CancellationToken cancellationToken = default)
            => UpdateReprimandAsync(reprimand, details, ReprimandStatus.Updated, cancellationToken);

        private static ReprimandAction ModifyReprimand(ReprimandAction reprimand, ModifiedReprimand details,
            ReprimandStatus status)
        {
            reprimand.Status         = status;
            reprimand.ModifiedAction = new ModerationAction(details);

            return reprimand;
        }

        private async Task UpdateReprimandAsync(ReprimandAction reprimand, ModifiedReprimand details,
            ReprimandStatus status, CancellationToken cancellationToken)
        {
            _db.Update(ModifyReprimand(reprimand, details, status));
            await _db.SaveChangesAsync(cancellationToken);

            await _logging.PublishReprimandAsync(reprimand, details, cancellationToken);
        }
    }
}