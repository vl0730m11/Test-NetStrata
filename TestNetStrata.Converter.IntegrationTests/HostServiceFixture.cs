using TestNetStrata.Converter.Services.ParseClassesServices;
using TestNetStrata.Converter.Services.TypeScriptExportServices;
using TestNetStrata.Converter;
using Moq;

public class HostServiceFixture
{
  public IParseClassesService ParseClassesService { get; }
  public ITypeScriptExportService TypeScriptExportService { get; }
  public HostService HostService { get; }

  public HostServiceFixture()
  {
    // Here, use mocks or real implementations as appropriate
    ParseClassesService = new ParseClassesService();
    TypeScriptExportService = new TypeScriptExportService();

    //ParseClassesService = parseClassesServiceMock.Object;
    //TypeScriptExportService = typeScriptExportServiceMock.Object;

    HostService = new HostService(ParseClassesService, TypeScriptExportService);
  }
}