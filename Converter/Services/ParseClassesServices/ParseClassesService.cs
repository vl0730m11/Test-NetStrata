using System.Text.RegularExpressions;
using TestNetStrata.Converter.Builders;
using TestNetStrata.Converter.Extensions;
using TestNetStrata.Converter.Models;

namespace TestNetStrata.Converter.Services.ParseClassesServices
{
  public class ParseClassesService : IParseClassesService
  {
    private static readonly Regex ClassRegex = new Regex(@"public class (\w+)\s*\{([\s\S]*)\}", RegexOptions.Compiled);
    private static readonly Regex InnerClassRegex = new Regex(@"public class (\w+)\s*\{", RegexOptions.Compiled);
    private static readonly Regex PropertyRegex = new Regex(@"public\s+([\w\?\<\>]+)\s+(\w+)\s*\{\s*get;\s*set;\s*\}", RegexOptions.Compiled);
    public List<ClassDefinition> ParseClasses(string input)
    {
      var classes = new List<ClassDefinition>();

      var outerClass = ClassRegex.ExtractOuterClass(input);
      foreach (var kvp in outerClass)
      {
        var (props, nestedClassLines) = SplitClassBody(kvp.Value);

        var classDefinition = new ClassDefinitionBuilder(kvp.Key)
          .WithProperties(props, PropertyRegex)
          .Build();

        classes.Add(classDefinition);

        if (nestedClassLines.Any())
        {
          var nestedClass = ParseNestedClass(nestedClassLines);
          classes.AddRange(nestedClass);
        }
      }

      return classes;
    }

    public List<ClassDefinition> ParseNestedClass(List<string> nestedClassLines)
    {
      var nestedText = string.Join(Environment.NewLine, nestedClassLines);
      var result = new List<ClassDefinition>();
      var classes = InnerClassRegex.ExtractInnerClasses(nestedText);
      foreach (var kvp in classes)
      {
        var (props, _) = SplitClassBody(kvp.Value);

        var classDefinition = new ClassDefinitionBuilder(kvp.Key)
          .WithProperties(props, PropertyRegex)
          .Build();

        result.Add(classDefinition);
      }

      return result;
    }

    private (List<string> outerProps, List<string> nestedClassLines) SplitClassBody(string body) 
    {
      var lines = body.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();

      var outerProps = new List<string>();
      var nestedClassLines = new List<string>();
      bool insideNestedClass = false;
      int braceCount = 0;

      foreach (var line in lines)
      {
        string trimmed = line.Trim();

        if (trimmed.StartsWith("public class "))
        {
          insideNestedClass = true;
          braceCount = 1;
          nestedClassLines.Add(trimmed);
        }
        else if (insideNestedClass)
        {
          nestedClassLines.Add(trimmed);
          braceCount += trimmed.CountChar('{');
          braceCount -= trimmed.CountChar('}');

          if (braceCount == 0)
            insideNestedClass = false;
        }
        else
        {
          outerProps.Add(trimmed);
        }
      }

      return (outerProps, nestedClassLines);
    }
  }
}

