using Game.Companions;
using Newtonsoft.Json;
using Spectre.Console;

namespace Game.Experience.Effects;

/// <summary>
/// A class used to apply evolutions to <see cref="Pokemon"/> which meet the requirements.
/// </summary>
[JsonObject]
public class EvolveEffect : ILevelEffect
{
    /// <inheritdoc cref="ILevelEffect.Execute"/>
    public void Execute(Pokemon actor, int oldLevel, int newLevel)
    {
        // Check if the actor can evolve
        if (actor.Evolution is null || actor.Evolution.Level > actor.Experience.Level)
            return;

        // Check if the owner wants to evolve
        if (actor.Owner is null || !actor.Owner.Decider.Boolean($"[{Colors.Pokemon}]{actor.Name}[/] is ready to evolve, do you want to proceed?"))
            return;

        // Get evolution
        var evolution = actor.Evolution.Pokemon;
        
        evolution.Experience.Value = actor.Experience.Value;
        evolution.Moves.Value = actor.Moves.Value;
        evolution.Nature = actor.Nature;
        
        // Switch owner
        var owner = actor.Owner!;
        owner.RemovePokemon(actor);
        owner.AddPokemon(evolution);

        // Switch team
        if (actor.Team is not null)
        {
            var team = actor.Team;
            var wasTeamActor = team.Actor == actor;

            team.RemoveMember(actor);
            team.AddMember(evolution);

            if (wasTeamActor)
                team.Actor = evolution;
        }

        AnsiConsole.MarkupLine($"[{Colors.Pokemon}]{actor.Name}[/] evolved into [{Colors.Pokemon}]{evolution.Name}[/]!");
    }
}