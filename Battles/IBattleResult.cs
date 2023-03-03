namespace Game.Battles;

/// <summary>
/// An interface representing a result of a <see cref="Battle"/>.
/// </summary>
public interface IBattleResult
{
    /// <summary>
    /// The message of the <see cref="IBattleResult"/>.
    /// </summary>
    public string Message { get; }
}