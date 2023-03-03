using Game.Battles;
using Game.Experience;
using Game.Moves;
using Game.Natures;
using Game.Stats;   
using Game.Statuses;
using Game.Trainers;
using Game.Types;
using Newtonsoft.Json;

namespace Game.Companions;

/// <summary>
/// A class used to represent a Pokemon in the game.
/// </summary>
[JsonObject(IsReference = true)]
public class Pokemon
{
    public Pokemon(string name, PokemonNature nature, List<ElementalType> types)
    {
        Name = name;
        Nature = nature;
        Types = types;
    }
    
    /// <summary>
    /// Create a new instance of a <see cref="Pokemon"/> by using a <see cref="PokemonBuilder"/>.
    /// </summary>
    /// <param name="name">The name of the <see cref="Pokemon"/>.</param>
    /// <param name="nature">The <see cref="PokemonNature"/> of the <see cref="Pokemon"/>.</param>
    /// <param name="types">All the different <see cref="ElementalType"/> of the <see cref="Pokemon"/>.</param>
    /// <returns>A <see cref="PokemonBuilder"/> used to finish the <see cref="Pokemon"/> creation.</returns>
    public static PokemonBuilder Create(string name, PokemonNature nature, IEnumerable<ElementalType> types)
        => new(new Pokemon(name, nature, types.ToList()));
    
    /// <summary>
    /// The name of the <see cref="Pokemon"/>.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The <see cref="PokemonNature"/> of the <see cref="Pokemon"/>.
    /// </summary>
    public PokemonNature Nature { get; set; }
    
    /// <summary>
    /// The different <see cref="ElementalType"/> of the <see cref="Pokemon"/> representing its combined type.
    /// </summary>
    public List<ElementalType> Types { get; set; }
    
    /// <summary>
    /// The different status conditions that the <see cref="Pokemon"/> is experiencing in a <see cref="Battle"/>.
    /// </summary>
    [JsonIgnore] public List<PokemonStatus> StatusConditions { get; } = new();

    
    /// <summary>
    /// The <see cref="Pokemon"/> to which this <see cref="Pokemon"/> evolves to at a certain level.
    /// </summary>
    public Evolution? Evolution { get; set; }

    /// <summary>
    /// The statistics of the <see cref="Pokemon"/>.
    /// </summary>
    public StatList Statistics { get; set; } = null!;

    /// <summary>
    /// The moves that the <see cref="Pokemon"/> has learned.
    /// </summary>
    public MoveList Moves { get; set; } = null!;

    /// <summary>
    /// The experience that the <see cref="Pokemon"/> has gained through combat (and cheating).
    /// </summary>
    public ExperienceList Experience { get; set; } = null!;

    /// <summary>
    /// The owning <see cref="Trainer"/> of the <see cref="Pokemon"/>.
    /// </summary>
    public Trainer? Owner { get; set; }
    
    /// <summary>
    /// The different stages that each <see cref="Stat"/> can reside in during a <see cref="Battle"/>.
    /// </summary>
    [JsonIgnore] public StageList Stages { get; } = new();

    /// <summary>
    /// The effective stats of the <see cref="Pokemon"/>, after taking into consideration the
    /// <see cref="Stages"/> the <see cref="Pokemon"/> resides in.
    /// </summary>
    [JsonIgnore] public Dictionary<Stat, double> Stats => Stages.CalculateEffectiveStats(Statistics.Value);
    
    /// <summary>
    /// The <see cref="Game.Trainers.Team"/> who controls the <see cref="Pokemon"/>.
    /// </summary>
    [JsonIgnore] public Team? Team { get; set; }
    
    /// <summary>
    /// Whether or not the <see cref="Pokemon"/> is dead, or has whited out.
    /// </summary>
    [JsonIgnore] public bool Whiteout => Stats[Stat.Health] <= 0;

    /// <summary>
    /// Heal the <see cref="Pokemon"/> to full health.
    /// </summary>
    /// <returns>The amount of health that has been restored.</returns>
    public double FullHeal()
    {
        var original = Stats[Stat.Health];
        Statistics.Reset(Stat.Health);

        return Stats[Stat.Health] - original;
    }

    /// <summary>
    /// Heal the <see cref="Pokemon"/> by a certain amount.
    /// </summary>
    /// <param name="amount">The amount by which the <see cref="Pokemon"/> should be healed.</param>
    public void Heal(double amount)
        => Statistics.Value[Stat.Health] += amount;

    /// <summary>
    /// Damage the <see cref="Pokemon"/> by a certain amount.
    /// </summary>
    /// <param name="amount">The amount by which the <see cref="Pokemon"/> should be damaged.</param>
    public void Damage(double amount)
        => Statistics.Value[Stat.Health] -= amount;

    /// <summary>
    /// Reset all the <see cref="Stages"/> and <see cref="StatusConditions"/> and others effects.
    /// </summary>
    public void Reset()
    {
        Stages.Reset();
        StatusConditions.Clear();

        foreach (var move in Moves.Value)
            move.PP.Current = move.PP.Maximum;
    }

    public override string ToString() => Name;

    /// <summary>
    /// Default <see cref="GetHashCode"/> implementation would create a circular dependency between <see cref="Pokemon"/> and <see cref="Team"/>.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
        => Name.GetHashCode() + Statistics.IVs.Select(s => s.Value).Sum();
}