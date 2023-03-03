using Game.Battles.Events;
using Game.Battles.Results;
using Game.Trainers;
using Spectre.Console;

namespace Game.Battles.Turns;

/// <summary>
/// An <see cref="ITurn"/> which retreats the <see cref="Trainer"/> from the current <see cref="Battle"/>.
/// </summary>
public record RetreatTurn : ITurn
{
    /// <inheritdoc cref="ITurn.Priority"/>
    public double Priority => double.MaxValue;

    /// <inheritdoc cref="ITurn.Team"/>
    public required Team Team { get; set; }

    /// <inheritdoc cref="ITurn.Execute"/>
    public IEnumerable<Event> Execute(Battle battle)
    {
        if (!AnsiConsole.Confirm($"Are you sure you want to retreat from this battle, [{Colors.Trainer}]trainer[/]?"))
            return new List<Event>();
        
        // End the battle with a retreated result
        battle.End(new RetreatedResult(battle.Player));
        
        // Log the retreat
        return new[]
        {
            new Event
            {
                Message = $"[{Colors.Trainer}]{Team.Owner.Name}[/] has retreated from battle!"
            }
        };
    }
}