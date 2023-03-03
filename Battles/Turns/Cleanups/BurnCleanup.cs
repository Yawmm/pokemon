using Game.Battles.Events;
using Game.Stats;
using Game.Statuses;

namespace Game.Battles.Turns.Cleanups;

/// <summary>
/// A class used to apply burn damage to the actor of the <see cref="ITurn"/>'s team after the <see cref="ITurn"/>
/// was executed.
/// </summary>
public class BurnCleanup : ITurnCleanup
{
    /// <inheritdoc cref="ITurnCleanup.Execute"/>
    public IEnumerable<Event>? Execute(ITurn turn, Battle battle)
    {
        var actor = turn.Team.Actor;
        if (!actor.StatusConditions.Contains(PokemonStatus.Burn))
            return null;

        // Damage is 1/8 of maximum health, see https://bulbapedia.bulbagarden.net/wiki/Burn_(status_condition)#Effect
        var damage = actor.Statistics.Maximum[Stat.Health] / 8;
        actor.Damage(damage);

        return new[]
        {
            new Event
            {
                Message = $"The [{Colors.Burn}]burn[/] effect on [{Colors.Pokemon}]{actor}[/] hurt it by {damage:F1} damage!"
            }
        };
    }
}