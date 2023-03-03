using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;

namespace Game.Moves.Cleanups;

/// <summary>
/// An <see cref="IMoveCleanup"/> used to deduct PP from the executed <see cref="PokemonMove"/>.
/// </summary>
public class PPCleanup : IMoveCleanup
{
    /// <inheritdoc cref="IMoveCleanup.Execute"/>
    public IEnumerable<Event>? Execute(MoveTurn turn, Pokemon actor, Pokemon opponent)
    {
        turn.Move.PP.Current -= 1;
        return null;
    }
}