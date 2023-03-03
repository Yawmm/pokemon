namespace Game.Trainers.Deciders;

/// <summary>
/// An <see cref="IDecider"/> which chooses a random value.
/// </summary>
public class RandomDecider : IDecider
{
    /// <inheritdoc cref="IDecider.Multiple{T}"/>
    public IEnumerable<T> Multiple<T>(string message, IEnumerable<T> options) where T : notnull
    {
        var result = new List<T>();
        
        var original = options.ToList();
        var amount = Random.Shared.Next(1, original.Count);
        
        for (var i = 0; i < amount; i++)
        {
            var item = original.OrderBy(_ => Guid.NewGuid())
                .First();
            
            result.Add(item);
        }

        return result;
    }

    /// <inheritdoc cref="IDecider.Single{T}"/>
    public T Single<T>(string message, IEnumerable<T> options) where T : notnull
    {
        var original = options.ToList();
        return original[Random.Shared.Next(0, original.Count - 1)];
    }

    /// <inheritdoc cref="IDecider.Boolean"/>
    public bool Boolean(string message)
        => Random.Shared.Next(0, 2) == 0;
}