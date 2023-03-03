using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Events;
using Game.Moves.Damage;

namespace Game.Moves.Effects;

/// <summary>
/// An <see cref="IMoveEffect"/> used to hit the opposing <see cref="Pokemon"/>.
/// </summary>
/// <remarks>Does apply a range of <see cref="IDamageModifier"/> to the calculations. Use <see cref="DamageEffect"/> if an absolute damage amount is required.</remarks>
public record HitEffect : IMoveEffect
{
    /// <inheritdoc cref="IMoveEffect.Execute"/>
    public IEnumerable<Event> Execute(
        MoveTurn turn, 
        Pokemon actor, 
        Pokemon opponent,
        Func<DamageResult> calculateDamage
        )
    {
        var damage = calculateDamage();
        if (damage.Value is null)
            return new[]
            {
                new NoEffectEvent(turn.Move, actor, opponent)
            };

        var result = (double)damage.Value;
        opponent.Damage(result);
        
        return new[]
        {
            new HitEvent(actor, opponent, damage)
        };
    }
}