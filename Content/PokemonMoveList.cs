using Game.Moves;
using Game.Moves.Cleanups;
using Game.Moves.Damage.Modifiers;
using Game.Moves.Effects;
using Game.Moves.Wrappers;
using Game.Stats;
using Game.Statuses;
using Game.Types;

namespace Game.Content;

/// <summary>
/// A content class used to list all the available <see cref="PokemonMove"/>.
/// </summary>
public static class PokemonMoveList
{
    #region Bulbasaur moves

    public static PokemonMove Tackle() => PokemonMove
        .Create(
            name: "Tackle",
            description: "A physical attack in which the user charges and slams into the target with its whole body.",
            type: ElementalType.Normal,
            category: MoveCategory.Physical
        )
        .WithPowerPoint(35)
        .WithAccuracy(100)
        .WithPower(40)
        .Stage()
            .WithHitEffects()
            .WithHitCleanups()
            .Add()
        .Build();

    public static PokemonMove Growl() => PokemonMove
        .Create(
            name: "Growl",
            description: "The user growls in an endearing way, making opposing Pokémon less wary. This lowers their Attack stats.",
            type: ElementalType.Normal,
            category: MoveCategory.Status
        )
        .WithPowerPoint(40)
        .WithAccuracy(100)
        .Stage()
            .WithEffects(new [] { new StatStageChangeEffect(Stat.Attack, -1, false) })
            .Add()
        .Build();

    public static PokemonMove VineWhip() => PokemonMove
        .Create(
            name: "Vine Whip",
            description: "The target is struck with slender, whiplike vines to inflict damage.",
            type: ElementalType.Grass,
            category: MoveCategory.Special
        )
        .WithPowerPoint(25)
        .WithAccuracy(100)
        .WithPower(35)
        .Stage()
            .WithHitEffects()
            .WithHitCleanups()
            .Add()
        .Build();

    public static PokemonMove PoisonPowder() => PokemonMove
        .Create(
            name: "Poison Powder",
            description: "The user scatters a cloud of poisonous dust on the target. This may also poison the target.",
            type: ElementalType.Poison,
            category: MoveCategory.Status
        )
        .WithPowerPoint(35)
        .WithAccuracy(75)
        .WithPriority(1)
        .Stage()
            .WithEffects(new [] { new StatusConditionEffect(PokemonStatus.Poison, Colors.Poison) })
            .Add()
        .Build();

    public static PokemonMove RazorLeaf() => PokemonMove
        .Create(
            name: "Razor Leaf",
            description: "Sharp-edged leaves are launched to slash at the opposing Pokémon. Critical hits land more easily.",
            type: ElementalType.Grass,
            category: MoveCategory.Physical
        )
        .WithPowerPoint(25)
        .WithAccuracy(95)
        .WithPower(55)
        .Stage()
            .WithHitEffects()
            .WithHitCleanups()
            .ReplaceDamageModifier(new CritModifier(High: true))
            .Add()
        .Build();

    public static PokemonMove SweetScent() => PokemonMove
        .Create(
            name: "Sweet Scent",
            description: "A sweet scent that harshly lowers opposing Pokémon's evasiveness.",
            type: ElementalType.Normal,
            category: MoveCategory.Status
        )
        .WithPowerPoint(20)
        .WithAccuracy(100)
        .WithPriority(1)
        .Stage()
            .WithEffects(new [] { new StatStageChangeEffect(Stat.Evasion, -2, false) })
            .Add()
        .Build();

    public static PokemonMove Growth() => PokemonMove
        .Create(
            name: "Growth",
            description: "The user's body grows all at once, raising the Attack and Sp. Atk stats.",
            type: ElementalType.Normal,
            category: MoveCategory.Status
        )
        .WithPowerPoint(20)
        .WithPriority(1)
        .Stage()
            .WithEffects(new [] { new StatStageChangeEffect(Stat.Attack, 1), new StatStageChangeEffect(Stat.SpecialAttack, 1) })
            .Add()
        .Build();

    public static PokemonMove Synthesis() => PokemonMove
        .Create(
            name: "Synthesis",
            description: "The user restores its own HP.",
            type: ElementalType.Grass,
            category: MoveCategory.Status
        )
        .WithPowerPoint(5)
        .Stage()
            .WithEffects(new [] { new HealEffect(1.0 / 4.0, true) })
            .Add()
        .Build();
    
    public static PokemonMove SolarBeam() => PokemonMove
        .Create(
            name: "Solar Beam",
            description: "A two-turn attack. The user gathers light, then blasts a bundled beam on the next turn.",
            type: ElementalType.Grass,
            category: MoveCategory.Special
        )
        .WithPowerPoint(10)
        .WithAccuracy(100)
        .WithPower(120)
        .Stage()
            .WithEffects(new List<IMoveEffect>())
            .WithCleanups(new List<IMoveCleanup>())
            .Add()
        .Stage()
            .WithHitEffects()
            .WithHitCleanups()
            .Add()
        .Build();

