using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNetStrata.Converter.Models;

namespace TestNetStrata.Converter.Services.ParseClassesServices
{
  public interface IParseClassesService
  {
    List<ClassDefinition> ParseClasses(string input);
  }
}
