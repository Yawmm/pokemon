using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Events;
using Game.Moves.Damage;
using Game.Stats;

namespace Game.Moves.Effects;

/// <summary>
/// An <see cref="IMoveEffect"/> used to heal the acting <see cref="Pokemon"/> by an absolute or relative value. 
/// </summary>
/// <param name="Amount">The absolute amount that the <see cref="Pokemon"/> should be healed by, or the percentage that it should be healed by.</param>
/// <param name="Relative">Whether or not the <see cref="Amount"/> healed should be an absolute value, or a percentage of the <see cref="Pokemon"/>'s health.</param>
public record HealEffect(double Amount, bool Relative) : IMoveEffect
{
    /// <inheritdoc cref="IMoveEffect.Execute"/>
    public IEnumerable<Event> Execute(
        MoveTurn turn,
        Pokemon actor, 
        Pokemon opponent, 
        Func<DamageResult> calculateDamage
        )
    {
        var amount = Relative
            ? actor.Stats[Stat.Health] * Amount
            : Amount;
        
        actor.Heal(amount);
        return new[]
        {
            new HealEvent(actor, amount)
        };
    }
}