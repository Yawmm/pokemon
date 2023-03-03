using Game.Battles.Events;
using Game.Companions;

namespace Game.Events;

/// <summary>
/// An <see cref="Event"/> used to indicate that a <see cref="Pokemon"/> has healed itself.
/// </summary>
public record HealEvent : Event
{
    public HealEvent(Pokemon actor, double amount)
        => Message = $"[{Colors.Pokemon}]{actor.Name}[/] healed itself for {amount} health!";
}