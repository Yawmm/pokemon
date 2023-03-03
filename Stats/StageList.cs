using Game.Companions;

namespace Game.Stats;

/// <summary>
/// A class used to store the range of stat stages a <see cref="Pokemon"/> has during a battle.
/// </summary>
public class StageList
{
    public StageList() { Reset(); }
    
    /// <summary>
    /// The range of stat stages the <see cref="Pokemon"/> has.
    /// </summary>
    public Dictionary<Stat, int> Value { get; private set; } = new();
    
    /// <summary>
    /// Check whether or not any stat stages have been changed.
    /// </summary>
    /// <returns>Whether or not any stat stages have been changed.</returns>
    public bool IsActive()
        => Value.Any(s => s.Value != 0);
    
    /// <summary>
    /// Change a stat stage by a given amount.
    /// </summary>
    /// <param name="stat">The stage of the <see cref="Stat"/> which needs to be changed.</param>
    /// <param name="amount">The amount by which the stage should change.</param>
    public void Change(Stat stat, int amount)
        => Value[stat] = Math.Clamp(Value[stat] + amount, -6, 6);
    
    /// <summary>
    /// Reset the entire range of stat stages to zero.
    /// </summary>
    public void Reset()
        => Value = Enum.GetValues<Stat>()
            .ToDictionary(s => s, _ => 0);

    /// <summary>
    /// Modify a given list of stats by the stat stages that have been modified.
    /// </summary>
    /// <param name="stats">The base stats used for the calculation.</param>
    /// <returns>The stats which have been modified by the modified stat stages.</returns>
    public Dictionary<Stat, double> CalculateEffectiveStats(Dictionary<Stat, double> stats)
    {
        var result = new Dictionary<Stat, double>();
        foreach (var (stat, value) in stats)
        {
            var stage = Value[stat];
            var multiplier = stage > 0
                ? (2.0 + stage) / 2.0
                : 2.0 / (2.0 - stage);

            var effectiveValue = value * multiplier;
            result.Add(stat, effectiveValue);
        }

        return result;
    }
}