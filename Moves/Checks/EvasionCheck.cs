using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Events;
using Game.Stats;

namespace Game.Moves.Checks;

/// <summary>
/// An <see cref="IMoveCheck"/> used to check whether or not the <see cref="MoveTurn"/> hits by the opposing <see cref="Pokemon"/>'s evasion stat.
/// </summary>
public class EvasionCheck : IMoveCheck
{
    /// <inheritdoc cref="IMoveCheck.Execute"/>
    public IEnumerable<Event>? Execute(MoveTurn turn, Pokemon actor, Pokemon opponent)
        => Random.Shared.Next(0, 100) < opponent.Stats[Stat.Evasion]
            ? new []
            {
                new EvadedEvent(actor, opponent)
            }
            : null;
}