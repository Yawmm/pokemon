using Game.Companions;
using Game.Trainers;

namespace Game.Battles;

/// <summary>
/// An interface representing a turn which a <see cref="Pokemon"/> can execute.
/// </summary>
public interface ITurn : IAction
{
    /// <summary>
    /// The priority of the <see cref="ITurn"/>
    /// </summary>
    public double Priority { get; }

    /// <summary>
    /// The <see cref="Game.Trainers.Team"/> which executed the <see cref="ITurn"/>.
    /// </summary>
    public Team Team { get; }
}