    #endregion

    #region Charmander moves

    public static PokemonMove Scratch() => PokemonMove
        .Create(
            name: "Scratch",
            description: "Hard, pointed, sharp claws rake the target to inflict damage.",
            type: ElementalType.Normal,
            category: MoveCategory.Physical
        )
        .WithPowerPoint(35)
        .WithAccuracy(100)
        .WithPower(40)
        .Stage()
            .WithHitEffects()
            .WithHitCleanups()
            .Add()
        .Build();

    public static PokemonMove HeatWave() => PokemonMove
        .Create(
            name: "Heat Wave",
            description: "The user attacks by exhaling hot breath on the opposing Pokémon. This may also leave those Pokémon with a burn.",
            type: ElementalType.Fire,
            category: MoveCategory.Special
        )
        .WithPowerPoint(10)
        .WithAccuracy(90)
        .WithPower(100)
        .Stage()
            .WithHitEffects()
            .AddEffect(new ChanceWrapper(new StatusConditionEffect(PokemonStatus.Burn, Colors.Burn), 10))
            .WithHitCleanups()
            .Add()
        .Build();

    public static PokemonMove Ember() => PokemonMove
        .Create(
            name: "Ember",
            description: "The target is attacked with small flames. This may also leave the target with a burn.",
            type: ElementalType.Fire,
            category: MoveCategory.Special
        )
        .WithPowerPoint(25)
        .WithAccuracy(100)
        .WithPower(40)
        .Stage()
            .WithHitEffects()
            .AddEffect(new ChanceWrapper(new StatusConditionEffect(PokemonStatus.Burn, Colors.Burn), 10))
            .WithHitCleanups()
            .Add()
        .Build();

    public static PokemonMove Smokescreen() => PokemonMove
        .Create(
            name: "Smokescreen",
            description: "The user releases an obscuring cloud of smoke or ink. This lowers the target's accuracy.",
            type: ElementalType.Normal,
            category: MoveCategory.Status
        )
        .WithPowerPoint(20)
        .WithAccuracy(100)
        .WithPriority(2)
        .Stage()
            .WithEffects(new [] { new StatStageChangeEffect(Stat.Accuracy, 1) })
            .Add()
        .Build();

    public static PokemonMove MetalClaw() => PokemonMove
        .Create(
            name: "Metal Claw",
            description: "The target is raked with steel claws. This may also raise the user's Attack stat.",
            type: ElementalType.Steel,
            category: MoveCategory.Physical
        )
        .WithPowerPoint(35)
        .WithAccuracy(95)
        .WithPower(50)
        .Stage()
            .WithHitEffects()
            .AddEffect(new ChanceWrapper(new StatStageChangeEffect(Stat.Attack, 1), 10))
            .WithHitCleanups()
            .Add()
        .Build();

    public static PokemonMove ScaryFace() => PokemonMove
        .Create(
            name: "Scary Face",
            description: "The user frightens the target with a scary face to harshly lower its Speed stat.",
            type: ElementalType.Normal,
            category: MoveCategory.Status
        )
        .WithPowerPoint(10)
        .WithAccuracy(100)
        .WithPriority(1)
        .Stage()
            .WithEffects(new []{ new StatStageChangeEffect(Stat.Speed, -2, false) })
            .Add()
        .Build();

    public static PokemonMove Flamethrower() => PokemonMove
        .Create(
            name: "Flamethrower",
            description: "The target is scorched with an intense blast of fire. This may also leave the target with a burn.",
            type: ElementalType.Fire,
            category: MoveCategory.Special
        )
        .WithPowerPoint(15)
        .WithAccuracy(100)
        .WithPower(90)
        .Stage()
            .WithHitEffects()
            .AddEffect(new ChanceWrapper(new StatusConditionEffect(PokemonStatus.Burn, Colors.Burn), 10))
            .WithHitCleanups()
            .Add()
        .Build();

    public static PokemonMove WingAttack() => PokemonMove
        .Create(
            name: "Wing Attack",
            description: "The target is struck with large, imposing wings spread wide to inflict damage",
            type: ElementalType.Flying,
            category: MoveCategory.Physical
        )
        .WithPowerPoint(35)
        .WithAccuracy(100)
        .WithPower(60)
        .Stage()
            .WithHitEffects()
            .WithHitCleanups()
            .Add()
        .Build();

    public static PokemonMove Slash() => PokemonMove
        .Create(
            name: "Slash",
            description: "The target is attacked with a slash of claws or blades. Critical hits land more easily.",
            type: ElementalType.Normal,
            category: MoveCategory.Physical
        )
        .WithPowerPoint(20)
        .WithAccuracy(100)
        .WithPower(70)
        .Stage()
            .WithHitEffects()
            .WithHitCleanups()
            .ReplaceDamageModifier(new CritModifier(High: true))
            .Add()
        .Build();

