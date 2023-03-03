using Game.Experience;
using Game.Experience.Effects;
using Game.Moves;
using Game.Stats;

namespace Game.Companions;

/// <summary>
/// A builder class used to create a finished <see cref="Pokemon"/>.
/// </summary>
public class PokemonBuilder
{
    public PokemonBuilder(Pokemon instance)
    {
        _instance = instance;
    }
    
    /// <summary>
    /// The instance of the <see cref="Pokemon"/> being constructed.
    /// </summary>
    private readonly Pokemon _instance;

    /// <summary>
    /// Add a <see cref="LearnSet"/> to the <see cref="Pokemon"/> instance.
    /// </summary>
    /// <param name="learnSet">The <see cref="LearnSet"/> to be added.</param>
    /// <returns>The same <see cref="PokemonBuilder"/> from which the <see cref="Pokemon"/> can be built.</returns>
    public PokemonBuilder WithLearnSet(LearnSet learnSet)
    {
        _instance.Moves = new MoveList(_instance, learnSet);
        
        return this;
    }

    /// <summary>
    /// Add a <see cref="StatList"/> to the <see cref="Pokemon"/> instance.
    /// </summary>
    /// <param name="basis">The base stats of the <see cref="Pokemon"/>.</param>
    /// <param name="yield">The stat yield another <see cref="Pokemon"/> gets when killing this <see cref="Pokemon"/>.</param>
    /// <returns>The same <see cref="PokemonBuilder"/> from which the <see cref="Pokemon"/> can be built.</returns>
    public PokemonBuilder WithStatistics(Dictionary<Stat, int> basis, Dictionary<Stat, int> yield)
    {
        _instance.Statistics = new StatList(_instance, basis, yield);

        return this;
    }
    
    /// <summary>
    /// Add an <see cref="Evolution"/> to the <see cref="Pokemon"/> instance.
    /// </summary>
    /// <param name="level">The level on which the <see cref="Pokemon"/> can evolve.</param>
    /// <param name="evolution">The <see cref="Pokemon"/> to which can be evolved.</param>
    /// <returns>The same <see cref="PokemonBuilder"/> from which the <see cref="Pokemon"/> can be built.</returns>
    public PokemonBuilder WithEvolution(int level, Pokemon evolution)
    {
        _instance.Evolution = new Evolution
        {
            Level = level,
            Pokemon = evolution
        };

        return this;
    }
    
    /// <summary>
    /// Add an <see cref="ExperienceList"/> to the <see cref="Pokemon"/> instance.
    /// </summary>
    /// <param name="level">The starting level of the <see cref="Pokemon"/>.</param>
    /// <param name="yield">The experience yield another <see cref="Pokemon"/> gets when killing this <see cref="Pokemon"/>.</param>
    /// <returns>The same <see cref="PokemonBuilder"/> from which the <see cref="Pokemon"/> can be built.</returns>
    public PokemonBuilder WithExperience(int level, int yield)
    {
        _instance.Experience = new ExperienceList
        {
            Value = Experiences.FromLevel(level),
            Yield = yield,
            Effects = new List<ILevelEffect>
            {
                new GeneralEffect(),
                new LearnEffect(),
                new EvolveEffect()
            }
        };

        return this;
    }

    /// <summary>
    /// Finalize the <see cref="Pokemon"/> instance, <see cref="_instance"/>.
    /// </summary>
    /// <returns>The constructed <see cref="Pokemon"/>.</returns>
    public Pokemon Build()
        => _instance;
}