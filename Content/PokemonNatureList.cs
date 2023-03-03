using Game.Natures;
using Game.Stats;

namespace Game.Content;

/// <summary>
/// A content class used to list all the available <see cref="PokemonNature"/>.
/// </summary>
public static class PokemonNatureList
{
    /// <summary>
    /// Retrieve a random available <see cref="PokemonNature"/>.
    /// </summary>
    /// <returns>The random <see cref="PokemonNature"/>.</returns>
    public static PokemonNature Random()
        => List[System.Random.Shared.Next(0, List.Count - 1)];

    /// <summary>
    /// A list of all the available <see cref="PokemonNature"/>.
    /// </summary>
    private static List<PokemonNature> List => new()
    {
        Hardy,
        Lonely,
        Brave,
        Adamant,
        Naughty,
        Bold,
        Docile
    };
    
    public static PokemonNature Hardy => new()
    {
        Type = Nature.Hardy
    };
    
    public static PokemonNature Lonely => new()
    {
        Type = Nature.Lonely,
        Increased = Stat.Attack,
        Decreased = Stat.Defense
    };
    
    public static PokemonNature Brave => new()
    {
        Type = Nature.Brave,
        Increased = Stat.Attack,
        Decreased = Stat.Speed
    };
    
    public static PokemonNature Adamant => new()
    {
        Type = Nature.Adamant,
        Increased = Stat.Attack,
        Decreased = Stat.SpecialAttack
    };
    
    public static PokemonNature Naughty => new()
    {
        Type = Nature.Naughty,
        Increased = Stat.Attack,
        Decreased = Stat.SpecialDefense
    };
    
    public static PokemonNature Bold => new()
    {
        Type = Nature.Bold,
        Increased = Stat.Defense,
        Decreased = Stat.Attack
    };
    
    public static PokemonNature Docile => new()
    {
        Type = Nature.Docile
    };
}