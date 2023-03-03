using Game.Companions;
using Game.Natures;
using Game.Stats;
using Game.Types;

namespace Game.Content;

/// <summary>
/// A content class used to list all the available <see cref="Pokemon"/>.
/// </summary>
public static class PokemonList
{
    #region Bulbasaur evolutions

    public static Pokemon Bulbasaur(int level = 1) => Pokemon
        .Create(
            name: "Bulbasaur",
            nature: PokemonNatureList.Random(),
            types: new [] { ElementalType.Grass, ElementalType.Poison }
        )
        .WithExperience(level, yield: 64)
        .WithEvolution(level: 16, evolution: Ivysaur())
        .WithLearnSet(learnSet: LearnsetList.Bulbasaur())
        .WithStatistics(
            basis: new Dictionary<Stat, int>
            {
                { Stat.Health, 45 },
                { Stat.Attack, 49 },
                { Stat.Defense, 49 },
                { Stat.SpecialAttack, 65 },
                { Stat.SpecialDefense, 65 },
                { Stat.Speed, 45 }
            },
            yield: new Dictionary<Stat, int>
            {
                { Stat.SpecialAttack, 1 }
            }
        )
        .Build();
    
    public static Pokemon Ivysaur(int level = 16) => Pokemon
        .Create(
            name: "Ivysaur",
            nature: PokemonNatureList.Random(),
            types: new [] { ElementalType.Grass, ElementalType.Poison }
        )
        .WithExperience(level, yield: 142)
        .WithEvolution(level: 36, evolution: Venusaur())
        .WithLearnSet(learnSet: LearnsetList.Ivysaur())
        .WithStatistics(
            basis: new Dictionary<Stat, int>
            {
                { Stat.Health, 60 },
                { Stat.Attack, 62 },
                { Stat.Defense, 63 },
                { Stat.SpecialAttack, 80 },
                { Stat.SpecialDefense, 80 },
                { Stat.Speed, 60 }
            },
            yield: new Dictionary<Stat, int>
            {
                { Stat.SpecialAttack, 1 },
                { Stat.SpecialDefense, 1 }
            }
        )
        .Build();
    
    public static Pokemon Venusaur(int level = 36) => Pokemon
        .Create(
            name: "Venusaur",
            nature: PokemonNatureList.Random(),
            types: new [] { ElementalType.Grass, ElementalType.Poison }
        )
        .WithExperience(level, yield: 263)
        .WithLearnSet(learnSet: LearnsetList.Venusaur())
        .WithStatistics(
            basis: new Dictionary<Stat, int>
            {
                { Stat.Health, 80 },
                { Stat.Attack, 82 },
                { Stat.Defense, 83 },
                { Stat.SpecialAttack, 100 },
                { Stat.SpecialDefense, 100 },
                { Stat.Speed, 80 }
            },
            yield: new Dictionary<Stat, int>
            {
                { Stat.SpecialAttack, 2 },
                { Stat.SpecialDefense, 1 }
            }
        )
        .Build();

    #endregion

    #region Charmander evolutions

    public static Pokemon Charmander(int level = 1) => Pokemon
        .Create(
            name: "Charmander",
            nature: PokemonNatureList.Random(),
            types: new [] { ElementalType.Fire }
        )
        .WithExperience(level, yield: 62)
        .WithEvolution(level: 16, evolution: Charmeleon())
        .WithLearnSet(learnSet: LearnsetList.Charmander())
        .WithStatistics(
            basis: new Dictionary<Stat, int>
            {
                { Stat.Health, 39 },
                { Stat.Attack, 52 },
                { Stat.Defense, 43 },
                { Stat.SpecialAttack, 60 },
                { Stat.SpecialDefense, 50 },
                { Stat.Speed, 65 }
            },
            yield: new Dictionary<Stat, int>
            {
                { Stat.Speed, 1 }
            }
        )
        .Build();

