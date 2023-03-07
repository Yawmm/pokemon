using System.Globalization;
using Game.Companions;
using Game.Experience;
using Game.Inputs.Extensions;
using Game.Moves;
using Game.Stats;
using Game.Statuses;
using Spectre.Console;

namespace Game.Inputs.Prompts;

/// <summary>
/// A prompt class used to inform the player about the <see cref="Pokemon"/> they have.
/// </summary>
public static class PokemonPrompts
{
    /// <summary>
    /// Increase the level of a <see cref="Pokemon"/>.
    /// </summary>
    /// <param name="options">The list of <see cref="Pokemon"/> that the player can choose from.</param>
    public static void LevelPokemon(IEnumerable<Pokemon> options)
    {
        var target = AnsiConsole.Prompt(
            new SelectionPrompt<Pokemon>()
                .Title($"Which [{Colors.Pokemon}]pokemon[/] would you like to level up?")
                .AddChoices(options)
        );

        // Add the experience difference between the current level, and the next level
        var nextLevelExperience = Experiences.FromLevel(target.Experience.Level + 1) - target.Experience.Value;
        target.Experience.AddExperience(target, nextLevelExperience);
    }
    
    /// <summary>
    /// Get information about a <see cref="Pokemon"/>.
    /// </summary>
    /// <param name="options">The list of <see cref="Pokemon"/> that the player can choose from.</param>
    public static void GetPokemon(IEnumerable<Pokemon> options)
    {
        var moreDetails = AnsiConsole.Prompt(
            new SelectionPrompt<Pokemon>()
                .Title($"Which [{Colors.Pokemon}]pokemon[/] would you like to see more details of?")
                .AddChoices(options)
        );

        // Log basic information
        AnsiConsole.MarkupLine($"[{Colors.Pokemon} underline]{moreDetails.Name}[/]");

        AnsiConsole.MarkupLine($"[white bold]Types[/] - [silver]{string.Join(',', moreDetails.Types)}[/]");
        AnsiConsole.MarkupLine($"[white bold]Nature[/] - [silver]{moreDetails.Nature.Type}[/]");
        AnsiConsole.MarkupLine($"[white bold]Level[/] - [silver]{moreDetails.Experience.Level}[/]");
        AnsiConsole.MarkupLine($"[white bold]Experience[/] - [silver]{moreDetails.Experience.Value:F1}[/] [grey](next level at {Experiences.FromLevel(moreDetails.Experience.Level + 1)} experience)[/]");
        AnsiConsole.MarkupLine($"[white bold]Health[/] - [silver]{moreDetails.Stats[Stat.Health]:F1}[/]");

        // Log information only available in a battle context
        if (moreDetails.StatusConditions.Any())
            LogStatusConditions(moreDetails);
        
        if (moreDetails.Stages.IsActive())
            LogStages(moreDetails);

        // Log basic statistics 
        if (moreDetails.Moves.Value.Any())
            LogMoves(moreDetails);

        LogStats(moreDetails);

        if (moreDetails.Statistics.EVs.Any(s => s.Value != 0))
            LogEVs(moreDetails);

        LogIVs(moreDetails);
    }

    /// <summary>
    /// Get information about all the <see cref="PokemonStatus"/> a <see cref="Pokemon"/> has.
    /// </summary>
    /// <param name="moreDetails">The <see cref="Pokemon"/> from which the information will be logged.</param>
    private static void LogStatusConditions(Pokemon moreDetails)
    {
        var statusConditions = new Table { Border = TableBorder.Rounded }
            .AddColumns("Type")
            .BorderColor(Color.White)
            .LeftAligned();

        foreach (var statusCondition in moreDetails.StatusConditions)
        {
            var title = statusCondition.ToString().ToReadableText();
            statusConditions.AddRow($"[{Colors.Status}]{title}[/]");
        }

        AnsiConsole.MarkupLine($"\n[{Colors.Status} underline]Status Conditions[/]");
        AnsiConsole.Write(statusConditions);
    }
    
