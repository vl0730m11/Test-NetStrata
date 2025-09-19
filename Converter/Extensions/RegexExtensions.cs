using System.Text.RegularExpressions;
using TestNetStrata.Converter.Models;

namespace TestNetStrata.Converter.Extensions
{
  public static class RegexExtensions
  {
    public static PropertyDefinition ParseProperty(this Regex regex, string line)
    {
      var match = regex.Match(line);
      if (!match.Success)
        return null;

      string type = match.Groups[1].Value;
      string name = match.Groups[2].Value;

      return new PropertyDefinition(name, type);
    }

    public static Dictionary<string, string> ExtractOuterClass(this Regex regex, string input)
    {
      var match = regex.Match(input);
      if (!match.Success)
        throw new ArgumentException("Input does not contain a valid class definition.");

      return new Dictionary<string, string>
      {
        [match.Groups[1].Value] = match.Groups[2].Value.Trim()
      };
    }

    public static Dictionary<string, string> ExtractInnerClasses(this Regex regex, string input)
    {
      var result = new Dictionary<string, string>();

      var matches = regex.Matches(input);
      foreach (Match match in matches)
      {
        var className = match.Groups[1].Value;
        var body = input.GetBody(match);

        result[className] = body;
      }

      return result;
    }
  }
}
