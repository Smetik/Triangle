using TriangleProduct.Models;
using TriangleProduct.Services;
using Xunit;

namespace TriangleProduct.Tests;

public sealed class TriangleServiceTests
{
    private readonly TriangleService _service = new();

    [Fact]
    public void Analyze_ReturnsEquilateral_ForEqualSides()
    {
        var result = _service.Analyze(6, 6, 6);

        Assert.True(result.IsValid);
        Assert.Equal(TriangleType.Equilateral, result.TriangleType);
    }

    [Fact]
    public void Analyze_ReturnsIsosceles_ForTwoEqualSides()
    {
        var result = _service.Analyze(5, 5, 8);

        Assert.True(result.IsValid);
        Assert.Equal(TriangleType.Isosceles, result.TriangleType);
    }

    [Fact]
    public void Analyze_ReturnsScalene_ForDifferentSides()
    {
        var result = _service.Analyze(3, 4, 5);

        Assert.True(result.IsValid);
        Assert.Equal(TriangleType.Scalene, result.TriangleType);
    }

    [Theory]
    [InlineData(0, 4, 5)]
    [InlineData(-1, 4, 5)]
    [InlineData(4, 0, 5)]
    public void Analyze_ReturnsError_ForZeroOrNegativeValues(int a, int b, int c)
    {
        var result = _service.Analyze(a, b, c);

        Assert.False(result.IsValid);
        Assert.Equal(TriangleType.Invalid, result.TriangleType);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(10, 1, 1)]
    [InlineData(2, 7, 2)]
    public void Analyze_ReturnsError_WhenTriangleInequalityFails(int a, int b, int c)
    {
        var result = _service.Analyze(a, b, c);

        Assert.False(result.IsValid);
        Assert.Equal(TriangleType.Invalid, result.TriangleType);
    }

    [Theory]
    [InlineData("a", "4", "5")]
    [InlineData("+", "4", "5")]
    [InlineData("4.5", "4", "5")]
    [InlineData("", "4", "5")]
    public void Analyze_ReturnsError_ForIncorrectStringInput(string sideA, string sideB, string sideC)
    {
        var result = _service.Analyze(sideA, sideB, sideC);

        Assert.False(result.IsValid);
        Assert.Equal(TriangleType.Invalid, result.TriangleType);
    }
}
