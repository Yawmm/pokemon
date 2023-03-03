using Game.Battles.Events;
using Game.Battles.Input;
using Game.Inputs.Prompts;

namespace Game.Battles.Actions;

/// <summary>
/// An <see cref="IAction"/> which executes the help command.
/// </summary>
public class HelpAction : IAction
{
    /// <inheritdoc cref="IAction.Execute"/>
    public IEnumerable<Event>? Execute(Battle battle)
    {
        HelpPrompts.GetCommandHelp<BattleCommand>();
        return null;
    }
}