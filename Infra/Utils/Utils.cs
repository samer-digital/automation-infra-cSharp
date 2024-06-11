using System.Reflection;
using System.Text.RegularExpressions;

public static class Utils
{
    private static readonly Random _random = new Random();
    private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    
    /// <summary>
    /// Extracts the first word from a string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>The first word in the string.</returns>
    public static string ExtractFirstWord(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        string[] words = input.Split(' ');
        return words[0];
    }

    /// <summary>
    /// Extracts the first match of a regex pattern from a string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="pattern">The regex pattern.</param>
    /// <returns>The first match found in the string.</returns>
    public static string ExtractFirstRegexMatch(string input, string pattern)
    {
        if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(pattern))
        {
            return string.Empty;
        }

        Regex regex = new Regex(pattern);
        Match match = regex.Match(input);

        return match.Success ? match.Value : string.Empty;
    }

    /// <summary>
    /// Waits for a specified condition to be true, checking at regular intervals.
    /// </summary>
    /// <param name="condition">The condition to wait for.</param>
    /// <param name="timeout">The maximum time to wait, in milliseconds.</param>
    /// <param name="interval">The interval between checks, in milliseconds.</param>
    /// <returns>True if the condition is met within the timeout period, otherwise false.</returns>
    public static async Task<bool> WaitForConditionAsync(Func<Task<bool>> condition, int timeout = 30000, int interval = 500)
    {
        var endTime = DateTime.Now.AddMilliseconds(timeout);

        while (DateTime.Now < endTime)
        {
            if (await condition())
            {
                return true;
            }

            await Task.Delay(interval);
        }

        return false;
    }

    public static IDictionary<string, string>? ToDictionary(this object source)
    {
        if (source == null) return null;

        var dictionary = new Dictionary<string, string>();
        foreach (PropertyInfo property in source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            var value = property.GetValue(source);
            if (value != null)
            {
                string? valueString = value.ToString();
                if (valueString != null)
                {
                    dictionary.Add(property.Name, valueString);
                }
            }
        }

        return dictionary;
    }

    public static string GenerateRandomString(int length)
    {
        var result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = Characters[_random.Next(Characters.Length)];
        }
        return new string(result);
    }
}