using Game.Battles.Events;
using Game.Trainers;

namespace Game.Battles;

/// <summary>
/// An interface representing an action which a <see cref="Trainer"/> can perform.
/// </summary>
public interface IAction
{
    /// <summary>
    /// Execute an action during a <see cref="Battle"/>.
    /// </summary>
    /// <param name="battle">The context of the <see cref="Battle"/>.</param>
    /// <returns>The resulting <see cref="Event"/> that occurred during the action.</returns>
    IEnumerable<Event>? Execute(Battle battle);
}