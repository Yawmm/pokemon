using Game.Battles.Events;
using Game.Companions;
using Game.Stats;

namespace Game.Events;

/// <summary>
/// An <see cref="Event"/> used to indicate that the stat stage of a <see cref="Pokemon"/> has changed.
/// </summary>
public record StageChangeEvent : Event
{
    public StageChangeEvent(Pokemon actor, Stat stat, int stages)
        => Message = $"[{Colors.Pokemon}]{actor.Name}[/]'s {stat.ToString().ToLower()} " + stages switch
        {
            1 => "rose!",
            2 => "sharply rose!",
            >= 3 => "rose drastically!",
            
            -1 => "fell!",
            -2 => "harshly fell!",
            <= -3 => "severely fell!",
            
            _ => "remains unchanged!"
        };
}