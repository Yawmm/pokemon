using Game.Battles.Events;
using Game.Trainers;

namespace Game.Battles.Rounds;

/// <summary>
/// A class used to track the <see cref="Rounds"/> of a <see cref="Battle"/>.
/// </summary>
public class History
{
    /// <summary>
    /// The current <see cref="Round"/> of the <see cref="Battle"/>.
    /// </summary>
    private Round? Current { get; set; }
    
    /// <summary>
    /// The previous list of <see cref="Round"/> in the <see cref="Battle"/>.
    /// </summary>
    public List<Round> Rounds { get; } = new();
    
    /// <summary>
    /// End the recording of <see cref="Rounds"/>.
    /// </summary>
    public void End()
    {
        if (Current is not null)
            Rounds.Add(Current);
        
        Current = null;
    }

    /// <summary>
    /// Register a <see cref="Round"/> as finished, and start a new <see cref="Round"/>.
    /// </summary>
    /// <param name="player">The player which is playing in the <see cref="Round"/>.</param>
    /// <param name="opponent">The opponent which is playing in the <see cref="Round"/>.</param>
    public void Lapse(Team player, Team opponent)
    {
        if (Current is not null)
            Rounds.Add(Current);
        
        Current = new Round
        {
            Players = new List<Team> { player, opponent }
        };
    }

    /// <summary>
    /// Log an <see cref="ITurn"/> in the current <see cref="Round"/>.
    /// </summary>
    /// <param name="turn">The <see cref="ITurn"/> to be registered.</param>
    /// <param name="events">The list of <see cref="Event"/> to be logged.</param>
    /// <param name="team">The <see cref="Team"/> under which the <see cref="ITurn"/> was executed.</param>
    public void Log(ITurn turn, List<Event> events, Team team)
    {
        if (Current is null)
            return;
        
        // Add the events to the current action, or create a new action if it doesn't
        // exist in the current round
        if (Current.Actions.TryGetValue(team, out var curr)) 
            curr.Events.AddRange(events);
        else 
        {
            Current.Actions.Add(team, new Action
            {
                Team = team,
                Turn = turn,
                Events = events
            });
        }

        Event.Log(events);
    }
}