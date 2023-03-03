using Game.Companions;
using Game.Trainers.Deciders;
using Newtonsoft.Json;

namespace Game.Trainers;

/// <summary>
/// A class representing a person or player in the game.
/// </summary>
[JsonObject(IsReference = true)]
public record Trainer
{
    [JsonConstructor]
    public Trainer() { }
    
    public Trainer(string name, IDecider decider, List<Pokemon> pokemon)
    {
        Name = name;
        Decider = decider;
        
        foreach (var item in pokemon)
            AddPokemon(item);
    }
    
    /// <summary>
    /// The name of the <see cref="Trainer"/>.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The <see cref="IDecider"/> of the <see cref="Trainer"/>.
    /// </summary>
    public IDecider Decider { get; set; }

    /// <summary>
    /// The range of <see cref="Game.Companions.Pokemon"/> the <see cref="Trainer"/> possesses.
    /// </summary>
    public List<Pokemon> Pokemon { get; set; } = new();

    /// <summary>
    /// Add a new <see cref="Game.Companions.Pokemon"/> to the <see cref="Trainer"/>.
    /// </summary>
    /// <param name="pokemon">The <see cref="Game.Companions.Pokemon"/> which should be added.</param>
    public void AddPokemon(Pokemon pokemon)
    {
        Pokemon.Add(pokemon);
        pokemon.Owner = this;
    }

    /// <summary>
    /// Remove a <see cref="Game.Companions.Pokemon"/> from the <see cref="Trainer"/>.
    /// </summary>
    /// <param name="pokemon">The <see cref="Game.Companions.Pokemon"/> which should be removed.</param>
    public void RemovePokemon(Pokemon pokemon)
    {
        Pokemon.Remove(pokemon);
        pokemon.Owner = null;
    }

    /// <summary>
    /// Reset all the <see cref="Game.Companions.Pokemon"/> the <see cref="Trainer"/> possesses.
    /// </summary>
    public void Reset()
    {
        foreach (var member in Pokemon)
            member.Reset();
    }

    public override string ToString() => $"{Name} - Level {Pokemon.Average(p => p.Experience.Level):F0}";
}