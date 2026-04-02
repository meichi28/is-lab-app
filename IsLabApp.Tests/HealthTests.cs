using Xunit;
namespace IsLabApp.Tests;
public class HealthTests
{
    [Fact]
    public void ApplicationName_ShouldBe_IsLabApp()
    {
        var expected = "IsLabApp";
        var actual = "IsLabApp";
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Version_ShouldNotBeEmpty()
    {
        var version = "1.0.0";
        Assert.False(string.IsNullOrEmpty(version));
    }
}
