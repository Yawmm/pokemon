using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Moves.Damage;

namespace Game.Moves.Wrappers;

/// <summary>
/// A wrapper class used to wrap an <see cref="IMoveEffect"/> by a random chance.
/// If the generated number is below the chance, the <see cref="IMoveEffect"/> will not be applied.
/// </summary>
/// <param name="Effect">The <see cref="IMoveEffect"/> that should be wrapped.</param>
/// <param name="Chance">The chance of the <see cref="IMoveEffect"/> occurring.</param>
public record ChanceWrapper(IMoveEffect Effect, int Chance) : IMoveEffect
{
    /// <inheritdoc cref="IMoveEffect.Execute"/>
    public IEnumerable<Event> Execute(MoveTurn turn, Pokemon actor, Pokemon opponent, Func<DamageResult> calculateDamage)
    {
        var rand = Random.Shared.Next(0, 100);
        if (Chance != 100 && rand > Chance)
            return new List<Event>();

        return Effect.Execute(turn, actor, opponent, calculateDamage);
    }
}