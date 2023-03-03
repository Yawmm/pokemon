using Game.Battles.Events;
using Game.Companions;
using Game.Trainers;

namespace Game.Events;

/// <summary>
/// An <see cref="Event"/> used to indicate that the actor of a <see cref="Team"/> has been replaced.
/// </summary>
public record ReplaceActorEvent : Event
{
    public ReplaceActorEvent(Team team, Pokemon original, Pokemon replacement)
        => Message = $"[{Colors.Trainer}]{team.Owner.Name}[/] replaced their team's actor [{Colors.Pokemon}]{original}[/] with [{Colors.Pokemon}]{replacement}[/]!";
}