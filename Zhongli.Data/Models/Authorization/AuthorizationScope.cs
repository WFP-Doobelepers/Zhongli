﻿using System;
using System.ComponentModel;

namespace Zhongli.Data.Models.Authorization;

[Flags]
public enum AuthorizationScope
{
    None = 0,

    [Description("All permissions. Dangerous!")]
    All = 1 << 0,
    Warning = 1 << 1,
    Mute = 1 << 2,
    Kick = 1 << 3,
    Ban = 1 << 4,

    [Description("Allows configuration of settings.")]
    Configuration = 1 << 5,

    [Description("Allows using the quote feature.")]
    Quote = 1 << 6,

    [Description("Allows warning, mute, kick, and ban.")]
    Moderator = Warning | Mute | Kick | Ban,

    [Description("Allows warning and mute.")]
    Helper = Warning | Mute
}