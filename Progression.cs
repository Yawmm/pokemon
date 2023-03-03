using Game.Configurations;
using Game.Trainers;
using Newtonsoft.Json;

namespace Game;

/// <summary>
/// A class representing the progression of the player.
/// </summary>
[Serializable]
[JsonObject]
public class Progression
{
    /// <summary>
    /// The <see cref="Trainer"/> profile of the player.
    /// </summary>
    public Trainer Profile { get; set; } = null!;
    
    /// <summary>
    /// Settings used for the serialization, and deserialization, of the <see cref="Progression"/> object.
    /// </summary>
    private static JsonSerializerSettings Settings => new()
    {
        TypeNameHandling = TypeNameHandling.Auto
    };
    
    /// <summary>
    /// Load a save game from the given <see cref="Configuration"/> save path.
    /// </summary>
    /// <param name="configuration">The <see cref="Configuration"/> from which the save path will be used.</param>
    /// <returns>The deserialization <see cref="Progression"/> object.</returns>
    public static Progression? Load(Configuration configuration)
        => File.Exists(configuration.SaveFilePath)
            ? JsonConvert.DeserializeObject<Progression>(File.ReadAllText(configuration.SaveFilePath), Settings)
            : null;

    /// <summary>
    /// Save a game to the given save path.
    /// </summary>
    /// <param name="progression">The <see cref="Progression"/> object which should be serialized.</param>
    /// <param name="configuration">The <see cref="Configuration"/> from which the save path will be used.</param>
    public static void Save(Progression progression, Configuration configuration)
    {
        var json = JsonConvert.SerializeObject(progression, Settings);
        File.WriteAllText(configuration.SaveFilePath, json);
    }

    /// <summary>
    /// Remove a save game from the given <see cref="Configuration"/> save path.
    /// </summary>
    /// <param name="configuration">The <see cref="Configuration"/> from which the save path will be used.</param>
    public static void Remove(Configuration configuration)
    {
        if (!File.Exists(configuration.SaveFilePath) || Load(configuration) is null)
            return;
     
        File.Delete(configuration.SaveFilePath);
    }
}