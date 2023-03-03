# Pokemon
When you finished the initialization, type the command `help` to see all the available commands.
The commands here are different when compared to those during a battle. During a battle you cannot force your pokemon to level up, or to heal them.

### Initialization
Clone the project and edit the `appsettings.json` which should look something like this:
```json
{
  "Configuration": {
    "SaveFilePath": "..."
  }
}
```

Here you will want to edit the `SaveFilePath` to be a path on your computer where the game can write a `.json` save file to.

### Content
Since this is not a full game, there is a limited amount of content. The following pokemon (and their evolutions) are available:
1. Bulbasaur
2. Charmander
3. Squirtle

You can personally add more pokemon in the `PokemonList.cs` file, and more moves in the `PokemonMoveList.cs` and the `LearnSetList.cs` file.
The only moves that are available are those that are in the learn sets of the available pokemon.

Opponents can also be tweaked and added in the `OpponentList.cs` file. The included opponents are a range of opponents that each have one of the available pokemon. 

The range of available natures is also limited and does not include most of the natures in actual pokemon games.