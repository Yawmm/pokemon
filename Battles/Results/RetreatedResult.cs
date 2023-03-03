using Game.Trainers;

namespace Game.Battles.Results;

/// <summary>
/// An <see cref="IBattleResult"/> which defines a retreated message.
/// </summary>
/// <param name="Team">The <see cref="Game.Trainers.Team"/> which retreated.</param>
public record RetreatedResult(Team Team) : IBattleResult
{
    /// <inheritdoc cref="IBattleResult.Message"/> 
    public string Message => $"[{Colors.Trainer}]{Team.Owner.Name}[/] retreated from battle!";
}