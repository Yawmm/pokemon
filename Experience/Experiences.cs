namespace Game.Experience;

/// <summary>
/// A helper class used to calculate experience values.
/// Uses the fast experience group, see https://bulbapedia.bulbagarden.net/wiki/Experience#Fast
/// </summary>
public static class Experiences
{
    /// <summary>
    /// Calculate the level by the given <see cref="experience"/>.
    /// </summary>
    /// <param name="experience">The experience from which a level should be calculated.</param>
    /// <returns>The level extracted from the <see cref="experience"/>.</returns>
    public static int ToLevel(double experience)
        => (int)Math.Floor(Math.Pow((experience + 1) * 5 / 4, 1.0 / 3.0));

    /// <summary>
    /// Calculate the experience amount by the given <see cref="level"/>.
    /// </summary>
    /// <param name="level">The level from which the experience should be calculated.</param>
    /// <returns>The experience extracted from the <see cref="level"/>.</returns>
    public static int FromLevel(int level)
        => (int)Math.Floor(4 * Math.Pow(level, 3) / 5);
}