    /// <summary>
    /// Get information about the <see cref="StageList"/> a <see cref="Pokemon"/> has.
    /// </summary>
    /// <param name="moreDetails">The <see cref="Pokemon"/> from which the information will be logged.</param>
    private static void LogStages(Pokemon moreDetails)
    {
        var stages = new Table { Border = TableBorder.Rounded }
            .AddColumns("Stat", "Stage")
            .BorderColor(Color.White)
            .LeftAligned();

        foreach (var (stat, stage) in moreDetails.Stages.Value.Where(s => s.Value != 0))
        {
            var title = stat.ToString().ToReadableText();
            stages.AddRow($"[white bold]{title}[/]", stage.ToString(CultureInfo.InvariantCulture));
        }

        AnsiConsole.MarkupLine("\n[white bold underline]Stages[/]");
        AnsiConsole.Write(stages);
    }
    
    /// <summary>
    /// Get information about all the <see cref="PokemonMove"/> a <see cref="Pokemon"/> has.
    /// </summary>
    /// <param name="moreDetails">The <see cref="Pokemon"/> from which the information will be logged.</param>
    private static void LogMoves(Pokemon moreDetails)
    {
        AnsiConsole.MarkupLine("\n[white bold underline]Moves[/]");
        foreach (var move in moreDetails.Moves.Value)
        {
            AnsiConsole.MarkupLine($"\n[{Colors.Move}]{move.Name}[/]");
            AnsiConsole.MarkupLine($"[white italic]{move.Description}[/]");
            AnsiConsole.MarkupLine($"\n[white]Type[/] - [silver]{move.Type}[/]");
            AnsiConsole.MarkupLine($"[white]PP[/] - [silver]{move.PP}[/]");
            if (move.Power is not null)
                AnsiConsole.MarkupLine($"[white]Power[/] - [silver]{move.Power}[/]");
            if (move.Accuracy is not null)
                AnsiConsole.MarkupLine($"[white]Accuracy[/] - [silver]{move.Accuracy}[/]");
            if (move.Priority is not null)
                AnsiConsole.MarkupLine($"[white]Priority[/] - [silver]{move.Priority}[/]");
        }
    }

    /// <summary>
    /// Get information about the range of calculated <see cref="Stat"/> a <see cref="Pokemon"/> has.
    /// </summary>
    /// <param name="moreDetails">The <see cref="Pokemon"/> from which the information will be logged.</param>
    private static void LogStats(Pokemon moreDetails)
    {
        var stats = new Table { Border = TableBorder.Rounded }
            .AddColumns("Stat", "Value")
            .BorderColor(Color.White)
            .LeftAligned();

        foreach (var (stat, value) in moreDetails.Stats)
        {
            var title = stat.ToString().ToReadableText();
            stats.AddRow($"[darkblue bold]{title}[/]", double.Round(value, 1).ToString(CultureInfo.InvariantCulture));
        }

        AnsiConsole.MarkupLine("\n[white bold underline]Stats[/]");
        AnsiConsole.Write(stats);
    }

    /// <summary>
    /// Get information about the range of EV awards a <see cref="Pokemon"/> has.
    /// </summary>
    /// <param name="moreDetails">The <see cref="Pokemon"/> from which the information will be logged.</param>
    private static void LogEVs(Pokemon moreDetails)
    {
        var evs = new Table { Border = TableBorder.Rounded }
            .AddColumns("Stat", "Value")
            .BorderColor(Color.White)
            .LeftAligned();

        foreach (var (stat, value) in moreDetails.Statistics.EVs)
        {
            if (value == 0)
                continue;

            var title = stat.ToString().ToReadableText();
            evs.AddRow($"[darkred bold]{title}[/]", double.Round(value, 1).ToString(CultureInfo.InvariantCulture));
        }

        AnsiConsole.MarkupLine("\n[white bold underline]EVs[/]");
        AnsiConsole.Write(evs);
    }

    /// <summary>
    /// Get information about the range of random IVs a <see cref="Pokemon"/> has.
    /// </summary>
    /// <param name="moreDetails">The <see cref="Pokemon"/> from which the information will be logged.</param>
    private static void LogIVs(Pokemon moreDetails)
    {
        var ivs = new Table { Border = TableBorder.Rounded }
            .AddColumns("Stat", "Value")
            .BorderColor(Color.White)
            .LeftAligned();

        foreach (var (stat, value) in moreDetails.Statistics.IVs)
        {
            var title = stat.ToString().ToReadableText();
            ivs.AddRow($"[yellow bold]{title}[/]", double.Round(value, 1).ToString(CultureInfo.InvariantCulture));
        }

        AnsiConsole.MarkupLine("\n[white bold underline]IVs[/]");
        AnsiConsole.Write(ivs);
    }
}