    public static Pokemon Charmeleon(int level = 16) => Pokemon
        .Create(
            name: "Charmeleon",
            nature: PokemonNatureList.Random(),
            types: new [] { ElementalType.Fire }
        )
        .WithExperience(level, yield: 142)
        .WithEvolution(level: 36, evolution: Charizard())
        .WithLearnSet(learnSet: LearnsetList.Charmeleon())
        .WithStatistics(
            basis: new Dictionary<Stat, int>
            {
                { Stat.Health, 58 },
                { Stat.Attack, 64 },
                { Stat.Defense, 58 },
                { Stat.SpecialAttack, 80 },
                { Stat.SpecialDefense, 65 },
                { Stat.Speed, 80 }
            },
            yield: new Dictionary<Stat, int>
            {
                { Stat.SpecialAttack, 1 },
                { Stat.Speed, 1 }
            }
        )
        .Build();

    public static Pokemon Charizard(int level = 36) => Pokemon
        .Create(
            name: "Charizard",
            nature: PokemonNatureList.Random(),
            types: new [] { ElementalType.Fire, ElementalType.Flying }
        )
        .WithExperience(level, yield: 167)
        .WithLearnSet(learnSet: LearnsetList.Charizard())
        .WithStatistics(
            basis: new Dictionary<Stat, int>
            {
                { Stat.Health, 78 },
                { Stat.Attack, 84 },
                { Stat.Defense, 78 },
                { Stat.SpecialAttack, 109 },
                { Stat.SpecialDefense, 85 },
                { Stat.Speed, 100 }
            },
            yield: new Dictionary<Stat, int>
            {
                { Stat.SpecialAttack, 3 }
            }
        )
        .Build();

    #endregion

    #region Squirtle evolutions

    // Squirtle evolutions
    
     public static Pokemon Squirtle(int level = 1) => Pokemon
         .Create(
             name: "Squirtle",
             nature: PokemonNatureList.Random(),
             types: new [] { ElementalType.Water }
         )
         .WithExperience(level, yield: 63)
         .WithEvolution(level: 16, evolution: Wartortle())
         .WithLearnSet(learnSet: LearnsetList.Squirtle())
         .WithStatistics(
             basis: new Dictionary<Stat, int>
             {
                 { Stat.Health, 44 },
                 { Stat.Attack, 48 },
                 { Stat.Defense, 65 },
                 { Stat.SpecialAttack, 50 },
                 { Stat.SpecialDefense, 64 },
                 { Stat.Speed, 43 }
             },
             yield: new Dictionary<Stat, int>
             {
                 { Stat.Defense, 1 }
             }
         )
         .Build();

     public static Pokemon Wartortle(int level = 16) => Pokemon
         .Create(
             name: "Wartortle",
             nature: PokemonNatureList.Random(),
             types: new [] { ElementalType.Water }
         )
         .WithExperience(level, yield: 142)
         .WithEvolution(level: 36, evolution: Blastoise())
         .WithLearnSet(learnSet: LearnsetList.Wartortle())
         .WithStatistics(
             basis: new Dictionary<Stat, int>
             {
                 { Stat.Health, 59 },
                 { Stat.Attack, 63 },
                 { Stat.Defense, 80 },
                 { Stat.SpecialAttack, 65 },
                 { Stat.SpecialDefense, 80 },
                 { Stat.Speed, 58 }
             },
             yield: new Dictionary<Stat, int>
             {
                 { Stat.Defense, 1 },
                 { Stat.SpecialDefense, 1 }
             }
         )
         .Build();

     public static Pokemon Blastoise(int level = 36) => Pokemon
         .Create(
             name: "Blastoise",
             nature: PokemonNatureList.Random(),
             types: new [] { ElementalType.Water }
         )
         .WithExperience(level, yield: 165)
         .WithLearnSet(learnSet: LearnsetList.Blastoise())
         .WithStatistics(
             basis: new Dictionary<Stat, int>
             {
                 { Stat.Health, 79 },
                 { Stat.Attack, 83 },
                 { Stat.Defense, 100 },
                 { Stat.SpecialAttack, 85 },
                 { Stat.SpecialDefense, 105 },
                 { Stat.Speed, 78 }
             },
             yield: new Dictionary<Stat, int>
             {
                 { Stat.SpecialDefense, 3 }
             }
         )
         .Build();

    #endregion
}