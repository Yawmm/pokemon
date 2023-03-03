using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Events;
using Game.Moves.Damage;
using Game.Stats;

namespace Game.Moves.Effects;

/// <summary>
/// An <see cref="IMoveEffect"/> used to apply a change in a stat stage in the acting, or opposing, <see cref="Pokemon"/>.
/// </summary>
/// <param name="Stat">The <see cref="Stat"/> type from which the stage value will be changed.</param>
/// <param name="Stages">The amount by which the stat stage should change.</param>
/// <param name="Attacker">Whether or not the stat stage change should apply to the acting, or opposing, <see cref="Pokemon"/>.</param>
public record StatStageChangeEffect(Stat Stat, int Stages, bool Attacker = true) : IMoveEffect
{
    /// <inheritdoc cref="IMoveEffect.Execute"/>
    public IEnumerable<Event> Execute(
        MoveTurn turn,
        Pokemon actor,
        Pokemon opponent,
        Func<DamageResult> calculateDamage
        )
    {
        var pokemon = Attacker ? actor : opponent;
        
        if (Math.Abs(opponent.Stages.Value[Stat]) == 6)
            return new[]
            {
                new MaximumStageEvent(pokemon, Stat)
            };

        pokemon.Stages.Change(Stat, Stages);
        return new[]
        {
            new StageChangeEvent(pokemon, Stat, Stages)
        };
    }
}