using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Events;
using Game.Statuses;

namespace Game.Moves.Checks;

/// <summary>
/// An <see cref="IMoveCheck"/> used to check whether or not the <see cref="Pokemon"/> is frozen and cannot execute the <see cref="MoveTurn"/>.
/// </summary>
public class FrozenCheck : IMoveCheck
{
    /// <inheritdoc cref="IMoveCheck.Execute"/>
    public IEnumerable<Event>? Execute(MoveTurn turn, Pokemon actor, Pokemon opponent)
        => actor.StatusConditions.Contains(PokemonStatus.Freeze)
            ? new[]
            {
                new FrozenEvent(actor)
            }
            : null;
}