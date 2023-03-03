using Game.Companions;
using Game.Moves;

namespace Game.Types;

/// <summary>
/// A class used to calculate the effectiveness of one <see cref="ElementalType"/> against another.
/// </summary>
public static class TypeEffectiveness
{
    public const decimal Nullified = 0;
    public const decimal Ineffective = 0.25M;
    public const decimal Disadvantaged = 0.50M;
    public const decimal Default = 1;
    public const decimal Advantaged = 2;
    public const decimal Effective = 4;
    
    /// <summary>
    /// A map which contains all the <see cref="ElementalType"/> a <see cref="ElementalType"/> is advantaged against.
    /// </summary>
    private static readonly Dictionary<ElementalType, IEnumerable<ElementalType>> Advantages = new()
    {
        { ElementalType.Normal, new List<ElementalType>() },
        { ElementalType.Fighting, new [] { ElementalType.Normal, ElementalType.Rock, ElementalType.Steel, ElementalType.Ice, ElementalType.Dark } },
        { ElementalType.Flying, new [] { ElementalType.Fighting, ElementalType.Bug, ElementalType.Grass } },
        { ElementalType.Poison, new [] { ElementalType.Grass } },
        { ElementalType.Ground, new [] { ElementalType.Poison, ElementalType.Rock, ElementalType.Steel, ElementalType.Fire, ElementalType.Electric } },
        { ElementalType.Rock, new [] { ElementalType.Flying, ElementalType.Bug, ElementalType.Fire, ElementalType.Ice } },
        { ElementalType.Bug, new [] { ElementalType.Grass, ElementalType.Psychic, ElementalType.Dark } },
        { ElementalType.Ghost, new [] { ElementalType.Ghost, ElementalType.Psychic } },
        { ElementalType.Steel, new [] { ElementalType.Rock, ElementalType.Ice } },
        { ElementalType.Fire, new [] { ElementalType.Bug, ElementalType.Steel, ElementalType.Grass, ElementalType.Ice } },
        { ElementalType.Water, new [] { ElementalType.Ground, ElementalType.Rock, ElementalType.Fire } },
        { ElementalType.Grass, new [] { ElementalType.Ground, ElementalType.Rock, ElementalType.Water } },
        { ElementalType.Electric, new [] { ElementalType.Flying, ElementalType.Water } },
        { ElementalType.Psychic, new [] { ElementalType.Fighting, ElementalType.Ground } },
        { ElementalType.Ice, new [] { ElementalType.Flying, ElementalType.Ground, ElementalType.Grass, ElementalType.Dragon } },
        { ElementalType.Dragon, new [] { ElementalType.Dragon } },
        { ElementalType.Dark, new [] { ElementalType.Ghost, ElementalType.Psychic } }
    };

    /// <summary>
    /// A map which contains all the <see cref="ElementalType"/> a <see cref="ElementalType"/> is disadvantaged against.
    /// </summary>
    private static readonly Dictionary<ElementalType, IEnumerable<ElementalType>> Disadvantages = new()
    {
        { ElementalType.Normal, new [] { ElementalType.Rock, ElementalType.Steel } },
        { ElementalType.Fighting, new [] { ElementalType.Flying, ElementalType.Poison, ElementalType.Bug, ElementalType.Psychic } },
        { ElementalType.Flying, new [] { ElementalType.Rock, ElementalType.Steel, ElementalType.Electric } },
        { ElementalType.Poison, new [] { ElementalType.Poison, ElementalType.Ground, ElementalType.Rock, ElementalType.Ghost } },
        { ElementalType.Ground, new [] { ElementalType.Bug, ElementalType.Grass } },
        { ElementalType.Rock, new [] { ElementalType.Fighting, ElementalType.Ground, ElementalType.Steel } },
        { ElementalType.Bug, new [] { ElementalType.Fighting, ElementalType.Flying, ElementalType.Poison, ElementalType.Ghost, ElementalType.Steel, ElementalType.Fire } },
        { ElementalType.Ghost, new [] { ElementalType.Dark } },
        { ElementalType.Steel, new [] { ElementalType.Steel, ElementalType.Fire, ElementalType.Water, ElementalType.Electric } },
        { ElementalType.Fire, new [] { ElementalType.Rock, ElementalType.Fire, ElementalType.Water, ElementalType.Dragon } },
        { ElementalType.Water, new [] { ElementalType.Water, ElementalType.Grass, ElementalType.Dragon } },
        { ElementalType.Grass, new [] { ElementalType.Flying, ElementalType.Poison, ElementalType.Bug, ElementalType.Steel, ElementalType.Fire, ElementalType.Grass, ElementalType.Dragon } },
        { ElementalType.Electric, new [] { ElementalType.Grass, ElementalType.Electric, ElementalType.Dragon } },
        { ElementalType.Psychic, new [] { ElementalType.Steel, ElementalType.Psychic } },
        { ElementalType.Ice, new [] { ElementalType.Steel, ElementalType.Fire, ElementalType.Water, ElementalType.Ice } },
        { ElementalType.Dragon, new [] { ElementalType.Steel } },
        { ElementalType.Dark, new [] { ElementalType.Fighting, ElementalType.Dark } }
    };

