using Game.Battles.Events;
using Game.Companions;
using Game.Moves;

namespace Game.Events;

/// <summary>
/// An <see cref="Event"/> used to indicate that a <see cref="PokemonMove"/> has been evaded.
/// </summary>
public record EvadedEvent : Event
{
    public EvadedEvent(Pokemon attacker, Pokemon defender)
        => Message = $"[{Colors.Pokemon}]{defender.Name}[/] successfully dodged [{Colors.Pokemon}]{attacker.Name}[/]'s move!";
}