    public static PokemonMove DragonRage() => PokemonMove
        .Create(
            name: "Dragon Rage",
            description: "This attack hits the target with a shock wave of pure rage. This attack always inflicts 40 HP damage.",
            type: ElementalType.Dragon,
            category: MoveCategory.Special
        )
        .WithPowerPoint(10)
        .WithAccuracy(100)
        .Stage()
            .WithEffects(new [] { new DamageEffect(Damage: 40) })
            .WithHitCleanups()
            .Add()
        .Build();
    
    public static PokemonMove FireSpin() => PokemonMove
        .Create(
            name: "Fire Spin",
            description: "The target becomes trapped within a fierce vortex of fire that rages for four to five turns.",
            type: ElementalType.Fire,
            category: MoveCategory.Special
        )
        .WithPowerPoint(10)
        .WithAccuracy(100)
        .WithPower(35)
        .Stage()
            .WithHitEffects()
            .WithCleanups(new [] { new KillCleanup() })
            .Add(Random.Shared.Next(3, 5))
        .Stage()
            .WithHitEffects()
            .WithHitCleanups()
            .Add()
        .Build();

    #endregion

    #region Squirte moves

    public static PokemonMove TailWhip() => PokemonMove
        .Create(
            name: "Tail Whip",
            description: "The user wags its tail cutely, making opposing Pokémon less wary and lowering their Defense stat.",
            type: ElementalType.Normal,
            category: MoveCategory.Status
        )
        .WithPowerPoint(30)
        .WithAccuracy(100)
        .Stage()
            .WithEffects(new [] { new StatStageChangeEffect(Stat.Defense, -1, false) })
            .Add()
        .Build();

    public static PokemonMove Bubble() => PokemonMove
        .Create(
            name: "Bubble",
            description: "A spray of countless bubbles is jetted at the opposing Pokémon. This may also lower their Speed stats.",
            type: ElementalType.Water,
            category: MoveCategory.Special
        )
        .WithPowerPoint(30)
        .WithAccuracy(100)
        .WithPower(40)
        .Stage()
            .WithHitEffects()
            .AddEffect(new ChanceWrapper(new StatStageChangeEffect(Stat.Speed, -1, false), 10))
            .WithHitCleanups()
            .Add()
        .Build();

    public static PokemonMove Withdraw() => PokemonMove
        .Create(
            name: "Withdraw",
            description: "The user withdraws its body into its hard shell, raising its Defense stat.",
            type: ElementalType.Water,
            category: MoveCategory.Status
        )
        .WithPowerPoint(40)
        .WithPriority(3)
        .Stage()
            .WithEffects(new [] { new StatStageChangeEffect(Stat.Defense, 1) })
            .Add()
        .Build();

    public static PokemonMove WaterGun() => PokemonMove
        .Create(
            name: "Water Gun",
            description: "The target is blasted with a forceful shot of water.",
            type: ElementalType.Water,
            category: MoveCategory.Special
        )
        .WithPowerPoint(25)
        .WithAccuracy(100)
        .WithPower(40)
        .Stage()
            .WithHitEffects()
            .WithHitCleanups()
            .Add()
        .Build();

    public static PokemonMove Bite() => PokemonMove
        .Create(
            name: "Bite",
            description: "The target is bitten with viciously sharp fangs. This may also make the target flinch.",
            type: ElementalType.Dark,
            category: MoveCategory.Physical
        )
        .WithPowerPoint(25)
        .WithAccuracy(100)
        .WithPower(60)
        .Stage()
            .WithHitEffects()
            .AddEffect(new ChanceWrapper(new StatusConditionEffect(PokemonStatus.Flinch, Colors.Flinch), 30))
            .WithHitCleanups()
            .Add()
        .Build();

    public static PokemonMove SkullBash() => PokemonMove
        .Create(
            name: "Skull Bash",
            description: "The user tucks in its head to raise its Defense in the first turn, then rams the target on the next turn.",
            type: ElementalType.Normal,
            category: MoveCategory.Physical
        )
        .WithPowerPoint(10)
        .WithAccuracy(100)
        .WithPower(130)
        .Stage()
            .WithEffects(new [] { new StatStageChangeEffect(Stat.Defense, 1) })
            .Add() 
        .Stage()
            .WithHitEffects()
            .WithHitCleanups()
            .Add()
        .Build();

    public static PokemonMove HydroPump() => PokemonMove
        .Create(
            name: "Hydro Pump",
            description: "The target is blasted by a huge volume of water launched under great pressure.",
            type: ElementalType.Water,
            category: MoveCategory.Special
        )
        .WithPowerPoint(5)
        .WithAccuracy(80)
        .WithPower(120)
        .Stage()
            .WithHitEffects()
            .WithHitCleanups()
            .Add()
        .Build();

    #endregion
}