using Game.Companions;
using Newtonsoft.Json;

namespace Game.Experience;

/// <summary>
/// A class used to store experience information about a <see cref="Pokemon"/>.
/// </summary>
[JsonObject(IsReference = true)]
public record ExperienceList
{
    /// <summary>
    /// The total value of the experience of the <see cref="Pokemon"/>.
    /// </summary>
    public double Value { get; set; }
    
    /// <summary>
    /// The experience yield other <see cref="Pokemon"/> receive when killing this <see cref="Pokemon"/>.
    /// </summary>
    public int Yield { get; set; }
    
    /// <summary>
    /// The current level of the <see cref="Pokemon"/>.
    /// </summary>
    [JsonIgnore] public int Level => Experiences.ToLevel(Value);

    /// <summary>
    /// The list of <see cref="ILevelEffect"/> that should be applied after levelling up.
    /// </summary>
    public List<ILevelEffect> Effects { get; set; }

    /// <summary>
    /// Add experience to the current <see cref="Pokemon"/>.
    /// </summary>
    /// <param name="actor">The <see cref="Pokemon"/> which the experience should be added to.</param>
    /// <param name="value">The amount of experience that should be added.</param>
    public void AddExperience(Pokemon actor, double value)
    {
        var oldLevel = Level;
        Value += value;

        // No new level, exit
        if (Level <= oldLevel) 
            return;
        
        // Execute effects
        foreach (var effect in Effects)
            effect.Execute(actor, oldLevel, Level);
    }

    /// <summary>
    /// Add experience to the current <see cref="Pokemon"/> by the yield of another <see cref="Pokemon"/>.
    /// </summary>
    /// <param name="actor">The <see cref="Pokemon"/> which the experience should be added to.</param>
    /// <param name="opponent">The <see cref="Pokemon"/> which was killed by this <see cref="Pokemon"/>.</param>
    /// <returns>The experience yield that the <see cref="Pokemon"/> gained.</returns>
    public double Kill(Pokemon actor, Pokemon opponent)
    {
        // With e = 1 (no items), a = 1.5 (want quick experience gain) and t = 1 (trading not relevant)
        var experienceYield = opponent.Experience.Yield * opponent.Experience.Level / 7.0 * (1.0 / 8.0) * 1.5;
        AddExperience(actor, experienceYield);
        
        return experienceYield;
    }
}