using Game.Companions;
using Newtonsoft.Json;
using Spectre.Console;

namespace Game.Experience.Effects;

/// <summary>
/// A class used to apply new moves to <see cref="Pokemon"/> which meet the requirements.
/// </summary>
[JsonObject]
public class LearnEffect : ILevelEffect
{
    /// <inheritdoc cref="ILevelEffect.Execute"/>
    public void Execute(Pokemon actor, int oldLevel, int newLevel)
    {
        // Get available moves
        var learnableMoves = actor.Moves.LearnSet.Check(actor.Experience.Level);
        var newMoves = learnableMoves.Where(m => actor.Moves.Value.All(v => v.Name != m.Name));

        foreach (var move in newMoves)
        {
            // Replace move since a pokemon can only have four moves at a time.
            if (actor.Moves.Value.Count > 3)
            {
                // Check if the owner wants to forget a move to learn this new move.
                if (!actor.Owner.Decider.Boolean($"[{Colors.Pokemon}]{actor.Name}[/] is able to learn [{Colors.Move}]{move.Name}[/], but has no available slots, do you wish to forget a [{Colors.Move}]move[/] in order to learn this [{Colors.Move}]move[/]?"))
                {
                    actor.Moves.LearnSet.Value.Remove(actor.Moves.LearnSet.Value
                        .First(t => t.Value.Any(m => m.Name == move.Name))
                        .Key
                    );

                    continue;
                }

                // Forget old move
                var forgotten = actor.Owner.Decider.Single(
                    $"Which [{Colors.Move}]move[/] will [{Colors.Pokemon}]{actor.Name}[/] forget?",
                    actor.Moves.Value);

                actor.Moves.Value.Remove(forgotten);
                AnsiConsole.MarkupLine($"[{Colors.Pokemon}]{actor.Name}[/] forgot the move [{Colors.Move}]{forgotten.Name}[/]!");
            }

            // Remove move from learn set
            actor.Moves.LearnSet.Value.Remove(
                actor.Moves.LearnSet.Value
                    .First(t => t.Value.Any(m => m.Name == move.Name))
                    .Key
            );

            // Add new move
            actor.Moves.Value.Add(move);

            AnsiConsole.MarkupLine($"[{Colors.Pokemon}]{actor.Name}[/] learned the move [{Colors.Move}]{move.Name}[/]!");
        }
    }
}