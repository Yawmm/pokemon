using Game.Companions;
using Game.Trainers;
using Game.Trainers.Deciders;

namespace Game.Content;

/// <summary>
/// A content class used to list all the available opponents.
/// </summary>
public static class OpponentList
{
    #region Bulbasaur evolutions
    
    public static Trainer Lian() => new(
        name: "Lian",
        decider: new RandomDecider(),
        pokemon: new List<Pokemon>
        {
            PokemonList.Bulbasaur()
        }
    );
    
    public static Trainer Benga() => new(
        name: "Benga",
        decider: new RandomDecider(),
        pokemon: new List<Pokemon>
        {
            PokemonList.Ivysaur()
        }
    );

    public static Trainer Arven() => new(
        name: "Arven",
        decider: new RandomDecider(),
        pokemon: new List<Pokemon>
        {
            PokemonList.Venusaur()
        }
    );

    #endregion
    
    #region Charmander evolutions 
    
    public static Trainer Hassel() => new(
        name: "Hassel",
        decider: new RandomDecider(),
        pokemon: new List<Pokemon>
        {
            PokemonList.Charmander(level: 8)
        }
    );
    
    public static Trainer Janin() => new(
        name: "Janin",
        decider: new RandomDecider(),
        pokemon: new List<Pokemon>
        {
            PokemonList.Charmeleon(level: 23)
        }
    );
    
    public static Trainer Ash() => new(
        name: "Ash",
        decider: new RandomDecider(),
        pokemon: new List<Pokemon>
        {
            PokemonList.Charizard(level: 43)
        }
    );
    
    #endregion

    #region Squirtle evolutions

    public static Trainer Elesa() => new(
        name: "Elesa",
        decider: new RandomDecider(),
        pokemon: new List<Pokemon>
        {
            PokemonList.Squirtle(level: 15)
        }
    );
    
    public static Trainer Grant() => new(
        name: "Grant",
        decider: new RandomDecider(),
        pokemon: new List<Pokemon>
        {
            PokemonList.Wartortle(level: 30)
        }
    );
    
    public static Trainer Misty() => new(
        name: "Misty",
        decider: new RandomDecider(),
        pokemon: new List<Pokemon>
        {
            PokemonList.Blastoise(level: 50)
        }
    );

    #endregion

    #region Mixed

    public static Trainer Jeff() => new(
        name: "Jeff",
        decider: new RandomDecider(),
        pokemon: new List<Pokemon>
        {
            PokemonList.Bulbasaur(level: 8),
            PokemonList.Charmander(level: 15),
            PokemonList.Squirtle(level: 1)
        }
    );
    
    public static Trainer Akari() => new(
        name: "Akari",
        decider: new RandomDecider(),
        pokemon: new List<Pokemon>
        {
            PokemonList.Ivysaur(level: 23),
            PokemonList.Charmeleon(level: 30),
            PokemonList.Wartortle(level: 16)
        }
    );
    
    public static Trainer Oak() => new(
        name: "Gary Oak",
        decider: new RandomDecider(),
        pokemon: new List<Pokemon>
        {
            PokemonList.Venusaur(level: 60),
            PokemonList.Charizard(level: 80),
            PokemonList.Blastoise(level: 100)
        }
    );

    #endregion
    
}