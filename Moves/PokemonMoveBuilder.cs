using Game.Companions;

namespace Game.Moves;

/// <summary>
/// A builder class used to create a finished <see cref="PokemonMove"/>.
/// </summary>
public class PokemonMoveBuilder
{
    public PokemonMoveBuilder(PokemonMove instance)
    {
        _instance = instance;
    }
    
    /// <summary>
    /// The instance of the <see cref="PokemonMove"/> being constructed.
    /// </summary>
    private readonly PokemonMove _instance;
    
    /// <summary>
    /// Add a <see cref="PowerPoint"/> to the <see cref="PokemonMove"/>
    /// </summary>
    /// <param name="maximum">The maximum new PP of the <see cref="PokemonMove"/>.</param>
    /// <returns>The same <see cref="PokemonMoveBuilder"/> from which the <see cref="PokemonMove"/> can be built.</returns>
    public PokemonMoveBuilder WithPowerPoint(int maximum)
    {
        _instance.PP = new PowerPoint(maximum);
        return this;
    }
    
    /// <summary>
    /// Add a power value to the <see cref="PokemonMove"/>
    /// </summary>
    /// <param name="value">The new power of the <see cref="PokemonMove"/>.</param>
    /// <returns>The same <see cref="PokemonMoveBuilder"/> from which the <see cref="PokemonMove"/> can be built.</returns>
    public PokemonMoveBuilder WithPower(int value)
    {
        _instance.Power = value;
        return this;
    }
    
    /// <summary>
    /// Add an accuracy value to the <see cref="PokemonMove"/>
    /// </summary>
    /// <param name="value">The new accuracy of the <see cref="PokemonMove"/>.</param>
    /// <returns>The same <see cref="PokemonMoveBuilder"/> from which the <see cref="PokemonMove"/> can be built.</returns>
    public PokemonMoveBuilder WithAccuracy(int value)
    {
        _instance.Accuracy = value;
        return this;
    }
    
    /// <summary>
    /// Add a <see cref="PokemonMoveStage"/> to the <see cref="PokemonMove"/>
    /// </summary>
    /// <param name="stage">The <see cref="PokemonMoveStage"/> that should be added to the <see cref="PokemonMove"/>.</param>
    /// <returns>The same <see cref="PokemonMoveBuilder"/> from which the <see cref="PokemonMove"/> can be built.</returns>
    public PokemonMoveBuilder AddStage(PokemonMoveStage stage)
    {
        _instance.Stages.Add(stage);
        return this;
    }

    /// <summary>
    /// Start the construction of a <see cref="PokemonMoveStage"/>.
    /// </summary>
    /// <returns>The same <see cref="PokemonMoveStageBuilder"/> from which the <see cref="PokemonMoveStage"/> can be built.</returns>
    public PokemonMoveStageBuilder Stage()
        => new(this);
    
    /// <summary>
    /// Finalize the <see cref="PokemonMove"/> instance, <see cref="_instance"/>.
    /// </summary>
    /// <returns>The constructed <see cref="PokemonMove"/>.</returns>
    public PokemonMove Build()
        => _instance;
}