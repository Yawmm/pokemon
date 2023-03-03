using Game;
using Game.Configurations;
using Game.Inputs;
using Game.Inputs.Prompts;
using Spectre.Console;

// Run the game
Run();

void Run()
{
    // Load configuration
    var configuration = Configuration.Get("appsettings.json");
    var progression = Progression.Load(configuration);

    if (progression is null) ProgressionPrompts.Intro(progression = new Progression(), configuration);
    else ProgressionPrompts.Greeting(progression);

    // Play game
    RunInterface(progression, configuration);
}

void RunInterface(Progression progression, Configuration configuration)
{
    // Get the command the player wants to execute.
    var command = AnsiConsole.Ask<Command>(">");
    
    // Map the command to the corresponding action
    switch (command)
    {
        case Command.Help:
            HelpPrompts.GetCommandHelp<Command>();
            break;
        case Command.Battle:
            BattlePrompts.Battle(progression.Profile);
            break;
        case Command.Team:
            TeamPrompts.GetTeam(progression.Profile.Name, progression.Profile.Pokemon);
            break;
        case Command.Heal:
            TeamPrompts.HealTeam(progression.Profile.Name, progression.Profile.Pokemon);
            break;
        case Command.Level:
            PokemonPrompts.LevelPokemon(progression.Profile.Pokemon);
            break;
        case Command.Inspect:
            PokemonPrompts.GetPokemon(progression.Profile.Pokemon);
            break;
        case Command.Save:
            ProgressionPrompts.Save(progression, configuration);
            break;
        case Command.Reset:
            if (ProgressionPrompts.Reset(progression, configuration))
            {
                Run();
                Environment.Exit(0);
            }

            break;
        case Command.Exit:
            return;
    }
    
    // Save the game
    Progression.Save(progression, configuration);

    // Re-run the interface until the player decides to exist.
    RunInterface(progression, configuration);
}

