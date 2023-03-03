using Game.Companions;
using Newtonsoft.Json;

namespace Game.Moves;

/// <summary>
/// A class used to store the range of <see cref="PokemonMove"/> a <see cref="Pokemon"/> has, and the <see cref="Game.Moves.LearnSet"/> a <see cref="Pokemon"/> has.
/// </summary>
[JsonObject(IsReference = true)]
public record MoveList
{
    [JsonConstructor]
    public MoveList() { }
    
    public MoveList(Pokemon parent, LearnSet learnSet)
    {
        LearnSet = learnSet;
        
        var available = LearnSet.Check(parent.Experience.Level)
            .ToArray();
        
        Value = available.Length > 4
            ? available[^4..].ToList()
            : available.ToList();
    }
    
    /// <summary>
    /// The <see cref="Game.Moves.LearnSet"/> of the <see cref="Pokemon"/>.
    /// </summary>
    public LearnSet LearnSet { get; set; } = null!;

    /// <summary>
    /// The range of <see cref="PokemonMove"/> the <see cref="Pokemon"/> has.
    /// </summary>
    public List<PokemonMove> Value { get; set; } = new();
}