    /// <summary>
    /// A map which contains all the <see cref="ElementalType"/> a <see cref="ElementalType"/> is nullified against.
    /// </summary>
    private static readonly Dictionary<ElementalType, IEnumerable<ElementalType>> Nulled = new()
    {
        { ElementalType.Normal, new [] { ElementalType.Ghost } },
        { ElementalType.Fighting, new [] { ElementalType.Ghost } },
        { ElementalType.Flying, new List<ElementalType>() },
        { ElementalType.Poison, new [] { ElementalType.Steel } },
        { ElementalType.Ground, new [] { ElementalType.Flying } },
        { ElementalType.Rock, new List<ElementalType>() },
        { ElementalType.Bug, new List<ElementalType>() },
        { ElementalType.Ghost, new [] { ElementalType.Normal } },
        { ElementalType.Steel, new List<ElementalType>() },
        { ElementalType.Fire, new List<ElementalType>() },
        { ElementalType.Water, new List<ElementalType>() },
        { ElementalType.Grass, new List<ElementalType>() },
        { ElementalType.Electric, new [] { ElementalType.Ground } },
        { ElementalType.Psychic, new [] { ElementalType.Dark } },
        { ElementalType.Ice, new List<ElementalType>() },
        { ElementalType.Dragon, new List<ElementalType>() },
        { ElementalType.Dark, new List<ElementalType>() }
    };

    /// <summary>
    /// Get the modifier of type <see cref="a"/> against type <see cref="b"/>.
    /// </summary>
    /// <param name="a">The acting <see cref="ElementalType"/>.</param>
    /// <param name="b">The target <see cref="ElementalType"/>.</param>
    /// <returns>The effectiveness of type <see cref="a"/> against type <see cref="b"/>.</returns>
    private static decimal GetModifier(ElementalType a, ElementalType b)
    {
        if (Advantages[a].Contains(b))
            return Advantaged;

        if (Disadvantages[a].Contains(b))
            return Disadvantaged;

        if (Nulled[a].Contains(b))
            return Nullified;
        
        return Default;
    }
    
    /// <summary>
    /// Get the effectiveness of a <see cref="PokemonMove"/> against a target <see cref="Pokemon"/>.
    /// </summary>
    /// <param name="move">The acting <see cref="PokemonMove"/>.</param>
    /// <param name="defender">The target <see cref="Pokemon"/>.</param>
    /// <returns>The effectiveness of the <see cref="PokemonMove"/> against the <see cref="Pokemon"/>.</returns>
    public static decimal GetEffectiveness(PokemonMove move, Pokemon defender)
    {
        var firstTypeModifier = GetModifier(move.Type, defender.Types[0]);
        if (defender.Types.Count == 1)
            return firstTypeModifier;
        
        var secondTypeModifier = GetModifier(move.Type, defender.Types[1]);
        return firstTypeModifier * secondTypeModifier;
    }
}