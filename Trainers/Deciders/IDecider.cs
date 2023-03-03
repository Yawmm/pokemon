namespace Game.Trainers.Deciders;

/// <summary>
/// An interface representing an object which can decide a value from a given range of options.
/// </summary>
public interface IDecider
{
    /// <summary>
    /// Choose multiple values from a given range of options.
    /// </summary>
    /// <param name="message">The message which should be logged when deciding the final options.</param>
    /// <param name="options">The options from which multiple can be selected.</param>
    /// <typeparam name="T">The type of the options.</typeparam>
    /// <returns>The chosen values.</returns>
    IEnumerable<T> Multiple<T>(string message, IEnumerable<T> options) 
        where T : notnull;
    
    /// <summary>
    /// Choose a single values from a given range of options.
    /// </summary>
    /// <param name="message">The message which should be logged when choosing the final option.</param>
    /// <param name="options">The options from which one can be selected.</param>
    /// <typeparam name="T">The type of the options.</typeparam>
    /// <returns>The chosen value.</returns>
    T Single<T>(string message, IEnumerable<T> options) 
        where T : notnull;

    /// <summary>
    /// Choose a yes or no value.
    /// </summary>
    /// <param name="message">The message which should be logged when choosing the final option.</param>
    /// <returns>The yes or no value.</returns>
    bool Boolean(string message);
}