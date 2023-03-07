using Game.Battles.Events;
using Game.Companions;
using Game.Moves;
using Game.Stats;
using Game.Trainers;
using Spectre.Console;

namespace Game.Battles.Turns;

/// <summary>
/// An <see cref="ITurn"/> which executes a <see cref="PokemonMove"/>.
/// </summary>
public record MoveTurn : ITurn
{
    /// <summary>
    /// The list of <see cref="PokemonMoveStage"/> of the <see cref="MoveTurn"/> that have been executed.
    /// </summary>
    private readonly List<PokemonMoveStage> _executed = new();

    /// <summary>
    /// The <see cref="PokemonMove"/> that should be executed.
    /// </summary>
    public required PokemonMove Move { get; init; }
    
    /// <summary>
    /// The <see cref="Pokemon"/> that should execute the <see cref="Move"/>.
    /// </summary>
    public required Pokemon Actor { get; init; }

    /// <summary>
    /// The <see cref="Game.Trainers.Team"/> that should be effected by the <see cref="Move"/>.
    /// </summary>
    public required Team Target { get; init; }
    
    /// <inheritdoc cref="ITurn.Team"/>
    public required Team Team { get; init; }

    /// <inheritdoc cref="ITurn.Priority"/>
    public int Priority => Move.Priority ?? 0;

    /// <summary>
    /// Whether or not the <see cref="Move"/> is finished executing all its stages.
    /// </summary>
    public bool Finished => _executed.Count == Move.Stages.Count;

    /// <summary>
    /// The list of <see cref="IMoveCheck"/> that should be executed before executing the <see cref="Move"/>.
    /// </summary>
    public IEnumerable<IMoveCheck> Checks { get; init; } = new List<IMoveCheck>();

    /// <inheritdoc cref="ITurn.Execute"/>
    public IEnumerable<Event> Execute(Battle battle)
    {
        // Execute all the checks
        var checkResults = Checks
            .Select(c => c.Execute(
                this,
                Team.Actor,
                Target.Actor
            ))
            .Where(e => e is not null)
            .SelectMany(e => e!)
            .ToList();
        
        // Return the results of the checks if there are any, and stop since the checks failed
        if (checkResults.Any())
            return checkResults;
        
        AnsiConsole.MarkupLine($"[{Colors.Trainer}]{Team.Owner.Name}[/] chose [{Colors.Move}]{Move}[/]!");
        
        // Get the current stage of the move that should be executed
        var stage = Move.Stages.FirstOrDefault(s => !_executed.Contains(s));
        if (stage is null)
            return new List<Event>();
        
        // Execute the move stage
        var result = Move.Execute(stage, this)
            .ToList();
        
        // Register the executed move stage
        _executed.Add(stage);

        return result;
    }
}