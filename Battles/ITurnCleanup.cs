using Game.Battles.Events;

namespace Game.Battles;

/// <summary>
/// An interface representing the cleanup functions that need to occur after an <see cref="ITurn"/> is executed. 
/// </summary>
public interface ITurnCleanup
{
    /// <summary>
    /// Execute a cleanup after an <see cref="ITurn"/>.
    /// </summary>
    /// <param name="turn">The <see cref="ITurn"/> that was just executed.</param>
    /// <param name="battle">The context of the current <see cref="Battle"/>.</param>
    /// <returns>The list of <see cref="Event"/> that occured during the cleanup.</returns>
    IEnumerable<Event>? Execute(ITurn turn, Battle battle);
}