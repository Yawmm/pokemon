using Game.Trainers;

namespace Game.Battles.Rounds;

/// <summary>
/// A class used to store the different rounds in a <see cref="Battle"/>.
/// </summary>
public record Round
{
    /// <summary>
    /// The list of <see cref="Team"/> who participated in the <see cref="Round"/>.
    /// </summary>
    public required List<Team> Players { get; init; }
    
    /// <summary>
    /// The <see cref="Action"/> which were executed during the <see cref="Round"/>.
    /// </summary>
    public Dictionary<Team, Action> Actions { get; } = new();
}