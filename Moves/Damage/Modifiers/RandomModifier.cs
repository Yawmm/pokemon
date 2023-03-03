using Game.Companions;

namespace Game.Moves.Damage.Modifiers;

/// <summary>
/// An <see cref="IDamageModifier"/> used to add a random deviation to the <see cref="DamageResult"/>.
/// </summary>
public class RandomModifier : IDamageModifier
{
    /// <inheritdoc cref="IDamageModifier.Apply"/>
    public double Apply(DamageResult current, PokemonMove move, Pokemon attacker, Pokemon defender)
        => Random.Shared.Next(85, 100) / 100.0;
}