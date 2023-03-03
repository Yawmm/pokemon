using Game.Battles.Events;
using Game.Companions;

namespace Game.Events;

/// <summary>
/// An <see cref="Event"/> used to indicate that a <see cref="Pokemon"/> is exhausted and that it does not have any PP left.
/// </summary>
public record ExhaustedEvent : Event
{
    public ExhaustedEvent(Pokemon attacker)
        => Message = $"[{Colors.Pokemon}]{attacker.Name}[/] could not perform its [{Colors.Move}]move[/] because it is exhausted!";
}