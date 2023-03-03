using Game.Trainers;

namespace Game.Battles.Results;

/// <summary>
/// An <see cref="IBattleResult"/> which defines a defeated message.
/// </summary>
/// <param name="Winner">The <see cref="Team"/> which defeated the <see cref="Defeated"/> <see cref="Team"/>.</param>
/// <param name="Defeated">The <see cref="Team"/> which was defeated.</param>
public record DefeatedResult(Team Winner, Team Defeated) : IBattleResult
{
    /// <inheritdoc cref="IBattleResult.Message"/> 
    public string Message => $"[{Colors.Trainer}]{Winner.Owner.Name}[/] has defeated [{Colors.Trainer}]{Defeated.Owner.Name}[/]!";
}