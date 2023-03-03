using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Events;
using Game.Statuses;

namespace Game.Moves.Checks;

/// <summary>
/// An <see cref="IMoveCheck"/> used to check whether or not the <see cref="Pokemon"/> flinched and cannot execute the <see cref="MoveTurn"/>.
/// </summary>
public class FlinchCheck : IMoveCheck
{
    /// <inheritdoc cref="IMoveCheck.Execute"/>
    public IEnumerable<Event>? Execute(MoveTurn turn, Pokemon actor, Pokemon opponent)
    {
        if (!actor.StatusConditions.Contains(PokemonStatus.Flinch))
            return null;

        actor.StatusConditions.Remove(PokemonStatus.Flinch);
        return new []
        {
            new FlinchEvent(actor)
        };
    }
}