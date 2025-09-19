using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TestNetStrata.Converter.Services.ParseClassesServices;
using TestNetStrata.Converter.Services.TypeScriptExportServices;

namespace TestNetStrata.Converter
{
  public class HostService : IHostedService
  {
    private readonly IParseClassesService parseClassesService;
    private readonly ITypeScriptExportService typeScriptExportService;

    public HostService(IParseClassesService parseClassesService, ITypeScriptExportService typeScriptExportService)
    {
      this.parseClassesService = parseClassesService;
      this.typeScriptExportService = typeScriptExportService;
    }

    public string Convert(string input)
    {
      var classes = parseClassesService.ParseClasses(input);
      var res = typeScriptExportService.GenerateTypeScriptInterfaces(classes);

      return res;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      Console.WriteLine("Paste your multi-line text. Press Enter on an empty line to execute:");
      var lines = new List<string>();
      string? line;
      while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
      {
        lines.Add(line);
      }

      string input = string.Join(Environment.NewLine, lines);
      string result = Convert(input);
      Console.WriteLine("Converted Output:");
      Console.WriteLine(result);

      // Exit after completion
      Environment.Exit(0);
      return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}
