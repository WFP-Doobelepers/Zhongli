using System;

namespace Zhongli.Data.Models.Moderation.Infractions.Actions;

public abstract class ReprimandAction
{
    public Guid Id { get; set; }

    public override string ToString() => ((IAction) this).Action;
}