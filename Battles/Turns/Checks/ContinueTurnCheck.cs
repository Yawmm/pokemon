using Game.Companions;
using Game.Trainers;
using Spectre.Console;

namespace Game.Battles.Turns.Checks;

/// <summary>
/// Check if an <see cref="ITurn"/> previously executed by a <see cref="Pokemon"/> should be continued
/// to its next stage.
/// </summary>
public class ContinueTurnCheck : ITurnCheck
{
    /// <inheritdoc cref="ITurnCheck.Execute"/>
    public ITurn? Execute(Team team, Battle battle)
    {
        // Check if any previous actions exist
        if (battle.History.Rounds.Count <= 0)
            return null;

        // Check if a previous action exists
        if (!battle.History.Rounds[^1].Actions.TryGetValue(team, out var move))
            return null;

        // Check if the turn is still ongoing
        if (move.Turn is not MoveTurn { Finished: false } moveTurn) 
            return null;

        // Check if the actor is still alive
        if (moveTurn.Actor.Whiteout)
            return null;
        
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[{Colors.Trainer}]{team.Owner.Name}[/] must continue the last [{Colors.Move}]move[/]!");
        return moveTurn;
    }
}