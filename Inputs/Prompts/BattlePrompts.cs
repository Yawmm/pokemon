using Game.Battles;
using Game.Battles.Actions;
using Game.Battles.Input;
using Game.Battles.Rounds;
using Game.Battles.Turns;
using Game.Battles.Turns.Checks;
using Game.Battles.Turns.Cleanups;
using Game.Content;
using Game.Moves.Checks;
using Game.Trainers;
using Spectre.Console;

namespace Game.Inputs.Prompts;

/// <summary>
/// A prompt class used to create an interface between the user and the game.
/// </summary>
public static class BattlePrompts
{
    /// <summary>
    /// Start a new <see cref="Battles.Battle"/>.
    /// </summary>
    /// <param name="player">The player <see cref="Trainer"/> who should be in the <see cref="Battles.Battle"/>.</param>
    public static void Battle(Trainer player)
    {
        // Check if the player has any pokemon which are still alive
        if (player.Pokemon.All(m => m.Whiteout))
        {
            AnsiConsole.MarkupLine($"You are not able to enter a new [{Colors.Battle}]battle[/], everyone is dead!");
            return;
        }

        // Confirm that the player wants to start a battle
        AnsiConsole.MarkupLine($"[{Colors.Battle}]Battle[/] against other [{Colors.Trainer}]trainers[/] to get more [{Colors.Pokemon}]pokemon[/] and level up your own.");
        if (!AnsiConsole.Confirm($"Are you sure you want to initiate a [{Colors.Battle}]battle[/]? You cannot go back when you're in."))
            return;

        // Get the opponent to whom the player wants to fight
        AnsiConsole.WriteLine();
        var opponent = AnsiConsole.Prompt(
            new SelectionPrompt<Trainer>()
                .Title($"Which [{Colors.Opponent}]opponent[/] will you fight against?")
                .AddChoices(
                    OpponentList.Lian(), 
                    OpponentList.Benga(), 
                    OpponentList.Arven(), 

                    OpponentList.Hassel(), 
                    OpponentList.Janin(), 
                    OpponentList.Ash(), 

                    OpponentList.Elesa(), 
                    OpponentList.Grant(), 
                    OpponentList.Misty(), 

                    OpponentList.Jeff(),
                    OpponentList.Akari(),
                    OpponentList.Oak()
                )
        );

        var opponentTeam = new Team(
            owner: opponent,
            members: opponent.Pokemon,
            actor: opponent.Decider.Single($"Which [{Colors.Pokemon}]pokemon[/] will be the first in your team?", 
                opponent.Pokemon)
        );
        

        // Get the team of the player
        AnsiConsole.MarkupLine($"[{Colors.Trainer}]{opponent.Name}[/] will be your next [{Colors.Opponent}]opponent[/].");
        var playerTeam = new Team(
            owner: player,
            members: player.Pokemon,
            actor: player.Decider.Single($"Which [{Colors.Pokemon}]pokemon[/] will be the first in your team?",
                player.Pokemon.Where(m => !m.Whiteout))
        );

        // Log battle information
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"[{Colors.Pokemon}]{playerTeam.Actor.Name}[/] will be [{Colors.Trainer}]{player.Name}[/]'s first [{Colors.Pokemon}]pokemon[/].");
        AnsiConsole.MarkupLine($"[{Colors.Pokemon}]{opponentTeam.Actor.Name}[/] will be [{Colors.Trainer}]{opponent.Name}[/]'s first [{Colors.Pokemon}]pokemon[/].");

