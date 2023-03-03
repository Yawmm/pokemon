using Game.Battles.Events;
using Game.Companions;
using Game.Moves;

namespace Game.Events;

/// <summary>
/// An <see cref="Event"/> used to indicate that a <see cref="Pokemon"/> missed its <see cref="PokemonMove"/>.
/// </summary>
public record MissedEvent : Event
{
    public MissedEvent(Pokemon attacker)
        => Message = $"[{Colors.Pokemon}]{attacker.Name}[/]'s move missed!";
}