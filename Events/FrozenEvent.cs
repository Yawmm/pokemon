using Game.Battles.Events;
using Game.Companions;

namespace Game.Events;

/// <summary>
/// An <see cref="Event"/> used to indicate that a <see cref="Pokemon"/> is frozen and couldn't perform its <see cref="ITurn"/>.
/// </summary>
public record FrozenEvent : Event
{
    public FrozenEvent(Pokemon pokemon)
        => Message = $"[{Colors.Pokemon}]{pokemon.Name}[/] could not attack because they are [{Colors.Freeze}]frozen[/]!";
}