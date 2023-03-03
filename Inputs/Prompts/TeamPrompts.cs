using Game.Companions;
using Game.Stats;
using Game.Trainers;
using Spectre.Console;

namespace Game.Inputs.Prompts;

public static class TeamPrompts
{
    /// <summary>
    /// Log the members of <see cref="Trainer"/>'s team.
    /// </summary>
    /// <param name="name">The name of the <see cref="Trainer"/>.</param>
    /// <param name="members">The members in the team of the <see cref="Trainer"/>.</param>
    public static void GetTeam(string name, List<Pokemon> members)
    {
        AnsiConsole.MarkupLine($"The following [{Colors.Pokemon}]pokemon[/] belong to [{Colors.Trainer}]{name}[/]:\n");
        foreach (var pokemon in members)
            AnsiConsole.MarkupLine($"[{Colors.Pokemon}{(pokemon.Whiteout ? " strikethrough" : "")}]{pokemon.Name}[/] - [silver]{pokemon.Nature.Type}, health {Math.Clamp(pokemon.Stats[Stat.Health], 0, double.MaxValue):F1}, level {pokemon.Experience.Level}[/]");

        AnsiConsole.WriteLine();
        if (AnsiConsole.Confirm($"Would you like to inspect one of these [{Colors.Pokemon}]pokemon[/] in more detail?", defaultValue: false))
            PokemonPrompts.GetPokemon(members);
    }

    /// <summary>
    /// Heal a number of <see cref="Pokemon"/> on your team.
    /// </summary>
    /// <param name="name">The name of the <see cref="Trainer"/>.</param>
    /// <param name="members">The members in the team of the <see cref="Trainer"/>.</param>
    public static void HealTeam(string name, List<Pokemon> members)
    {
        AnsiConsole.MarkupLine($"The following [{Colors.Pokemon}]pokemon[/] belong to [{Colors.Trainer}]{name}[/]:\n");
        foreach (var pokemon in members)
            AnsiConsole.MarkupLine($"[{Colors.Pokemon}]{pokemon.Name}[/] - [silver]Health {Math.Clamp(pokemon.Stats[Stat.Health], 0, double.MaxValue):F1}[/]");
        
        AnsiConsole.WriteLine();
        var toHeal = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Pokemon>()
                .Title($"Which [{Colors.Pokemon}]pokemon[/] would you like to heal?")
                .AddChoices(members)
        );

        foreach (var pokemon in toHeal)
        {
            if (pokemon.FullHeal() != 0)
            {
                AnsiConsole.MarkupLine($"[{Colors.Pokemon}]{pokemon.Name}[/]'s health has been restored to {pokemon.Stats[Stat.Health]:F1}!");
                continue;
            }
        
            AnsiConsole.MarkupLine($"[{Colors.Pokemon}]{pokemon.Name}[/]'s health was already at its maximum.");
        }
    }
}