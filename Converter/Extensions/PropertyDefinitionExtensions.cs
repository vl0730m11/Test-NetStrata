using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNetStrata.Converter.Mappings;
using TestNetStrata.Converter.Models;

namespace TestNetStrata.Converter.Extensions
{
  public static class PropertyDefinitionExtensions
  {
    public static string ToTypeScript(this PropertyDefinition prop)
    {
      string tsName = prop.Name.ToCamelCase();
      string tsType = CSharpTypeToTypeScriptMapper.Convert(prop.Type, out bool isNullable);

      string optionalMark = isNullable ? "?" : "";

      return $"  {tsName}{optionalMark}: {tsType};";
    }
  }
}
