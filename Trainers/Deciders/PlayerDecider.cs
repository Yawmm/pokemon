using Spectre.Console;

namespace Game.Trainers.Deciders;

/// <summary>
/// An <see cref="IDecider"/> which lets the player choose a value from the console.
/// </summary>
public record PlayerDecider : IDecider
{
    /// <inheritdoc cref="IDecider.Multiple{T}"/>
    public IEnumerable<T> Multiple<T>(string message, IEnumerable<T> options) 
        where T : notnull 
        => AnsiConsole.Prompt(
            new MultiSelectionPrompt<T>()
                .Title(message)
                .AddChoices(options)
        );

    /// <inheritdoc cref="IDecider.Single{T}"/>
    public T Single<T>(string message, IEnumerable<T> options)
        where T : notnull 
        => AnsiConsole.Prompt(
            new SelectionPrompt<T>()
                .Title(message)
                .AddChoices(options)
        );

    /// <inheritdoc cref="IDecider.Boolean"/>
    public bool Boolean(string message)
        => AnsiConsole.Confirm(message);
}