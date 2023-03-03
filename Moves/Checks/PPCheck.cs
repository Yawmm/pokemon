using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Events;

namespace Game.Moves.Checks;

/// <summary>
/// An <see cref="IMoveCheck"/> used to check whether or not the <see cref="PokemonMove"/> is exhausted and cannot be executed.
/// </summary>
public class PPCheck : IMoveCheck
{
    /// <inheritdoc cref="IMoveCheck.Execute"/>
    public IEnumerable<Event>? Execute(MoveTurn turn, Pokemon actor, Pokemon opponent)
        => turn.Move.PP.Current <= 0
            ? new []
            {
                new ExhaustedEvent(actor) 
            }
            : null;
}