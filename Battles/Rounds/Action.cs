using Game.Battles.Events;
using Game.Trainers;

namespace Game.Battles.Rounds;

/// <summary>
/// A class used to store the <see cref="ITurn"/> which was executed during a <see cref="Round"/>, and
/// its <see cref="Event"/> results
/// </summary>
public record Action
{
    /// <summary>
    /// The <see cref="Game.Trainers.Team"/> under which the <see cref="Turn"/> was executed.
    /// </summary>
    public required Team Team { get; set; }
    
    /// <summary>
    /// The <see cref="ITurn"/> that was executed.
    /// </summary>
    public required ITurn Turn { get; set; }

    /// <summary>
    /// The list of <see cref="Event"/> that occured during the <see cref="Action"/>.
    /// </summary>
    public List<Event> Events { get; set; } = new();
}