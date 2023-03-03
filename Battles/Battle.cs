using Game.Battles.Events;
using Game.Battles.Results;
using Game.Battles.Rounds;
using Game.Trainers;
using Spectre.Console;

namespace Game.Battles;

/// <summary>
/// A class used to track a battle between two <see cref="Team"/>.
/// </summary>
public record Battle
{
    /// <summary>
    /// The <see cref="Team"/> of the player <see cref="Trainer"/>.
    /// </summary>
    public required Team Player { get; init; }
    
    /// <summary>
    /// The <see cref="Team"/> of the chosen opponent <see cref="Trainer"/>.
    /// </summary>
    public required Team Opponent { get; init; }

    /// <summary>
    /// The result of the <see cref="Battle"/>, or when the battle is still ongoing and the value is null.
    /// </summary>
    public IBattleResult? Result { get; private set; }
    
    /// <summary>
    /// The recorded list of <see cref="Rounds.Action"/> of the <see cref="Battle"/>.
    /// </summary>
    public History History { get; } = new();
    
    /// <summary>
    /// The list of <see cref="ITurnCheck"/> that need to be executed before an <see cref="ITurn"/> is executed.
    /// </summary>
    public required IEnumerable<ITurnCheck> TurnChecks { get; init; }
    
    /// <summary>
    /// The list of <see cref="ITurnCleanup"/> that need to be executed after an <see cref="ITurn"/> is executed.
    /// </summary>
    public required IEnumerable<ITurnCleanup> TurnCleanups { get; init; }

    /// <summary>
    /// Start the <see cref="Battle"/> and the recording of <see cref="Round"/>.
    /// </summary>
    public void Begin()
    {
        History.Lapse(Player, Opponent);
    }
    
    /// <summary>
    /// End the <see cref="Battle"/> and reset all the <see cref="Trainer"/> and reset the <see cref="History"/>.
    /// </summary>
    /// <param name="result">The <see cref="IBattleResult"/> of the <see cref="Battle"/>.</param>
    public void End(IBattleResult result)
    {
        Opponent.Owner.Reset();
        Player.Owner.Reset();

        History.End();

        Result = result;
    }
    
    /// <summary>
    /// Execute a <see cref="Round"/> in the <see cref="Battle"/>.
    /// </summary>
    /// <param name="player">The <see cref="ITurn"/> of the player.</param>
    /// <param name="opponent">The <see cref="ITurn"/> of the opponent.</param>
    /// <returns>The <see cref="IBattleResult"/> of the <see cref="Battle"/>, or null if it is still ongoing.</returns>
    public IBattleResult? Execute(ITurn player, ITurn opponent)
    {
        // Get the correct turn order.
        var (first, second) = player.Priority > opponent.Priority
            ? (player, opponent)
            : player.Priority < opponent.Priority 
                ? (opponent, player)
                : Random.Shared.Next(0, 2) == 0
                    ? (player, opponent)
                    : (opponent, player);

        // Execute the turns
        var turnResult = ExecuteTurns(first, second);
        if (turnResult is not null)
            return turnResult;

        // Execute the cleanups
        var cleanupResult = ExecuteCleanup(first, second);
        
        // Register round
        History.Lapse(Player, Opponent);
        return cleanupResult;
    }

    /// <summary>
    /// Execute the two <see cref="ITurn"/> in the given order.
    /// </summary>
    /// <param name="first">The first <see cref="ITurn"/> that should be executed.</param>
    /// <param name="second">The second <see cref="ITurn"/> that should be executed.</param>
    /// <returns>The <see cref="IBattleResult"/> of the <see cref="Battle"/>, or null if it is still ongoing.</returns>
    private IBattleResult? ExecuteTurns(ITurn first, ITurn second)
    {
        // Execute first turn
        var firstEvents = first.Execute(this)?.ToList();
        if (firstEvents is not null && firstEvents.Any()) History.Log(first, firstEvents, first.Team);
        if (second.Team.IsDefeated)
            return new DefeatedResult(first.Team, second.Team);

        AnsiConsole.WriteLine();

        // Execute second turn
        var secondEvents = second.Execute(this)?.ToList();
        if (secondEvents is not null && secondEvents.Any()) History.Log(second, secondEvents, second.Team);
        if (first.Team.IsDefeated)
            return new DefeatedResult(second.Team, first.Team);

        return null;
    }

    /// <summary>
    /// Execute the <see cref="TurnCleanups"/> for each of the executed set of <see cref="ITurn"/>.
    /// </summary>
    /// <param name="first">The first <see cref="ITurn"/> that was executed.</param>
    /// <param name="second">The second <see cref="ITurn"/> that was executed.</param>
    /// <returns>The <see cref="IBattleResult"/> of the <see cref="Battle"/>, or null if it is still ongoing.</returns>
    private IBattleResult? ExecuteCleanup(ITurn first, ITurn second)
    {
        // Execute cleanups
        var firstCleanupEvents = GetCleanups(first);
        var secondCleanupEvents = GetCleanups(second);
        
        // Log cleanups
        if (firstCleanupEvents.Any() || secondCleanupEvents.Any())
        {
            AnsiConsole.WriteLine();
            History.Log(first, firstCleanupEvents, first.Team);
            History.Log(second, secondCleanupEvents, second.Team);
        }
        
        // Get battle results
        if (first.Team.IsDefeated)
            return new DefeatedResult(second.Team, first.Team);
        
        if (second.Team.IsDefeated)
            return new DefeatedResult(first.Team, second.Team);

        return null;
    }

    /// <summary>
    /// Execute the <see cref="TurnCleanups"/> for a <see cref="ITurn"/>.
    /// </summary>
    /// <param name="turn">The executed <see cref="ITurn"/>.</param>
    /// <returns>The list of <see cref="Event"/> that occured during the <see cref="ITurn"/>.</returns>
    private List<Event> GetCleanups(ITurn turn)
        => TurnCleanups
            .Select(c => c.Execute(
                turn,
                this
            ))
            .Where(e => e is not null)
            .SelectMany(e => e!)
            .ToList();
}
