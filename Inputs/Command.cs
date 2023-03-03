using System.ComponentModel;

namespace Game.Inputs;

/// <summary>
/// An enum used to represent the different commands which are available outside of a <see cref="Battle"/>.
/// </summary>
public enum Command
{
    [Description("Get information about the commands that are available")]
    Help,
    
    [Description("Start a new battle with another trainer")]
    Battle,
    
    [Description("Inspect your team")]
    Team,
    
    [Description("Heal your team")]
    Heal,
    
    [Description("Force a levelup of a pokemon in your team")]
    Level,
    
    [Description("Inspect one of the pokemon in your team in detail")]
    Inspect,
    
    [Description("Save the game")]
    Save,

    [Description("Exit the game")]
    Exit,
    
    [Description("Reset your save slot")]
    Reset
}