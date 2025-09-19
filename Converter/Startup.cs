using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNetStrata.Converter.Services;

namespace TestNetStrata.Converter
{
  public static class Startup
  {
    public static void SetUp(HostBuilderContext hostBuilder, IServiceCollection services)
    {
      var configuration = hostBuilder.Configuration;
      services.AddServices(configuration);
    }
  }
}
