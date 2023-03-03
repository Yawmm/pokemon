using Game.Battles.Events;
using Game.Companions;
using Game.Moves.Damage;
using Game.Stats;
using Game.Types;

namespace Game.Events;

/// <summary>
/// An <see cref="Event"/> used to indicate that a <see cref="Pokemon"/> has successfully hit another <see cref="Pokemon"/>.
/// </summary>
public record HitEvent : Event
{
    public HitEvent(Pokemon attacker, Pokemon defender, DamageResult damage)
    {
        // Set base message
        Message = $"[{Colors.Pokemon}]{attacker.Name}[/] hit [{Colors.Pokemon}]{defender.Name}[/] for {damage.Value:F1} damage!";
        
        if (damage.Effectiveness != TypeEffectiveness.Nullified)
        {
            // Add defender health message
            if (!defender.Whiteout)
                Message += $" [{Colors.Pokemon}]{defender.Name}[/]'s HP is now down to {defender.Stats[Stat.Health]:F1} health.";

            // Add critical hit message
            if (damage.IsCritical)
                Message += " A critical hit!";
        }

        // Add effectiveness message
        if (damage.Effectiveness != TypeEffectiveness.Default)
        {
            Message += damage.Effectiveness switch
            {
                TypeEffectiveness.Ineffective or TypeEffectiveness.Disadvantaged => " It was not very effective!",
                TypeEffectiveness.Effective or TypeEffectiveness.Advantaged => " It was very effective!",
                _ => " It had no effect!"
            };
        }
    }
}