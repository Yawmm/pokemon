using Game.Trainers;

namespace Game.Battles;

/// <summary>
/// An interface representing the checks that need to occur before an <see cref="ITurn"/> is executed. 
/// </summary>
public interface ITurnCheck
{
    /// <summary>
    /// Execute a check before an <see cref="ITurn"/> is executed.
    /// </summary>
    /// <param name="team">The <see cref="Team"/> which is executing the <see cref="ITurn"/>.</param>
    /// <param name="battle">The context of the current <see cref="Battle"/>.</param>
    /// <returns>A replacement <see cref="ITurn"/> that should be executed, or null when no replacement should be used.</returns>
    ITurn? Execute(Team team, Battle battle);
}