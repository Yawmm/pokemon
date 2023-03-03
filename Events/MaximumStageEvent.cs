using Game.Battles.Events;
using Game.Companions;
using Game.Stats;

namespace Game.Events;

/// <summary>
/// An <see cref="Event"/> used to indicate that a <see cref="Pokemon"/> couldn't increase or decrease a stat stage.
/// </summary>
public record MaximumStageEvent : Event
{
    public MaximumStageEvent(Pokemon actor, Stat stat)
        => Message = $"[{Colors.Pokemon}]{actor.Name}[/]'s {stat.ToString().ToLower()} could not go any further!";
}