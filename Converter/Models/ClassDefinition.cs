using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNetStrata.Converter.Models
{
  public class ClassDefinition
  {
    public string Name { get; }
    public List<PropertyDefinition> Properties { get; }

    public ClassDefinition(string name)
    {
      Name = name;
      Properties = new List<PropertyDefinition>();
    }
  }
}
