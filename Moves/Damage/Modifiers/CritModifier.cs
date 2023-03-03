using Game.Companions;
using Game.Stats;

namespace Game.Moves.Damage.Modifiers;

/// <summary>
/// An <see cref="IDamageModifier"/> used to add critical hits to the <see cref="DamageResult"/>.
/// </summary>
/// <param name="High">Whether or not the critical hit calculation should use the higher chance formula.</param>
public record CritModifier(bool High = false) : IDamageModifier
{
    /// <inheritdoc cref="IDamageModifier.Apply"/>
    public double Apply(DamageResult current, PokemonMove move, Pokemon attacker, Pokemon defender)
    {
        var threshold = High
            ? Math.Min(8 * (attacker.Statistics.Base[Stat.Speed] / 2), 255)
            : attacker.Statistics.Base[Stat.Speed] / 2;

        current.IsCritical = Random.Shared.Next(0, 255) < threshold;
        return current.IsCritical ? 2.0 : 1.0;
    }
}