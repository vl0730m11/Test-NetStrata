using Microsoft.Extensions.Hosting;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestNetStrata.Converter.Builders;
using TestNetStrata.Converter.Models;
using TestNetStrata.Converter.Services.ParseClassesServices;
using TestNetStrata.Converter.Services.TypeScriptExportServices;

namespace TestNetStrata.Converter.Tests
{
  public class HostServiceTests
  {
    private readonly HostService hostService;
    private readonly Mock<IParseClassesService> mockParseClassesService = new();
    private readonly Mock<ITypeScriptExportService> mockTypeScriptExportService = new();
    private static readonly Regex PropertyRegex = new Regex(@"public\s+([\w\?\<\>]+)\s+(\w+)\s*\{\s*get;\s*set;\s*\}", RegexOptions.Compiled);

    public HostServiceTests()
    {
      hostService = new HostService(mockParseClassesService.Object, mockTypeScriptExportService.Object);
    }

    [Fact]
    public async void Convert_ReturnTypeScript_WhenPassingCSharpObject()
    {
      var cSharpObject = @"
        ""public class PersonDto
        {
          public string Name { get; set; }
          public List<Address> Addresses { get; set; }
          public class Address
          {
            public int StreetNumber { get; set; }
          }
        }""
      ";

      var typeScriptObjects = @"
        export interface PersonDto {
          name: string;
          addresses: Address[];
        }
        export interface Address {
          streetNumber: number;
        }
      ";

      var personClass = new ClassDefinitionBuilder("PersonDto")
        .WithProperties(new List<string> { "public string Name { get; set; }" }, PropertyRegex)
        .WithProperties(new List<string> { "public List<Address> Addresses { get; set; }" }, PropertyRegex)
        .Build();

      var addressClass = new ClassDefinitionBuilder("Address")
        .WithProperties(new List<string> { "public int StreetNumber { get; set; }" }, PropertyRegex)
        .Build();

      var classes = new List<ClassDefinition>() {
        personClass,
        addressClass,
      };

      mockParseClassesService.Setup(x => x.ParseClasses(cSharpObject))
        .Returns(classes);
      mockTypeScriptExportService.Setup(x => x.GenerateTypeScriptInterfaces(classes))
        .Returns(typeScriptObjects);

      var result = hostService.Convert(cSharpObject);

      result.ShouldBe(typeScriptObjects);
    }
  }
}
