using Game.Companions;
using Newtonsoft.Json;

namespace Game.Moves;

/// <summary>
/// A class used to represent a set of <see cref="PokemonMove"/> which a <see cref="Pokemon"/> can learn at certain level.
/// </summary>
[JsonObject(IsReference = true)]
public class LearnSet
{
    /// <summary>
    /// The list of steps of the <see cref="LearnSet"/>.
    /// </summary>
    public required Dictionary<int, List<PokemonMove>> Value { get; set; }

    /// <summary>
    /// Gets the available list of <see cref="PokemonMove"/> a <see cref="Pokemon"/> could learn.
    /// </summary>
    /// <param name="level">The level of the <see cref="Pokemon"/>.</param>
    /// <returns>The list of available <see cref="PokemonMove"/>.</returns>
    public IEnumerable<PokemonMove> Check(int level)
        => Value.Where(s => s.Key <= level)
            .SelectMany(s => s.Value);
}