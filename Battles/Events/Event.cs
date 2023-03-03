using Spectre.Console;

namespace Game.Battles.Events;

/// <summary>
/// A record class representing an event which occured during a <see cref="Battle"/>.
/// </summary>
public record Event
{
    /// <summary>
    /// The message of the <see cref="Event"/>.
    /// </summary>
    public string? Message { get; init; }
    
    /// <summary>
    /// Log a list of <see cref="Event"/> to the console.
    /// </summary>
    /// <param name="events">The <see cref="Event"/> which should be logged.</param>
    public static void Log(List<Event> events)
    {
        foreach (var even in events)
        {
            if (even.Message is null)
                continue;
            
            AnsiConsole.MarkupLine(even.Message);
        }
    }
}