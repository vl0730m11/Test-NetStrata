using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNetStrata.Converter.Extensions;
using Xunit;

namespace TestNetStrata.Converter.Tests.Extensions
{
  public class StringExtensionsTests
  {
    [Theory]
    [InlineData("StandardCase", "standardCase")]
    [InlineData("A", "a")]
    [InlineData("", "")]
    public void ToCamelCase_ShouldReturnExpectedResult(string input, string expectedRes)
    {
      var result = input.ToCamelCase();
      result.ShouldBe(expectedRes);
    }

    [Theory]
    [InlineData("test{common{case}}", '{', 2)]
    [InlineData("t!est{com!mon{cas!e}}", '!', 3)]
    [InlineData("", '!', 0)]
    public void CountChar_ShouldReturnExpectedResult(string input, char c, int expectedCount)
    {
      var result = input.CountChar(c);
      result.ShouldBe(expectedCount);
    }
  }
}
