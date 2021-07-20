﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Zhongli.Data;

namespace Zhongli.Data.Migrations
{
    [DbContext(typeof(ZhongliContext))]
    partial class ZhongliContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Zhongli.Data.Models.Authorization.AuthorizationGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ActionId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("GuildEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<int>("Scope")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("GuildEntityId");

                    b.ToTable("AuthorizationGroup");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Criteria.Criterion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ActionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AuthorizationGroupId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CensorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("AuthorizationGroupId");

                    b.HasIndex("CensorId");

                    b.ToTable("Criterion");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Criterion");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Discord.GuildEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal?>("MuteRoleId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.ToTable("Guilds");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Discord.GuildUserEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DiscriminatorValue")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("JoinedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nickname")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id", "GuildId");

                    b.HasIndex("GuildId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Logging.LoggingRules", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal?>("ModerationChannelId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<bool>("Verbose")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("GuildId")
                        .IsUnique();

                    b.ToTable("LoggingRules");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.AntiSpamRules", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<TimeSpan?>("DuplicateMessageTime")
                        .HasColumnType("interval");

                    b.Property<int?>("DuplicateTolerance")
                        .HasColumnType("integer");

                    b.Property<long?>("EmojiLimit")
                        .HasColumnType("bigint");

                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<long?>("MessageLimit")
                        .HasColumnType("bigint");

                    b.Property<TimeSpan?>("MessageSpamTime")
                        .HasColumnType("interval");

                    b.Property<long?>("NewlineLimit")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.ToTable("AntiSpamRules");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.AutoModerationRules", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AntiSpamRulesId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BanTriggerId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<Guid?>("KickTriggerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AntiSpamRulesId");

                    b.HasIndex("GuildId")
                        .IsUnique();

                    b.ToTable("AutoModerationRules");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Censors.Censor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ActionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AutoModerationRulesId")
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Options")
                        .HasColumnType("integer");

                    b.Property<string>("Pattern")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("AutoModerationRulesId");

                    b.ToTable("Censor");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Censor");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.ModerationAction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("ModeratorId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.HasIndex("ModeratorId", "GuildId");

                    b.ToTable("ModerationAction");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Reprimands.ReprimandAction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ActionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.Property<int>("Source")
                        .HasColumnType("integer");

                    b.Property<decimal>("UserId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("GuildId");

                    b.HasIndex("UserId", "GuildId");

                    b.ToTable("ReprimandAction");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ReprimandAction");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Triggers.NoticeTrigger", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ActionId")
                        .HasColumnType("uuid");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("AutoModerationRulesId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Retroactive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("AutoModerationRulesId");

                    b.ToTable("NoticeTrigger");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Triggers.WarningTrigger", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ActionId")
                        .HasColumnType("uuid");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("AutoModerationRulesId")
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Retroactive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("AutoModerationRulesId");

                    b.ToTable("WarningTriggers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("WarningTrigger");
                });

            modelBuilder.Entity("Zhongli.Data.Models.VoiceChat.VoiceChatLink", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("OwnerId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("TextChannelId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("VoiceChannelId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<Guid?>("VoiceChatRulesId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.HasIndex("VoiceChatRulesId");

                    b.HasIndex("OwnerId", "GuildId");

                    b.ToTable("VoiceChatLink");
                });

            modelBuilder.Entity("Zhongli.Data.Models.VoiceChat.VoiceChatRules", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("HubVoiceChannelId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<bool>("PurgeEmpty")
                        .HasColumnType("boolean");

                    b.Property<bool>("ShowJoinLeave")
                        .HasColumnType("boolean");

                    b.Property<decimal>("VoiceChannelCategoryId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("VoiceChatCategoryId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("GuildId")
                        .IsUnique();

                    b.ToTable("VoiceChatRules");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Criteria.ChannelCriterion", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Criteria.Criterion");

                    b.Property<decimal>("ChannelId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<bool>("IsCategory")
                        .HasColumnType("boolean");

                    b.HasDiscriminator().HasValue("ChannelCriterion");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Criteria.GuildCriterion", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Criteria.Criterion");

                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.HasDiscriminator().HasValue("GuildCriterion");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Criteria.PermissionCriterion", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Criteria.Criterion");

                    b.Property<int>("Permission")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("PermissionCriterion");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Criteria.RoleCriterion", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Criteria.Criterion");

                    b.Property<decimal>("RoleId")
                        .HasColumnType("numeric(20,0)");

                    b.HasDiscriminator().HasValue("RoleCriterion");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Criteria.UserCriterion", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Criteria.Criterion");

                    b.Property<decimal>("UserId")
                        .HasColumnType("numeric(20,0)");

                    b.HasDiscriminator().HasValue("UserCriterion");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Censors.BanCensor", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Moderation.Infractions.Censors.Censor");

                    b.Property<long>("DeleteDays")
                        .HasColumnType("bigint");

                    b.HasDiscriminator().HasValue("BanCensor");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Censors.KickCensor", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Moderation.Infractions.Censors.Censor");

