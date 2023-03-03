using Game.Companions;
using Game.Moves;

namespace Game.Content;

/// <summary>
/// A content class used to list all the available <see cref="LearnSet"/> that <see cref="Pokemon"/> can use.
/// </summary>
public class LearnsetList
{
    public static LearnSet Bulbasaur() => new()
    {
        Value = new Dictionary<int, List<PokemonMove>>
        {
            { 1, new (){ PokemonMoveList.Tackle() } },
            { 4, new (){ PokemonMoveList.Growl() } },
            { 10, new (){ PokemonMoveList.VineWhip() } },
            { 15, new (){ PokemonMoveList.PoisonPowder() } },
            { 20, new (){ PokemonMoveList.RazorLeaf() } },
            { 25, new (){ PokemonMoveList.SweetScent() } },
            { 32, new (){ PokemonMoveList.Growth() } },
            { 39, new (){ PokemonMoveList.Synthesis() } },
            { 45, new (){ PokemonMoveList.SolarBeam() } }
        }
    };

    public static LearnSet Ivysaur() => new()
    {
        Value = new Dictionary<int, List<PokemonMove>>
        {
            { 1, new (){ PokemonMoveList.Tackle() } },
            { 4, new (){ PokemonMoveList.Growl() } },
            { 10, new (){ PokemonMoveList.VineWhip() } },
            { 15, new (){ PokemonMoveList.PoisonPowder() } },
            { 22, new (){ PokemonMoveList.RazorLeaf() } },
            { 29, new (){ PokemonMoveList.SweetScent() } },
            { 38, new (){ PokemonMoveList.Growth() } },
            { 47, new (){ PokemonMoveList.Synthesis() } },
            { 56, new (){ PokemonMoveList.SolarBeam() } }
        }
    };

    public static LearnSet Venusaur() => new()
    {
        Value = new Dictionary<int, List<PokemonMove>>
        {
            { 1, new (){ PokemonMoveList.Tackle() } },
            { 4, new (){ PokemonMoveList.Growl() } },
            { 10, new (){ PokemonMoveList.VineWhip() } },
            { 15, new (){ PokemonMoveList.PoisonPowder() } },
            { 22, new (){ PokemonMoveList.RazorLeaf() } },
            { 29, new (){ PokemonMoveList.SweetScent() } },
            { 41, new (){ PokemonMoveList.Growth() } },
            { 53, new (){ PokemonMoveList.Synthesis() } },
            { 65, new (){ PokemonMoveList.SolarBeam() } }
        }
    };

    public static LearnSet Charmander() => new()
    {
        Value = new Dictionary<int, List<PokemonMove>>
        {
            { 1, new (){ PokemonMoveList.Scratch() } },
            { 2, new (){ PokemonMoveList.Growl() } },
            { 7, new (){ PokemonMoveList.Ember() } },
            { 13, new (){ PokemonMoveList.MetalClaw() } },
            { 19, new (){ PokemonMoveList.Smokescreen() } },
            { 25, new (){ PokemonMoveList.ScaryFace() } },
            { 31, new (){ PokemonMoveList.Flamethrower() } },
            { 37, new (){ PokemonMoveList.Slash() } },
            { 43, new (){ PokemonMoveList.DragonRage() } },
            { 49, new (){ PokemonMoveList.FireSpin() } }
        }
    };

    public static LearnSet Charmeleon() => new()
    {
        Value = new Dictionary<int, List<PokemonMove>>
        {
            { 1, new (){ PokemonMoveList.Scratch() } },
            { 2, new (){ PokemonMoveList.Growl() } },
            { 7, new (){ PokemonMoveList.Ember() } },
            { 13, new (){ PokemonMoveList.MetalClaw() } },
            { 20, new (){ PokemonMoveList.Smokescreen() } },
            { 27, new (){ PokemonMoveList.ScaryFace() } },
            { 34, new (){ PokemonMoveList.Flamethrower() } },
            { 41, new (){ PokemonMoveList.Slash() } },
            { 48, new (){ PokemonMoveList.DragonRage() } },
            { 55, new (){ PokemonMoveList.FireSpin() } }
        }
    };

    public static LearnSet Charizard() => new()
    {
        Value = new Dictionary<int, List<PokemonMove>>
        {
            { 1, new (){ PokemonMoveList.Scratch() } },
            { 2, new (){ PokemonMoveList.Growl() } },
            { 4, new (){ PokemonMoveList.HeatWave() } },
            { 7, new (){ PokemonMoveList.Ember() } },
            { 13, new (){ PokemonMoveList.MetalClaw() } },
            { 20, new (){ PokemonMoveList.Smokescreen() } },
            { 27, new (){ PokemonMoveList.ScaryFace() } },
            { 34, new (){ PokemonMoveList.Flamethrower() } },
            { 36, new (){ PokemonMoveList.WingAttack() } },
            { 40, new (){ PokemonMoveList.SkullBash() } },
            { 44, new (){ PokemonMoveList.Slash() } },
            { 54, new (){ PokemonMoveList.DragonRage() } },
            { 64, new (){ PokemonMoveList.FireSpin() } }
        }
    };

    public static LearnSet Squirtle() => new()
    {
        Value = new Dictionary<int, List<PokemonMove>>
        {
            { 1, new (){ PokemonMoveList.Tackle(), PokemonMoveList.TailWhip() } },
            { 8, new (){ PokemonMoveList.Bubble() } },
            { 15, new (){ PokemonMoveList.WaterGun() } },
            { 22, new (){ PokemonMoveList.Bite() } },
            { 28, new (){ PokemonMoveList.Withdraw() } },
            { 35, new (){ PokemonMoveList.SkullBash() } },
            { 42, new (){ PokemonMoveList.HydroPump() } }
        }
    };
    
    public static LearnSet Wartortle() => new()
    {
        Value = new Dictionary<int, List<PokemonMove>>
        {
            { 1, new (){ PokemonMoveList.Tackle(), PokemonMoveList.TailWhip() } },
            { 8, new (){ PokemonMoveList.Bubble() } },
            { 15, new (){ PokemonMoveList.WaterGun() } },
            { 24, new (){ PokemonMoveList.Bite() } },
            { 31, new (){ PokemonMoveList.Withdraw() } },
            { 39, new (){ PokemonMoveList.SkullBash() } },
            { 47, new (){ PokemonMoveList.HydroPump() } }
        }
    };
    
    public static LearnSet Blastoise() => new()
    {
        Value = new Dictionary<int, List<PokemonMove>>
        {
            { 1, new (){ PokemonMoveList.Tackle(), PokemonMoveList.TailWhip() } },
            { 8, new (){ PokemonMoveList.Bubble() } },
            { 15, new (){ PokemonMoveList.WaterGun() } },
            { 24, new (){ PokemonMoveList.Bite() } },
            { 31, new (){ PokemonMoveList.Withdraw() } },
            { 42, new (){ PokemonMoveList.SkullBash() } },
            { 52, new (){ PokemonMoveList.HydroPump() } }
        }
    };
}