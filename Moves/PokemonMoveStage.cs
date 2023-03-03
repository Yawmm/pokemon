using Game.Battles;
using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Moves.Damage;
using Newtonsoft.Json;

namespace Game.Moves;

/// <summary>
/// A class used to represent a stage of a <see cref="PokemonMove"/>.
/// </summary>
[JsonObject]
public class PokemonMoveStage
{
    [JsonConstructor]
    public PokemonMoveStage() { }
    
    /// <summary>
    /// The range of <see cref="IMoveEffect"/> the <see cref="PokemonMoveStage"/> has.
    /// </summary>
    public List<IMoveEffect> Effects { get; set; } = new();
    
    /// <summary>
    /// The range of <see cref="IMoveCheck"/> the <see cref="PokemonMoveStage"/> has.
    /// </summary>
    public List<IMoveCheck> Checks { get; set; } = new();
    
    /// <summary>
    /// The range of <see cref="IMoveCleanup"/> the <see cref="PokemonMoveStage"/> has.
    /// </summary>
    public List<IMoveCleanup> Cleanups { get; set; } = new();

    /// <summary>
    /// The range of <see cref="IDamageModifier"/> the <see cref="PokemonMoveStage"/> has.
    /// </summary>
    public List<IDamageModifier> Modifiers { get; set; } = new();

    /// <summary>
    /// Execute the <see cref="PokemonMoveStage"/> in the given <see cref="Battle"/> context.
    /// </summary>
    /// <param name="move">The parent <see cref="PokemonMove"/> which was executed.</param>
    /// <param name="moveTurn">The context of the executing <see cref="MoveTurn"/>.</param>
    /// <returns>The list of <see cref="Event"/> which occurred during the execution of the <see cref="PokemonMoveStage"/>.</returns>
    public IEnumerable<Event> Execute(PokemonMove move, MoveTurn moveTurn)
    {
        // Execute checks to determine whether the effect can be executed.
        var checkEvents = RunCheckEvents(moveTurn).ToList();
        if (checkEvents.Any())
            return checkEvents;

        // Execute all the effects of the move.
        var effectEvents = RunEffectEvents(move, moveTurn);

        // Execute all the cleanups
        var cleanupEvents = RunCleanupEvents(moveTurn);

        return effectEvents
            .Concat(cleanupEvents);
    }

    /// <summary>
    /// Run the range of <see cref="IMoveCheck"/> of the <see cref="PokemonMoveStage"/>.
    /// </summary>
    /// <param name="moveTurn">The context of the executing <see cref="MoveTurn"/>.</param>
    /// <returns>The list of <see cref="Event"/> which occurred during the checks.</returns>
    private IEnumerable<Event> RunCheckEvents(MoveTurn moveTurn)
        => Checks
            .Select(c => c.Execute(
                moveTurn,
                moveTurn.Actor,
                moveTurn.Target.Actor
            ))
            .Where(r => r is not null)
            .SelectMany(e => e!);

    /// <summary>
    /// Run the range of <see cref="IMoveEffect"/> of the <see cref="PokemonMoveStage"/>.
    /// </summary>
    /// <param name="move">The parent <see cref="PokemonMove"/> which was executed.</param>
    /// <param name="moveTurn">The context of the executing <see cref="MoveTurn"/>.</param>
    /// <returns>The list of <see cref="Event"/> which occurred during the effects.</returns>
    private IEnumerable<Event> RunEffectEvents(PokemonMove move, MoveTurn moveTurn)
        => Effects
            .SelectMany(e => e.Execute(
                moveTurn,
                moveTurn.Actor,
                moveTurn.Target.Actor,
                () => move.CalculateDamage(
                    moveTurn.Move,
                    moveTurn.Actor,
                    moveTurn.Target.Actor,
                    Modifiers
                )
            ));

    /// <summary>
    /// Run the range of <see cref="IMoveCleanup"/> of the <see cref="PokemonMoveStage"/>.
    /// </summary>
    /// <param name="moveTurn">The context of the executing <see cref="MoveTurn"/>.</param>
    /// <returns>The list of <see cref="Event"/> which occurred during the cleanups.</returns>
    private IEnumerable<Event> RunCleanupEvents(MoveTurn moveTurn)
        => Cleanups
            .Select(s => s.Execute(
                moveTurn,
                moveTurn.Actor,
                moveTurn.Target.Actor
            ))
            .Where(r => r is not null)
            .SelectMany(e => e!);
}