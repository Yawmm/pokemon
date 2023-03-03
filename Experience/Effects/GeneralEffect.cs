using Game.Companions;
using Newtonsoft.Json;
using Spectre.Console;

namespace Game.Experience.Effects;

/// <summary>
/// A class used to apply general effects to <see cref="Pokemon"/> that level up.
/// </summary>
[JsonObject]
public class GeneralEffect: ILevelEffect
{
    /// <inheritdoc cref="ILevelEffect.Execute"/>
    public void Execute(Pokemon actor, int oldLevel, int newLevel)
    {
        actor.Statistics.Recalculate(actor);
        AnsiConsole.MarkupLine($"[{Colors.Pokemon}]{actor.Name}[/] leveled up from level {oldLevel} to level {newLevel}!");
    }
}