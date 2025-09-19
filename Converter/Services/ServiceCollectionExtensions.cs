using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestNetStrata.Converter.Services.ParseClassesServices;
using TestNetStrata.Converter.Services.TypeScriptExportServices;

namespace TestNetStrata.Converter.Services
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddScoped<IParseClassesService, ParseClassesService>();
      services.AddScoped<ITypeScriptExportService, TypeScriptExportService>();

      return services;
    }
  }
}
