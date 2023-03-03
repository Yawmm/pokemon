using System.Text.RegularExpressions;

namespace Game.Inputs.Extensions;

/// <summary>
/// A helper class used to extend the <see cref="String"/> class.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Convert a string to more readable text (spaces after every capital letter).
    /// </summary>
    /// <param name="text">The text that should be converted.</param>
    /// <returns>The converted text.</returns>
    public static string ToReadableText(this string text)
        => Regex.Replace(text, "(\\B[A-Z])", " $1");
}