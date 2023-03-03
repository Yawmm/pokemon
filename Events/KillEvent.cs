using Game.Battles.Events;
using Game.Companions;

namespace Game.Events;

/// <summary>
/// An <see cref="Event"/> used to indicate that a <see cref="Pokemon"/> has successfully killed another <see cref="Pokemon"/>.
/// </summary>
public record KillEvent : Event
{
    public KillEvent(Pokemon attacker, Pokemon killed, double experienceYield)
        => Message = $"[{Colors.Pokemon}]{attacker.Name}[/] killed [{Colors.Pokemon}]{killed.Name}[/]! [{Colors.Pokemon}]{attacker.Name}[/] gained {experienceYield:F1} experience!";
}