using Game.Battles.Events;
using Game.Companions;
using Game.Statuses;

namespace Game.Events;

/// <summary>
/// An <see cref="Event"/> used to indicate that a <see cref="PokemonStatus"/> has been applied.
/// </summary>
public record ApplyStatusEvent : Event
{
    public ApplyStatusEvent(Pokemon attacker, Pokemon defender, PokemonStatus status, string color)
        => Message = $"[{Colors.Pokemon}]{attacker.Name}[/] applied the [{color}]{status.ToString().ToLower()}[/] effect to [{Colors.Pokemon}]{defender.Name}[/]!";
}