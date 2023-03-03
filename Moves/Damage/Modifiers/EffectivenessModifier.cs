using Game.Companions;
using Game.Types;

namespace Game.Moves.Damage.Modifiers;

/// <summary>
/// An <see cref="IDamageModifier"/> used to add effectiveness modifiers to the <see cref="DamageResult"/>.
/// </summary>
public class EffectivenessModifier : IDamageModifier
{
    /// <inheritdoc cref="IDamageModifier.Apply"/>
    public double Apply(DamageResult current, PokemonMove move, Pokemon attacker, Pokemon defender)
    {
        current.Effectiveness = TypeEffectiveness.GetEffectiveness(move, defender);
        return (double)current.Effectiveness;
    }
}