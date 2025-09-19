using System.Text;
using TestNetStrata.Converter.Extensions;
using TestNetStrata.Converter.Models;

namespace TestNetStrata.Converter.Services.TypeScriptExportServices
{
  public class TypeScriptExportService : ITypeScriptExportService
  {
    public string GenerateTypeScriptInterfaces(IEnumerable<ClassDefinition> classes)
    {
      var sb = new StringBuilder();
      foreach (var cls in classes)
      {
        sb.AppendLine($"export interface {cls.Name} {{");
        foreach (var prop in cls.Properties)
        {
          string tsProp = prop.ToTypeScript();
          sb.AppendLine(tsProp);
        }
        sb.AppendLine("}");
      }
      return sb.ToString().TrimEnd();
    }
  }
}
