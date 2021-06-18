﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Discord;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhongli.Data.Models.Moderation.Reprimands;

namespace Zhongli.Data.Models.Discord
{
    public class GuildUserEntity
    {
        protected GuildUserEntity() { }

        public GuildUserEntity(IGuildUser user)
        {
            Id = user.Id;

            CreatedAt = user.CreatedAt;
            JoinedAt  = user.JoinedAt;

            Username = user.Username;
            Nickname = user.Nickname;

            DiscriminatorValue = user.DiscriminatorValue;

            GuildId = user.GuildId;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ulong Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? JoinedAt { get; set; }

        public virtual GuildEntity Guild { get; set; }

        public virtual ICollection<Ban> BanHistory { get; init; } = new List<Ban>();

        public virtual ICollection<Kick> KickHistory { get; init; } = new List<Kick>();

        public virtual ICollection<Mute> MuteHistory { get; init; } = new List<Mute>();

        public virtual ICollection<Warning> WarningHistory { get; init; } = new List<Warning>();

        public int WarningCount { get; set; }

        public string Username { get; set; }

        public string? Nickname { get; set; }

        public ulong GuildId { get; set; }

        public ushort DiscriminatorValue { get; set; }
    }
}