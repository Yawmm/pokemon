using Game.Companions;

namespace Game.Moves.Damage.Modifiers;

/// <summary>
/// An <see cref="IDamageModifier"/> used to add the STAB-bonus to the <see cref="DamageResult"/>. See: https://bulbapedia.bulbagarden.net/wiki/Same-type_attack_bonus
/// </summary>
public class StabModifier : IDamageModifier
{
    /// <inheritdoc cref="IDamageModifier.Apply"/>
    public double Apply(DamageResult current, PokemonMove move, Pokemon attacker, Pokemon defender)
        => attacker.Types.Contains(move.Type) ? 1.5 : 1.0;
}