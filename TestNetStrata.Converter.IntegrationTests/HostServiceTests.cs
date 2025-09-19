using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestNetStrata.Converter.Services.ParseClassesServices;
using TestNetStrata.Converter.Services.TypeScriptExportServices;

namespace TestNetStrata.Converter.IntegrationTests
{
  public class HostServiceTests : IClassFixture<HostServiceFixture>
  {
    private readonly HostService hostService;

    public HostServiceTests(HostServiceFixture fixture)
    {
      hostService = fixture.HostService;
    }

    [Fact]
    public void Convert_ShouldGenerateTypeScriptInterfaces_FromCSharpClasses()
    {
      var input = @"
        ""public class PersonDto
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
        }""
      ";

      var expectedResult = @"export interface PersonDto {
            name: string;
            age: number;
            gender: string;
            driverLicenceNumber?: number;
            addresses: Address[];
            tests: Test[];
          }
          export interface Address {
            streetNumber: number;
            streetName: string;
            suburb: string;
            postCode: number;
          }
          export interface Test {
            fieldA: number;
          }";

      var res = hostService.Convert(input);

      res = NormalizeString(res);
      expectedResult = NormalizeString(expectedResult);
      res.ShouldBe(expectedResult);
    }

    public static string NormalizeString(string input)
    {
      input = input.Replace("\r", "").Replace("\n", "");
      input = Regex.Replace(input, @"\s+", "").Trim();
      return input;
    }
  }
}
