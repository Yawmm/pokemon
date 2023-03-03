using Game.Stats;

namespace Game.Natures;

/// <summary>
/// A class used to add the range of <see cref="Stat"/> which are modified by the <see cref="Nature"/> type.
/// </summary>
public record PokemonNature
{
    /// <summary>
    /// The actual <see cref="Nature"/> of the <see cref="PokemonNature"/>.
    /// </summary>
    public required Nature Type { get; init; }
    
    /// <summary>
    /// The <see cref="Stat"/> which is increased by the <see cref="PokemonNature"/>.
    /// </summary>
    public Stat? Increased { get; init; }
    
    /// <summary>
    /// The <see cref="Stat"/> which is decreased by the <see cref="PokemonNature"/>.
    /// </summary>
    public Stat? Decreased { get; init; }
}