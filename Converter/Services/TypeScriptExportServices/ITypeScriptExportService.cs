using TestNetStrata.Converter.Models;

namespace TestNetStrata.Converter.Services.TypeScriptExportServices
{
  public interface ITypeScriptExportService
  {
    string GenerateTypeScriptInterfaces(IEnumerable<ClassDefinition> classes);
  }
}
