using System.Text.RegularExpressions;

namespace TestNetStrata.Converter.Extensions
{
  public static class StringExtensions
  {
    public static string ToCamelCase(this string name)
    {
      if (string.IsNullOrEmpty(name) || char.IsLower(name[0]))
        return name;

      if (name.Length == 1)
        return name.ToLower();

      return char.ToLower(name[0]) + name.Substring(1);
    }

    public static int CountChar(this string s, char c)
    {
      int count = 0;
      foreach (var ch in s)
      {
        if (ch == c) count++;
      }
      return count;
    }

    public static string GetBody(this string input, Match match)
    {
      var startIndex = match.Index + match.Length;
      int braceCount = 1;
      int currentIndex = startIndex;

      while (braceCount > 0 && currentIndex < input.Length)
      {
        if (input[currentIndex] == '{')
          braceCount++;
        if (input[currentIndex] == '}')
          braceCount--;
        currentIndex++;
      }

      // extract body (between braces at startIndex and currentIndex-1)
      return input.Substring(startIndex, (currentIndex - startIndex) - 1).Trim();
    }
  }
}
