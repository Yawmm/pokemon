using Game.Battles;
using Newtonsoft.Json;

namespace Game.Companions;

/// <summary>
/// A class representing the Power Points (PP) of a <see cref="Pokemon"/>.
/// </summary>
[JsonObject]
public class PowerPoint
{
    public PowerPoint(int maximum)
    {
        Maximum = maximum;
        Current = maximum;
    }
    
    /// <summary>
    /// The maximum amount of PP the <see cref="Pokemon"/> has.
    /// </summary>
    public int Maximum { get; set; }
    
    /// <summary>
    /// The current amount of PP the <see cref="Pokemon"/> has in a <see cref="Battle"/>
    /// </summary>
    public int Current { get; set; }

    public override string ToString() => Current.ToString();
}