using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Events;
using Game.Trainers;
using Spectre.Console;

namespace Game.Moves.Checks;

/// <summary>
/// An <see cref="IMoveCheck"/> used to check whether or not the <see cref="Pokemon"/> is dead.
/// </summary>
public class WhiteoutCheck : IMoveCheck
{
    /// <inheritdoc cref="IMoveCheck.Execute"/>
    public IEnumerable<Event>? Execute(MoveTurn turn, Pokemon actor, Pokemon opponent)
    {
        if (!actor.Whiteout) 
            return null;
        
        var replacements = GetReplacements(turn.Team);
        var replacement = turn.Team.ReplaceActor(replacements);
        
        return new[]
        {
            new ReplaceActorEvent(turn.Team, actor, replacement)
        };
    }

    /// <summary>
    /// Get a list of <see cref="Pokemon"/> that are eligible replacements for the <see cref="Team"/>. 
    /// </summary>
    /// <param name="team">The <see cref="Team"/> from which replacements should be gotten.</param>
    /// <returns>The eligible replacements for the <see cref="Team"/>.</returns>
    private static IEnumerable<Pokemon>? GetReplacements(Team team)
    {
        var availableReplacements = team.Members
            .Where(p => !p.Whiteout)
            .ToList();

        if (availableReplacements.Any())
            return availableReplacements;

        AnsiConsole.MarkupLine($"[{Colors.Trainer}]{team.Owner.Name}[/] has no more [{Colors.Pokemon}]pokemon[/] left in their team that haven't whited out.");
        return null;
    }
}