using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestNetStrata.Converter;

namespace CSharpToTypeScriptConverter
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
      /*
      "public class PersonDto
        {
          public string Name { get; set; }
          public int Age { get; set; }
          public string Gender { get; set; }
          public long? DriverLicenceNumber { get; set; }
          public List<Address> Addresses { get; set; }
          public List<Test> Tests { get; set; }
          public class Address
          {
            public int StreetNumber { get; set; }
            public string StreetName { get; set; }
            public string Suburb { get; set; }
            public int PostCode { get; set; }
          }
          public class Test
          {
            public int FieldA { get; set; }
          }
        }"
      */
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
      .ConfigureServices((hostBuilder, services) =>
      {
          Startup.SetUp(hostBuilder, services);
        services.AddHostedService<HostService>();
      });
  }
}