                    b.HasDiscriminator().HasValue("KickCensor");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Censors.MuteCensor", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Moderation.Infractions.Censors.Censor");

                    b.Property<TimeSpan?>("Length")
                        .HasColumnType("interval");

                    b.HasDiscriminator().HasValue("MuteCensor");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Censors.NoteCensor", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Moderation.Infractions.Censors.Censor");

                    b.HasDiscriminator().HasValue("NoteCensor");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Censors.WarnCensor", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Moderation.Infractions.Censors.Censor");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.HasDiscriminator().HasValue("WarnCensor");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Reprimands.Ban", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Moderation.Infractions.Reprimands.ReprimandAction");

                    b.Property<long>("DeleteDays")
                        .HasColumnType("bigint");

                    b.HasDiscriminator().HasValue("Ban");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Reprimands.Kick", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Moderation.Infractions.Reprimands.ReprimandAction");

                    b.HasDiscriminator().HasValue("Kick");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Reprimands.Mute", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Moderation.Infractions.Reprimands.ReprimandAction");

                    b.Property<DateTimeOffset?>("EndedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan?>("Length")
                        .HasColumnType("interval");

                    b.Property<DateTimeOffset?>("StartedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasDiscriminator().HasValue("Mute");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Reprimands.Note", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Moderation.Infractions.Reprimands.ReprimandAction");

                    b.HasDiscriminator().HasValue("Note");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Reprimands.Notice", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Moderation.Infractions.Reprimands.ReprimandAction");

                    b.HasDiscriminator().HasValue("Notice");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Reprimands.Warning", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Moderation.Infractions.Reprimands.ReprimandAction");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.HasDiscriminator().HasValue("Warning");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Triggers.BanTrigger", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Moderation.Infractions.Triggers.WarningTrigger");

                    b.Property<long>("DeleteDays")
                        .HasColumnType("bigint");

                    b.HasDiscriminator().HasValue("BanTrigger");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Triggers.KickTrigger", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Moderation.Infractions.Triggers.WarningTrigger");

                    b.HasDiscriminator().HasValue("KickTrigger");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Triggers.MuteTrigger", b =>
                {
                    b.HasBaseType("Zhongli.Data.Models.Moderation.Infractions.Triggers.WarningTrigger");

                    b.Property<TimeSpan?>("Length")
                        .HasColumnType("interval");

                    b.HasDiscriminator().HasValue("MuteTrigger");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Authorization.AuthorizationGroup", b =>
                {
                    b.HasOne("Zhongli.Data.Models.Moderation.Infractions.ModerationAction", "Action")
                        .WithMany()
                        .HasForeignKey("ActionId");

                    b.HasOne("Zhongli.Data.Models.Discord.GuildEntity", null)
                        .WithMany("AuthorizationGroups")
                        .HasForeignKey("GuildEntityId");

                    b.Navigation("Action");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Criteria.Criterion", b =>
                {
                    b.HasOne("Zhongli.Data.Models.Moderation.Infractions.ModerationAction", "Action")
                        .WithMany()
                        .HasForeignKey("ActionId");

                    b.HasOne("Zhongli.Data.Models.Authorization.AuthorizationGroup", null)
                        .WithMany("Collection")
                        .HasForeignKey("AuthorizationGroupId");

                    b.HasOne("Zhongli.Data.Models.Moderation.Infractions.Censors.Censor", null)
                        .WithMany("Exclusions")
                        .HasForeignKey("CensorId");

                    b.Navigation("Action");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Discord.GuildUserEntity", b =>
                {
                    b.HasOne("Zhongli.Data.Models.Discord.GuildEntity", "Guild")
                        .WithMany()
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guild");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Logging.LoggingRules", b =>
                {
                    b.HasOne("Zhongli.Data.Models.Discord.GuildEntity", "Guild")
                        .WithOne("LoggingRules")
                        .HasForeignKey("Zhongli.Data.Models.Logging.LoggingRules", "GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guild");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.AntiSpamRules", b =>
                {
                    b.HasOne("Zhongli.Data.Models.Discord.GuildEntity", "Guild")
                        .WithMany()
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guild");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.AutoModerationRules", b =>
                {
                    b.HasOne("Zhongli.Data.Models.Moderation.AntiSpamRules", "AntiSpamRules")
                        .WithMany()
                        .HasForeignKey("AntiSpamRulesId");

                    b.HasOne("Zhongli.Data.Models.Discord.GuildEntity", "Guild")
                        .WithOne("AutoModerationRules")
                        .HasForeignKey("Zhongli.Data.Models.Moderation.AutoModerationRules", "GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AntiSpamRules");

                    b.Navigation("Guild");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Censors.Censor", b =>
                {
                    b.HasOne("Zhongli.Data.Models.Moderation.Infractions.ModerationAction", "Action")
                        .WithMany()
                        .HasForeignKey("ActionId");

                    b.HasOne("Zhongli.Data.Models.Moderation.AutoModerationRules", null)
                        .WithMany("Censors")
                        .HasForeignKey("AutoModerationRulesId");

                    b.Navigation("Action");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.ModerationAction", b =>
                {
                    b.HasOne("Zhongli.Data.Models.Discord.GuildEntity", "Guild")
                        .WithMany()
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Zhongli.Data.Models.Discord.GuildUserEntity", "Moderator")
                        .WithMany()
                        .HasForeignKey("ModeratorId", "GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guild");

                    b.Navigation("Moderator");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Reprimands.ReprimandAction", b =>
                {
                    b.HasOne("Zhongli.Data.Models.Moderation.Infractions.ModerationAction", "Action")
                        .WithMany()
                        .HasForeignKey("ActionId");

                    b.HasOne("Zhongli.Data.Models.Discord.GuildEntity", "Guild")
                        .WithMany("ReprimandHistory")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Zhongli.Data.Models.Discord.GuildUserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId", "GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("Guild");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Triggers.NoticeTrigger", b =>
                {
                    b.HasOne("Zhongli.Data.Models.Moderation.Infractions.ModerationAction", "Action")
                        .WithMany()
                        .HasForeignKey("ActionId");

                    b.HasOne("Zhongli.Data.Models.Moderation.AutoModerationRules", null)
                        .WithMany("NoticeTriggers")
                        .HasForeignKey("AutoModerationRulesId");

                    b.Navigation("Action");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Triggers.WarningTrigger", b =>
                {
                    b.HasOne("Zhongli.Data.Models.Moderation.Infractions.ModerationAction", "Action")
                        .WithMany()
                        .HasForeignKey("ActionId");

                    b.HasOne("Zhongli.Data.Models.Moderation.AutoModerationRules", null)
                        .WithMany("WarningTriggers")
                        .HasForeignKey("AutoModerationRulesId");

                    b.Navigation("Action");
                });

            modelBuilder.Entity("Zhongli.Data.Models.VoiceChat.VoiceChatLink", b =>
                {
                    b.HasOne("Zhongli.Data.Models.Discord.GuildEntity", "Guild")
                        .WithMany()
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Zhongli.Data.Models.VoiceChat.VoiceChatRules", null)
                        .WithMany("VoiceChats")
                        .HasForeignKey("VoiceChatRulesId");

                    b.HasOne("Zhongli.Data.Models.Discord.GuildUserEntity", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId", "GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guild");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Zhongli.Data.Models.VoiceChat.VoiceChatRules", b =>
                {
                    b.HasOne("Zhongli.Data.Models.Discord.GuildEntity", "Guild")
                        .WithOne("VoiceChatRules")
                        .HasForeignKey("Zhongli.Data.Models.VoiceChat.VoiceChatRules", "GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guild");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Authorization.AuthorizationGroup", b =>
                {
                    b.Navigation("Collection");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Discord.GuildEntity", b =>
                {
                    b.Navigation("AuthorizationGroups");

                    b.Navigation("AutoModerationRules")
                        .IsRequired();

                    b.Navigation("LoggingRules")
                        .IsRequired();

                    b.Navigation("ReprimandHistory");

                    b.Navigation("VoiceChatRules");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.AutoModerationRules", b =>
                {
                    b.Navigation("Censors");

                    b.Navigation("NoticeTriggers");

                    b.Navigation("WarningTriggers");
                });

            modelBuilder.Entity("Zhongli.Data.Models.Moderation.Infractions.Censors.Censor", b =>
                {
                    b.Navigation("Exclusions");
                });

            modelBuilder.Entity("Zhongli.Data.Models.VoiceChat.VoiceChatRules", b =>
                {
                    b.Navigation("VoiceChats");
                });
#pragma warning restore 612, 618
        }
    }
}
