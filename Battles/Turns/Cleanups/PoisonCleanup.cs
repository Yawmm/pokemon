using Game.Battles.Events;
using Game.Stats;
using Game.Statuses;

namespace Game.Battles.Turns.Cleanups;

/// <summary>
/// A class used to apply poison damage to the actor of the <see cref="ITurn"/>'s team after the <see cref="ITurn"/>
/// was executed.
/// </summary>
public class PoisonCleanup : ITurnCleanup
{
    /// <inheritdoc cref="ITurnCleanup.Execute"/>
    public IEnumerable<Event>? Execute(ITurn turn, Battle battle)
    {
        var actor = turn.Team.Actor;
        if (!actor.StatusConditions.Contains(PokemonStatus.Poison))
            return null;

        // Damage is 1/8 of maximum health, see https://bulbapedia.bulbagarden.net/wiki/Poison_(status_condition)#Effect
        var damage = actor.Statistics.Maximum[Stat.Health] / 8;
        actor.Damage(damage);

        return new[]
        {
            new Event
            {
                Message = $"The [{Colors.Poison}]poison[/] effect on [{Colors.Pokemon}]{turn.Team.Actor}[/] hurt it by {damage:F1} damage!"
            }
        };
    }
}