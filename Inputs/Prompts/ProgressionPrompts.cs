using Game.Companions;
using Game.Configurations;
using Game.Content;
using Game.Trainers;
using Game.Trainers.Deciders;
using Spectre.Console;

namespace Game.Inputs.Prompts;

/// <summary>
/// A prompt class used to manage the <see cref="Progression"/> of the player.
/// </summary>
public static class ProgressionPrompts
{
    /// <summary>
    /// Start a new game.
    /// </summary>
    /// <param name="progression">The current <see cref="Progression"/> file.</param>
    /// <param name="configuration">The <see cref="Configuration"/> of the application.</param>
    public static void Intro(Progression progression, Configuration configuration)
    {
        // Greeting
        AnsiConsole.MarkupLine($"Welcome, [{Colors.Trainer}]trainer[/], to this game of [{Colors.Pokemon}]Pokemon Confrontation[/]! [grey]not to be confused with pokemon showdown[/]");
        AnsiConsole.MarkupLine($"Here, you will be fighting your toughest of [{Colors.Battle}]battles[/] with your best [{Colors.Pokemon}]pokemon[/].");

        // Get profile
        AnsiConsole.MarkupLine($"\nLet's get started with who you are, and which [{Colors.Pokemon}]pokemon[/] you will chose to bring along on your epic journey.\n");
        progression.Profile = GetPlayer();

        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"\nExcellent choice of starters, [{Colors.Trainer}]{progression.Profile.Name}[/]! [{Colors.Pokemon}]{progression.Profile.Pokemon.First().Name}[/] is one of the best [{Colors.Pokemon}]pokemon[/] to start off with in this game.");

        AnsiConsole.MarkupLine($"To navigate through the menu in this game, you will need to use [{Colors.Command}]commands[/]. [grey]use the 'help' command for extra information[/]");
        Progression.Save(progression, configuration);
    }

    /// <summary>
    /// Greet the player if a save game already exists.
    /// </summary>
    /// <param name="progression">The current <see cref="Progression"/> file.</param>
    public static void Greeting(Progression progression)
    {
        AnsiConsole.MarkupLine($"Welcome back, [{Colors.Trainer}]{progression.Profile.Name}[/]!");
        AnsiConsole.MarkupLine($"To navigate through the menu in this game, you will need to use [{Colors.Command}]commands[/]. [grey]use the 'help' command for extra information[/]");
    }

    /// <summary>
    /// Save the <see cref="Progression"/> of the player.
    /// </summary>
    /// <param name="progression">The current <see cref="Progression"/> file.</param>
    /// <param name="configuration">The <see cref="Configuration"/> of the application.</param>
    public static void Save(Progression progression, Configuration configuration)
    {
        Progression.Save(progression, configuration);
        AnsiConsole.MarkupLine($"Successfully saved [{Colors.Trainer}]{progression.Profile.Name}[/]'s game to disk.");
    }

    /// <summary>
    /// Reset the <see cref="Progression"/> of the player.
    /// </summary>
    /// <param name="progression">The current <see cref="Progression"/> file.</param>
    /// <param name="configuration">The <see cref="Configuration"/> of the application.</param>
    /// <returns>Whether or not the player proceeded with the reset.</returns>
    public static bool Reset(Progression progression, Configuration configuration)
    {
        if (!AnsiConsole.Confirm($"Are you sure you want to reset your progression, [{Colors.Trainer}]{progression.Profile.Name}[/]?"))
            return false;

        Progression.Remove(configuration);
        AnsiConsole.MarkupLine($"Successfully reset [{Colors.Trainer}]{progression.Profile.Name}[/]'s game to disk.");
        AnsiConsole.WriteLine();
        AnsiConsole.Clear();

        return true;
    }

    /// <summary>
    /// Create a new <see cref="Trainer"/> by the inputs of the player.
    /// </summary>
    /// <returns>The newly created <see cref="Trainer"/>.</returns>
    public static Trainer GetPlayer()
    {
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>($"What would you like your [blue]name[/] to be, [{Colors.Trainer}]trainer[/]?")
        );

        AnsiConsole.WriteLine();
        var starters = GetStarters(out var teamNames);

        AnsiConsole.WriteLine();
        ShowSummary(name, teamNames);

        AnsiConsole.WriteLine();
        if (AnsiConsole.Confirm("Finish your setup and confirm your profile?"))
            return new Trainer(
                name: name,
                decider: new PlayerDecider(), 
                pokemon: starters
            );

        AnsiConsole.MarkupLine("\n[silver]Rerunning setup...[/]\n\n");
        AnsiConsole.Clear();
        return GetPlayer();
    }

    /// <summary>
    /// Show a summary of the <see cref="Trainer"/> before it is created.
    /// </summary>
    /// <param name="name">The name of the <see cref="Trainer"/>.</param>
    /// <param name="teamNames">The names of the <see cref="Pokemon"/> that the player chose as starters.</param>
    private static void ShowSummary(string name, string teamNames)
    {
        var table = new Table { Border = TableBorder.Rounded }
            .AddColumns("Key", "Value")
            .AddRow("[white bold]Name[/]", name)
            .AddRow($"[{Colors.Pokemon}]Starter team[/]", teamNames);

        AnsiConsole.MarkupLine($"This will be your [{Colors.Trainer}]trainer[/]'s properties for the rest of the game.");
        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Get the list of <see cref="Pokemon"/> that the player would like as starters.
    /// </summary>
    /// <param name="teamNames">The names of the chosen list of <see cref="Pokemon"/> starters</param>
    /// <returns>The list of chosen <see cref="Pokemon"/> starters.</returns>
    private static List<Pokemon> GetStarters(out string teamNames)
    {
        var starters = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Pokemon>()
                .Title($"What will be your starting [{Colors.Pokemon}]pokemon[/]?")
                .AddChoices(
                    PokemonList.Bulbasaur(),
                    PokemonList.Charmander(),
                    PokemonList.Squirtle()
                )
        );

        teamNames = string.Join(", ", starters);
        AnsiConsole.MarkupLine($"[{Colors.Pokemon}]{teamNames}[/] will be in your starting team.");
        
        return starters;
    }
}