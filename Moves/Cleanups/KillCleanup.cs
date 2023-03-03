using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Events;
using Game.Stats;

namespace Game.Moves.Cleanups;

/// <summary>
/// An <see cref="IMoveCleanup"/> used to check if an executed <see cref="MoveTurn"/> killed the opponent, and if it did clean up the results.
/// </summary>
public class KillCleanup : IMoveCleanup
{
    /// <inheritdoc cref="IMoveCleanup.Execute"/>
    public IEnumerable<Event>? Execute(MoveTurn turn, Pokemon actor, Pokemon opponent)
    {
        if (opponent.Stats[Stat.Health] > 0)
            return null;

        foreach (var (stat, yield) in opponent.Statistics.Yield)
            actor.Statistics.EVs[stat] += yield;

        var experienceYield = actor.Experience.Kill(actor, opponent);
        
        var result = new List<Event>
        {
            new KillEvent(actor, opponent, experienceYield)
        };

        return result;
    }
}