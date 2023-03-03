using Game.Companions;

namespace Game.Moves.Damage;

/// <summary>
/// An interface representing the modifiers that need to be applied when calculating the damage of a <see cref="PokemonMove"/>.
/// </summary>
public interface IDamageModifier
{
    /// <summary>
    /// Apply the <see cref="IDamageModifier"/> to the current <see cref="DamageResult"/>.
    /// </summary>
    /// <param name="current">The current <see cref="IDamageModifier"/>.</param>
    /// <param name="move">The <see cref="PokemonMove"/> that is being executed.</param>
    /// <param name="attacker">The <see cref="Pokemon"/> which is executing the <see cref="PokemonMove"/>.</param>
    /// <param name="defender">The <see cref="Pokemon"/> which will be receiving the damage of the <see cref="move"/>.</param>
    /// <returns></returns>
    double Apply(
        DamageResult current,
        PokemonMove move,
        Pokemon attacker,
        Pokemon defender
    );
}