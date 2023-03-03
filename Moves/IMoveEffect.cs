using Game.Battles.Events;
using Game.Battles.Turns;
using Game.Companions;
using Game.Moves.Damage;

namespace Game.Moves;

/// <summary>
/// An interface representing the effects that need to occur during the execution of a <see cref="MoveTurn"/>. 
/// </summary>
public interface IMoveEffect
{
    /// <summary>
    /// Execute a check before a <see cref="MoveTurn"/> is executed.
    /// </summary>
    /// <param name="turn">The executing <see cref="MoveTurn"/>.</param>
    /// <param name="actor">The acting <see cref="Pokemon"/> executing the turn.</param>
    /// <param name="opponent">The opposing <see cref="Pokemon"/>.</param>
    /// <param name="calculateDamage">A function used to retrieve the damage that the <see cref="IMoveEffect"/> could deal, when <see cref="IDamageModifier"/> are applied.</param>
    /// <returns>The list of <see cref="Event"/> that occured during the check.</returns>
    IEnumerable<Event> Execute(
        MoveTurn turn,
        Pokemon actor,
        Pokemon opponent, 
        Func<DamageResult> calculateDamage
    );
}