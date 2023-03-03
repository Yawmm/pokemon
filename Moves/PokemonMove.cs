using Game.Battles;
using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Moves.Damage;
using Game.Stats;
using Game.Statuses;
using Game.Types;
using Newtonsoft.Json;

namespace Game.Moves;

/// <summary>
/// A class used to represent a move a <see cref="Pokemon"/> has and which can be executed during a <see cref="Battle"/>.
/// </summary>
[JsonObject]
public class PokemonMove
{
    [JsonConstructor]
    public PokemonMove() { }
    
    public PokemonMove(string name, string description, ElementalType type, MoveCategory category)
    {
        Name = name;
        Description = description;
        Type = type;
        Category = category;
    }

    /// <summary>
    /// Create a new instance of a <see cref="PokemonMove"/> by using a <see cref="PokemonMoveBuilder"/>.
    /// </summary>
    /// <param name="name">The name of the <see cref="PokemonMove"/>.</param>
    /// <param name="description">The description of the <see cref="PokemonMove"/>.</param>
    /// <param name="type">The <see cref="ElementalType"/> of the <see cref="PokemonMove"/>.</param>
    /// <param name="category">The <see cref="MoveCategory"/> of the <see cref="PokemonMove"/>.</param>
    /// <returns>A <see cref="PokemonMoveBuilder"/> used to finish the <see cref="PokemonMove"/> creation.</returns>
    public static PokemonMoveBuilder Create(string name, string description, ElementalType type, MoveCategory category)
        => new(new PokemonMove(name, description, type, category));
    
    /// <summary>
    /// The name of the <see cref="PokemonMove"/>.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// The description of the <see cref="PokemonMove"/>.
    /// </summary>
    public string Description { get; init; } = null!;
    
    /// <summary>
    /// The <see cref="ElementalType"/> of the <see cref="PokemonMove"/>.
    /// </summary>
    public ElementalType Type { get; init; }

    /// <summary>
    /// The <see cref="MoveCategory"/> of the <see cref="PokemonMove"/>.
    /// </summary>
    public MoveCategory Category { get; init; }

    /// <summary>
    /// The power of the <see cref="PokemonMove"/>.
    /// </summary>
    public int? Power { get; set; }
    
    /// <summary>
    /// The accuracy of the <see cref="PokemonMove"/>.
    /// </summary>
    public int? Accuracy { get; set; }
    
    /// <summary>
    /// The <see cref="PowerPoint"/> value of the <see cref="PokemonMove"/>.
    /// </summary>
    public PowerPoint PP { get; set; } = new(10);
    
    /// <summary>
    /// The execution priority of the <see cref="PokemonMove"/>. 
    /// </summary>
    public int? Priority { get; set; }

    /// <summary>
    /// The list of <see cref="PokemonMoveStage"/> which need to be executed before the <see cref="PokemonMove"/> is deemed finished.
    /// </summary>
    public List<PokemonMoveStage> Stages { get; set; } = new();

    /// <summary>
    /// Execute the next <see cref="PokemonMoveStage"/> of the <see cref="PokemonMove"/>.
    /// </summary>
    /// <param name="stage">The <see cref="PokemonMoveStage"/> which should be executed.</param>
    /// <param name="moveTurn">The context of the current executing <see cref="MoveTurn"/>.</param>
    /// <returns>The list of <see cref="Event"/> which occurred during the execution of the <see cref="PokemonMoveStage"/>.</returns>
    public IEnumerable<Event> Execute(PokemonMoveStage stage, MoveTurn moveTurn)
        => stage.Execute(this, moveTurn);

    /// <summary>
    /// Calculate the <see cref="DamageResult"/> the <see cref="PokemonMove"/> would do against an opposing <see cref="Pokemon"/>, by the acting <see cref="Pokemon"/>.
    /// </summary>
    /// <param name="move">The <see cref="PokemonMove"/> which is being executed.</param>
    /// <param name="attacker">The acting <see cref="Pokemon"/>.</param>
    /// <param name="defender">The opposing <see cref="Pokemon"/>.</param>
    /// <param name="modifiers">The range of <see cref="IDamageModifier"/> which should be applied to the final <see cref="DamageResult"/>.</param>
    /// <returns>The calculated <see cref="DamageResult"/>.</returns>
    public DamageResult CalculateDamage(
        PokemonMove move,
        Pokemon attacker,
        Pokemon defender,
        IEnumerable<IDamageModifier> modifiers
        )
    {
        var basic = (2 * attacker.Experience.Level / 5 + 2) * Power;

        var attack = attacker.Stats[Category == MoveCategory.Special ? Stat.SpecialAttack : Stat.Attack];
        var defense = defender.Stats[Category == MoveCategory.Special ? Stat.SpecialDefense : Stat.Defense];

        var damage = basic * (attack / defense) / 50;
        var burn = attacker.StatusConditions.Contains(PokemonStatus.Burn) && Category != MoveCategory.Special ? 0.5 : 1;

        var result = new DamageResult { Value = damage * burn + 2 };

        foreach (var modifier in modifiers)
            result.Value *= modifier.Apply(result, move, attacker, defender);

        return result;
    }

    public override string ToString() => Name;
}