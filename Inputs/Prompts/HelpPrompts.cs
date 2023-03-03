using Game.Inputs.Extensions;
using Spectre.Console;

namespace Game.Inputs.Prompts;

/// <summary>
/// A prompt class used to inform the player about critical game information.
/// </summary>
public static class HelpPrompts
{
    /// <summary>
    /// Log the available <see cref="Command"/> that the player can execute.
    /// </summary>
    /// <typeparam name="T">The <see cref="Command"/> enum.</typeparam>
    public static void GetCommandHelp<T>()
        where T : struct, Enum
    {
        AnsiConsole.MarkupLine($"The following [{Colors.Command}]commands[/] are available:");
        foreach (var cmd in Enum.GetValues<T>())
            AnsiConsole.MarkupLine($"{cmd.ToString().ToLower()} - [silver]{cmd.Description()}[/]");
    }
}