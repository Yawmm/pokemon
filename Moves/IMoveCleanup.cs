using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;

namespace Game.Moves;

/// <summary>
/// An interface representing the cleanups that need to occur after a <see cref="MoveTurn"/> is executed. 
/// </summary>
public interface IMoveCleanup
{
    /// <summary>
    /// Execute a cleanup after a <see cref="MoveTurn"/> is executed.
    /// </summary>
    /// <param name="turn">The <see cref="MoveTurn"/> that was executed.</param>
    /// <param name="actor">The acting <see cref="Pokemon"/> which executed the turn.</param>
    /// <param name="opponent">The opposing <see cref="Pokemon"/>.</param>
    /// <returns>The list of <see cref="Event"/> that occured during the cleanup.</returns>
    IEnumerable<Event>? Execute(
        MoveTurn turn,
        Pokemon actor, 
        Pokemon opponent
    );
}