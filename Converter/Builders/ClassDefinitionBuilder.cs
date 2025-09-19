using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestNetStrata.Converter.Extensions;
using TestNetStrata.Converter.Models;

namespace TestNetStrata.Converter.Builders
{
  public class ClassDefinitionBuilder
  {
    private readonly ClassDefinition classDef;

    public ClassDefinitionBuilder(string className)
    {
      classDef = new ClassDefinition(className);
    }

    public ClassDefinitionBuilder WithProperties(IEnumerable<string> propLines, Regex propertyRegex)
    {
      foreach (var line in propLines)
      {
        var prop = propertyRegex.ParseProperty(line);
        if (prop != null)
          classDef.Properties.Add(prop);
      }
      return this;
    }

    public ClassDefinition Build()
    {
      return classDef;
    }
  }
}
