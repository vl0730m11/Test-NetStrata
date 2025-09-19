using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNetStrata.Converter.Models
{
  public class PropertyDefinition
  {
    public string Name { get; }
    public string Type { get; }

    public PropertyDefinition(string name, string type)
    {
      Name = name;
      Type = type;
    }
  }
}
