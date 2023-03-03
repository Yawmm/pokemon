using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Events;

namespace Game.Moves.Checks;

/// <summary>
/// An <see cref="IMoveCheck"/> used to check whether or not the <see cref="MoveTurn"/> hits by its accuracy.
/// </summary>
public class AccuracyCheck : IMoveCheck
{
    /// <inheritdoc cref="IMoveCheck.Execute"/>
    public IEnumerable<Event>? Execute(MoveTurn turn, Pokemon actor, Pokemon opponent)
        => Random.Shared.Next(0, 100) > turn.Move.Accuracy
            ? new []
            {
                new MissedEvent(actor)
            }
            : null;
}