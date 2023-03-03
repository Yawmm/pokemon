using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace Game.Inputs.Extensions;

/// <summary>
/// A helper class used to extend <see cref="Enum"/> classes.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// A cache for retrieved descriptions.
    /// </summary>
    private static readonly ConcurrentDictionary<string, string?> DescriptionCache = new ();

    /// <summary>
    /// Get the description of an <see cref="Enum"/> value by a <see cref="DescriptionAttribute"/> via reflection. 
    /// </summary>
    /// <param name="value">The <see cref="Enum"/> value from which the descriptions should be extracted.</param>
    /// <returns>The description of the enum value</returns>
    public static string? Description(this Enum value)
    {
        // Get type name
        var type = value.GetType();
        var key = $"{type.FullName}.{value}";

        // Get description
        var description = DescriptionCache.GetOrAdd(key, _ =>
        {
            // Get field from enum
            var field = type
                .GetTypeInfo()
                .GetField(value.ToString());

            if (field is null)
                return null;
            
            // Get attribute
            var attribute = ((DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false))
                .FirstOrDefault();

            // Return attribute value
            return attribute?.Description;
        });

        return description;
    }
}