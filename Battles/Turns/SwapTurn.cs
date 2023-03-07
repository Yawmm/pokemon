using Game.Battles.Events;
using Game.Trainers;

namespace Game.Battles.Turns;

/// <summary>
/// An <see cref="ITurn"/> which swaps the current actor of the <see cref="Team"/>.
/// </summary>
public class SwapTurn : ITurn
{
    /// <inheritdoc cref="ITurn.Priority"/>
    public double Priority => double.MaxValue;

    /// <inheritdoc cref="ITurn.Team"/>
    public required Team Team { get; init; }
    
    /// <inheritdoc cref="ITurn.Execute"/>
    public IEnumerable<Event> Execute(Battle battle)
    {
        // Replace the acting pokemon
        var available = Team.Members.Where(p => !p.Whiteout);
        Team.Actor = Team.Owner.Decider
            .Single($"Which [{Colors.Pokemon}]pokemon[/] will replace [{Colors.Pokemon}]{Team.Actor}[/]?", available);
        
        // Log the swap
        return new[]
        {
            new Event
            {
                Message = $"[{Colors.Trainer}]{Team.Owner.Name}[/] chose [{Colors.Pokemon}]{Team.Actor}[/] to be their new acting pokemon."
            }
        };
    }
}