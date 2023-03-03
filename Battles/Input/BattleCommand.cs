using System.ComponentModel;

namespace Game.Battles.Input;

/// <summary>
/// An enum used to represent the different commands which are available during a <see cref="Battle"/>. 
/// </summary>
public enum BattleCommand
{
    [Description("Get information about the commands that are available")]
    Help,
    
    [Description("Use a move on your current pokemon")]
    Move,
    
    [Description("Swap your current acting pokemon for another one in your team")]
    Swap,

    [Description("Inspect your team, or the team of the opponent")]
    Inspect,
    
    [Description("Retreat from the battle")]
    Retreat,
}