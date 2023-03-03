namespace Game.Moves.Damage;

/// <summary>
/// A class representing the damage that occurred from a <see cref="PokemonMove"/>, and its effects.
/// </summary>
public class DamageResult
{
    /// <summary>
    /// The final damage value of the <see cref="PokemonMove"/>.
    /// </summary>
    public double? Value { get; set; }

    /// <summary>
    /// Whether or not the <see cref="PokemonMove"/> was a critical hit.
    /// </summary>
    public bool IsCritical { get; set; }
    
    /// <summary>
    /// The effectiveness of the <see cref="PokemonMove"/>.
    /// </summary>
    public decimal Effectiveness { get; set; }
}