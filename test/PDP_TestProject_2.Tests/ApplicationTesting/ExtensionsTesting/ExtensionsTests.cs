using AutoFixture;
using PDP_TestProject_2.Application.Extensions;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Models;

namespace PDP_TestProject_2.Tests.ApplicationTesting.ExtensionsTesting;

public class ExtensionsTests
{
    [Fact]
    public void FromCents_ShouldCorrectlyParse()
    {
        int cents = 10050;

        var result = cents.FromCents();

        Assert.Equal(100.5m, result);
    }
}
