using System.Text.RegularExpressions;

namespace TestNetStrata.Converter.Mappings
{
  public static class CSharpTypeToTypeScriptMapper
  {
    public static string Convert(string csharpType, out bool isNullable)
    {
      isNullable = false;

      // Handle nullable types
      if (csharpType.EndsWith("?"))
      {
        isNullable = true;
        csharpType = csharpType.TrimEnd('?');
      }

      // Handle List<T>
      var listMatch = Regex.Match(csharpType, @"List<(\w+)>");
      if (listMatch.Success)
      {
        string innerType = listMatch.Groups[1].Value;
        string tsInnerType = Convert(innerType, out _);
        return $"{tsInnerType}[]";
      }

      switch (csharpType)
      {
        case "string":
          return "string";
        case "int":
        case "long":
          return "number";
        default:
          // Assume nested class or unknown type, use as is
          return csharpType;
      }
    }
  }
}
