using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;

namespace Game.Moves;

/// <summary>
/// An interface representing the checks that need to occur before a <see cref="MoveTurn"/> is executed. 
/// </summary>
public interface IMoveCheck
{
    /// <summary>
    /// Execute a check before a <see cref="MoveTurn"/> is executed.
    /// </summary>
    /// <param name="turn">The executing <see cref="MoveTurn"/>.</param>
    /// <param name="actor">The acting <see cref="Pokemon"/> executing the turn.</param>
    /// <param name="opponent">The opposing <see cref="Pokemon"/>.</param>
    /// <returns>The list of <see cref="Event"/> that occured during the check.</returns>
    IEnumerable<Event>? Execute(
        MoveTurn turn,
        Pokemon actor,
        Pokemon opponent
    );
}