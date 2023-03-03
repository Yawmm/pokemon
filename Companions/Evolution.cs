using Game.Trainers;

namespace Game.Companions;

/// <summary>
/// A class representing an evolution of a <see cref="Game.Companions.Pokemon"/> at a certain level.
/// </summary>
public class Evolution
{
    /// <summary>
    /// The level on which the <see cref="Trainer"/> can choose to evolve to the <see cref="Pokemon"/>.
    /// </summary>
    public required int Level { get; init; }
    
    /// <summary>
    /// The <see cref="Game.Companions.Pokemon"/> to which can be evolved.
    /// </summary>
    public required Pokemon Pokemon { get; init; }
}