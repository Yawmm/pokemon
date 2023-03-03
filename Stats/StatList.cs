using Game.Companions;
using Game.Natures;
using Newtonsoft.Json;

namespace Game.Stats;

/// <summary>
/// A class used to store the range of stats a <see cref="Pokemon"/> has.
/// </summary>
[JsonObject(IsReference = true)]
public record StatList
{
    [JsonConstructor]
    public StatList() { }
    
    public StatList(
        Pokemon pokemon,
        Dictionary<Stat, int> basis, 
        Dictionary<Stat, int> yield
    )
    {
        Base = basis;
        Yield = yield;
        
        // Reset EVs
        if (EVs.Count <= 0)
            EVs = Enum.GetValues<Stat>()
                .ToDictionary(s => s, _ => 0);
        
        // Reset IVs
        if (IVs.Count <= 0)
            IVs = Enum.GetValues<Stat>()
                .ToDictionary(s => s, _ => Random.Shared.Next(0, 31));

        Recalculate(pokemon);
    }
    
    /// <summary>
    /// The base stats of a <see cref="Pokemon"/>.
    /// </summary>
    public Dictionary<Stat, int> Base { get; set; } = null!;

    /// <summary>
    /// The current stats of a <see cref="Pokemon"/>.
    /// </summary>
    public Dictionary<Stat, double> Value { get; private set; } = new();
    
    /// <summary>
    /// The maximum stats a <see cref="Pokemon"/> can have.
    /// </summary>
    public Dictionary<Stat, double> Maximum { get; private set; } = new();
    
    /// <summary>
    /// The yield of stats another <see cref="Pokemon"/> receives when killing this <see cref="Pokemon"/>.
    /// </summary>
    public Dictionary<Stat, int> Yield { get; set; } = new();
    
    /// <summary>
    /// The randomly generated individual strengths (IV) of the <see cref="Pokemon"/>.
    /// </summary>
    public Dictionary<Stat, int> IVs { get; set; } = new();
    
    /// <summary>
    /// The effort values (EV) the <see cref="Pokemon"/> has from killing other <see cref="Pokemon"/>.
    /// </summary>
    public Dictionary<Stat, int> EVs { get; set; } = new();

    /// <summary>
    /// Reset the current given stat to the maximum stats of the <see cref="Pokemon"/>.
    /// </summary>
    /// <param name="stat">The stat which should be reset to its maximum.</param>
    public void Reset(Stat stat)
        => Value[stat] = Maximum[stat];

    /// <summary>
    /// Recalculate all the stats of a <see cref="Pokemon"/>, its maximum stats and its current stats.
    /// </summary>
    /// <param name="pokemon">The <see cref="Pokemon"/> who's stats should be recalculated.</param>
    public void Recalculate(Pokemon pokemon)
    {
        Maximum = CalculateStats(pokemon.Nature, pokemon.Experience.Level);
        Value = CalculateStats(pokemon.Nature, pokemon.Experience.Level);
    }

    /// <summary>
    /// Calculate a range of stats by the given <see cref="PokemonNature"/> and level, and the base, IV and EV stats.
    /// </summary>
    /// <param name="nature">The <see cref="PokemonNature"/> which should be used in the calculations.</param>
    /// <param name="level">The level which should be used in the calculations.</param>
    /// <returns>The final calculated stats of the <see cref="Pokemon"/>.</returns>
    private Dictionary<Stat, double> CalculateStats(PokemonNature nature, int level)
    {
        // Health uses a different formula for calculating its value, so we set it here and not later
        var stats = new Dictionary<Stat, double>
        {
            {
                Stat.Health,
                GetHealthStat(
                    level, 
                    Base[Stat.Health],
                    IVs[Stat.Health], 
                    EVs[Stat.Health]
                )
            }
        };
    
        // Set all the stats except health
        foreach (var (stat, value) in IVs.Where(s => s.Key != Stat.Health))
            stats.Add(
                stat, 
                GetNormalStat(
                    level, 
                    value, 
                    IVs[stat], 
                    EVs[stat], 
                    GetNatureStat(nature, stat)
                )
            );
    
        return stats;
    }
    
    /// <summary>
    /// Calculate the health stat of a <see cref="Pokemon"/>.
    /// </summary>
    /// <param name="level">The level of the <see cref="Pokemon"/>.</param>
    /// <param name="baseStat">The base value of the given <see cref="Stat"/>.</param>
    /// <param name="ivStat">The IV value of the given <see cref="Stat"/>.</param>
    /// <param name="evStat">The EV value of the given <see cref="Stat"/>.</param>
    /// <returns>The final calculated health value.</returns>
    private double GetHealthStat(
        int level,
        int baseStat,
        int ivStat,
        int evStat
    ) => Math.Floor(0.01 * (2 * baseStat + ivStat + Math.Floor(0.25 * evStat)) * level) + level + 10;
    
    /// <summary>
    /// Calculate any <see cref="Stat"/>, except health, of a <see cref="Pokemon"/>.
    /// </summary>
    /// <param name="level">The level of the <see cref="Pokemon"/>.</param>
    /// <param name="baseStat">The base value of the given <see cref="Stat"/>.</param>
    /// <param name="ivStat">The IV value of the given <see cref="Stat"/>.</param>
    /// <param name="evStat">The EV value of the given <see cref="Stat"/>.</param>
    /// <param name="natureStat">The nature value of the given <see cref="Stat"/>.</param>
    /// <returns>The final calculated value for a <see cref="Stat"/>.</returns>
    private double GetNormalStat(
        int level,
        int baseStat, 
        int ivStat, 
        int evStat, 
        double natureStat
    ) => (Math.Floor(0.01 * (2 * baseStat + ivStat + Math.Floor(0.25 * evStat)) * level) + 5) * natureStat;
    
    /// <summary>
    /// Get the modifier of the <see cref="PokemonNature"/> of the <see cref="Pokemon"/> for the given stat.
    /// </summary>
    /// <param name="nature">The <see cref="PokemonBuilder"/> of the <see cref="Pokemon"/>.</param>
    /// <param name="stat">The stat from which the <see cref="PokemonNature"/> modifier should be calculated.</param>
    /// <returns>The <see cref="PokemonNature"/> modifier.</returns>
    private double GetNatureStat(
        PokemonNature nature, 
        Stat stat
    ) => nature.Increased == stat ? 1.10 : nature.Decreased == stat ? 0.90 : 1;
}