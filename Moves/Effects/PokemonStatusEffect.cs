using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Events;
using Game.Moves.Damage;
using Game.Statuses;

namespace Game.Moves.Effects;

/// <summary>
/// An <see cref="IMoveEffect"/> used to apply a <see cref="PokemonStatus"/> to the opposing <see cref="Pokemon"/>.
/// </summary>
/// <param name="Status">The <see cref="PokemonStatus"/> that should be applied.</param>
/// <param name="Color">The color which the <see cref="ApplyStatusEvent"/> should use when logging the event.</param>
public record StatusConditionEffect(PokemonStatus Status, string Color) : IMoveEffect
{
    /// <inheritdoc cref="IMoveEffect.Execute"/>
    public IEnumerable<Event> Execute(
        MoveTurn turn,
        Pokemon actor,
        Pokemon opponent,
        Func<DamageResult> calculateDamage
        )
    {
        if (!opponent.StatusConditions.Contains(Status))
            opponent.StatusConditions.Add(Status);

        return new[]
        {
            new ApplyStatusEvent(actor, opponent, Status, Color)
        };
    }
}