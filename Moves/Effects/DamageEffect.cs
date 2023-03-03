using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Events;
using Game.Moves.Damage;

namespace Game.Moves.Effects;

/// <summary>
/// An <see cref="IMoveEffect"/> used to damage the opposing <see cref="Pokemon"/> by an absolute value. 
/// </summary>
/// <remarks>Does not apply any <see cref="IDamageModifier"/> to the calculations. Use <see cref="HitEffect"/> it that is the desired effect.</remarks>
/// <param name="Damage">The absolute damage the opposing <see cref="Pokemon"/> should receive.</param>
public record DamageEffect(int Damage) : IMoveEffect
{
    /// <inheritdoc cref="IMoveEffect.Execute"/>
    public IEnumerable<Event> Execute(
        MoveTurn turn,
        Pokemon actor,
        Pokemon opponent,
        Func<DamageResult> calculateDamage
        )
    {
        // Get absolute damage
        var damage = new DamageResult
        {
            Value = Damage,
            IsCritical = false,
            Effectiveness = 1
        };

        if (damage.Value is null)
            return new[]
            {
                new NoEffectEvent(turn.Move, actor, opponent)
            };

        // Apply damage
        var result = (double)damage.Value;
        opponent.Damage(result);

        return new[]
        {
            new HitEvent(actor, opponent, damage)
        };
    }
}