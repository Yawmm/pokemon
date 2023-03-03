using Game.Battles;
using Game.Battles.Events;
using Game.Companions;

namespace Game.Events;

/// <summary>
/// An <see cref="Event"/> used to indicate that a <see cref="Pokemon"/> has flinched and couldn't perform its <see cref="ITurn"/>.
/// </summary>
public record FlinchEvent : Event
{
    public FlinchEvent(Pokemon actor)
        => Message = $"[{Colors.Pokemon}]{actor.Name}[/] [{Colors.Flinch}]flinched[/] and couldn't execute its move!";
}