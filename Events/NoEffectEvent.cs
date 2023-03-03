using Game.Battles.Events;
using Game.Companions;
using Game.Moves;

namespace Game.Events;

/// <summary>
/// An <see cref="Event"/> used to indicate that a <see cref="Pokemon"/>'s <see cref="PokemonMove"/> had no effect on the opposing <see cref="Pokemon"/>.
/// </summary>
public record NoEffectEvent : Event
{
    public NoEffectEvent(PokemonMove move, Pokemon attacker, Pokemon defender)
        => Message = $"[{Colors.Pokemon}]{attacker.Name}[/] tried to hit [{Colors.Pokemon}]{defender.Name}[/] with [{Colors.Move}]{move.Name}[/], but it does not deal damage!";
}