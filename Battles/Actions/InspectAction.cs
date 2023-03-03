using Game.Battles.Events;
using Game.Companions;
using Game.Inputs.Prompts;
using Game.Trainers;
using Spectre.Console;

namespace Game.Battles.Actions;

/// <summary>
/// An <see cref="IAction"/> which executes the inspect command which inspect a <see cref="Pokemon"/>
/// of a <see cref="Trainer"/> or <see cref="Team"/>.
/// </summary>
public class InspectAction : IAction
{
    /// <inheritdoc cref="IAction.Execute"/>
    public IEnumerable<Event>? Execute(Battle battle)
    {
        var inspected = AnsiConsole.Prompt(
            new SelectionPrompt<Team>()
                .Title($"Which [{Colors.Trainer}]trainer[/]'s team would you like to inspect?")
                .AddChoices(battle.Player, battle.Opponent)
        );
                    
        TeamPrompts.GetTeam(inspected.Owner.Name, inspected.Members);
        return null;
    }
}