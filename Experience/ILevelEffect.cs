using Game.Companions;

namespace Game.Experience;

/// <summary>
/// An interface representing functions that should be executed after a <see cref="Pokemon"/> levels up.
/// </summary>
public interface ILevelEffect
{
    /// <summary>
    /// Execute an effect after level up.
    /// </summary>
    /// <param name="actor">The <see cref="Pokemon"/> which levelled up.</param>
    /// <param name="oldLevel">The old level of the <see cref="Pokemon"/>.</param>
    /// <param name="newLevel">The new level of the <see cref="Pokemon"/>.</param>
    void Execute(Pokemon actor, int oldLevel, int newLevel);
}