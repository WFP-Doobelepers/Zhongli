﻿using System.Collections.Generic;
using System.Linq;
using Discord.Commands;

namespace Zhongli.Services.CommandHelp;

public class CommandHelpData
{
    public IReadOnlyCollection<ParameterHelpData> Parameters { get; set; }

    public IReadOnlyCollection<string> Aliases { get; set; }

    public string Name { get; set; }

    public string? Summary { get; set; }

    public static CommandHelpData FromCommandInfo(CommandInfo command)
    {
        var ret = new CommandHelpData
        {
            Name    = command.Name,
            Summary = command.Summary,
            Aliases = command.Aliases,
            Parameters = command.Parameters
                .Select(ParameterHelpData.FromParameterInfo)
                .ToArray()
        };

        return ret;
    }
}