        // Execute battle loop
        Execute(playerTeam, opponentTeam);
    }

    /// <summary>
    /// Execute the <see cref="Battles.Battle"/> for each of the two <see cref="Team"/>.
    /// </summary>
    /// <param name="player">The <see cref="Team"/> of the player <see cref="Trainer"/>.</param>
    /// <param name="opponent">The <see cref="Team"/> of the opponent <see cref="Trainer"/>.</param>
    private static void Execute(Team player, Team opponent)
    {
        // Initialize battle
        var battle = new Battle
        {
            Player = player,
            Opponent = opponent,

            TurnChecks = new ITurnCheck[] { new ContinueTurnCheck() },
            TurnCleanups = new ITurnCleanup[] { new BurnCleanup(), new PoisonCleanup() }
        };

        battle.Begin();

        // Run battle loop
        while (battle.Result is null)
        {
            // Get player turn
            if (!CheckTurn(battle.Player, battle, out var playerMove))
                playerMove = PlayerTurn(battle);
            
            // Get opponent turn
            if (!CheckTurn(battle.Opponent, battle, out var opponentMove))
                opponentMove = OpponentTurn(battle);

            // Execute turns
            var result = battle.Execute(playerMove!, opponentMove!);
            if (result is not null)
                battle.End(result);
        }

        // Cleanup battle
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine(battle.Result.Message);
    }

    /// <summary>
    /// Check whether or not a given <see cref="ITurn"/> can proceed, or whether a previous
    /// <see cref="ITurn"/> needs to continue this <see cref="Round"/>.
    /// </summary>
    /// <param name="team">The <see cref="Team"/> on which the list of <see cref="ITurnCheck"/> need to be executed.</param>
    /// <param name="battle">The context of the current <see cref="Battles.Battle"/>.</param>
    /// <param name="turn">The <see cref="ITurn"/> that meet the check..</param>
    /// <returns>Whether or not the checks failed.</returns>
    private static bool CheckTurn(Team team, Battle battle, out ITurn? turn)
    {
        turn = null;
        
        foreach (var check in battle.TurnChecks)
        {
            // Get check result
            var result = check.Execute(team, battle);
            if (result is null)
                continue;

            AnsiConsole.WriteLine();
            
            turn = result;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Get the <see cref="ITurn"/> that the player chooses to execute.
    /// </summary>
    /// <param name="battle">The context of the current <see cref="Battles.Battle"/>.</param>
    /// <returns>The <see cref="ITurn"/> of the player.</returns>
    private static ITurn PlayerTurn(Battle battle)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"What will you do during your turn, [{Colors.Trainer}]trainer[/]? [grey]use the 'help' command for extra information[/]");
        while (true)
        {
            var action = GetPlayerAction(battle);
            switch (action)
            {
                // No action was given, continue the loop
                case null:
                    continue;
                
                // Turn was given, exit the loop
                case ITurn turn:
                    return turn;
                
                // Execute the given action, continue the loop
                default:
                    action.Execute(battle);
                    break;
            }
        }
    }

    /// <summary>
    /// Get the <see cref="IAction"/> that the player chooses to execute.
    /// </summary>
    /// <param name="battle">The context of the current <see cref="Battles.Battle"/>.</param>
    /// <returns>The <see cref="IAction"/> of the player.</returns>
    private static IAction? GetPlayerAction(Battle battle)
        => AnsiConsole.Ask(">", BattleCommand.Move) switch
        {
            // Repeatable actions
            BattleCommand.Help => new HelpAction(),
            BattleCommand.Inspect => new InspectAction(),

            // Turns
            BattleCommand.Retreat => new RetreatTurn { Team = battle.Player },
            BattleCommand.Swap => new SwapTurn { Team = battle.Player },
            BattleCommand.Move => new MoveTurn
            {
                Checks = new [] { new WhiteoutCheck() },
                
                Team = battle.Player,
                Actor = battle.Player.Actor,
                Target = battle.Opponent,
                Move = battle.Player.Owner.Decider.Single(
                    $"Which [{Colors.Move}]move[/] will [{Colors.Trainer}]{battle.Player.Owner.Name}[/] choose to use from their [{Colors.Pokemon}]{battle.Player.Actor}[/]?", 
                    battle.Player.Actor.Moves.Value
                )
            },
            _ => null
        };
    
    /// <summary>
    /// Get the <see cref="ITurn"/> that the opponent chooses to execute.
    /// </summary>
    /// <param name="battle">The context of the current <see cref="Battles.Battle"/>.</param>
    /// <returns>The <see cref="ITurn"/> of the opponent.</returns>
    private static ITurn OpponentTurn(Battle battle)
        => new MoveTurn
        {
            Checks = new[] { new WhiteoutCheck() },

            Team = battle.Opponent,
            Actor = battle.Opponent.Actor,
            Target = battle.Player,
            Move = battle.Opponent.Owner.Decider.Single(
                $"Which [{Colors.Move}]move[/] will [{Colors.Trainer}]{battle.Opponent.Owner.Name}[/] choose to use from their [{Colors.Pokemon}]{battle.Opponent.Actor}[/]?", 
                battle.Opponent.Actor.Moves.Value
            )
        };
}