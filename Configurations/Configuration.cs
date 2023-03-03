using Microsoft.Extensions.Configuration;

namespace Game.Configurations;

/// <summary>
/// A class used to configure settings in the game.
/// </summary>
public class Configuration
{
    /// <summary>
    /// The path to which save files will be written.
    /// </summary>
    public string SaveFilePath { get; set; } = null!;

    /// <summary>
    /// Initialize the application configuration from a given path.
    /// </summary>
    /// <param name="path">The path of the application configuration.</param>
    /// <returns>The application settings.</returns>
    public static Configuration Get(string path)
    {
        var build = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path)
            .Build();

        // Deserialize the JSON to a usable object.
        var instance = new Configuration();
        build.GetRequiredSection("Configuration").Bind(instance);
        
        return instance;
    }
}