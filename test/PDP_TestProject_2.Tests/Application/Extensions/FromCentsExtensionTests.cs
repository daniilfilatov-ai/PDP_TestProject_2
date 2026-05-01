using AutoFixture;
using PDP_TestProject_2.Application.Extensions;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Models;

namespace PDP_TestProject_2.Tests.Application.Extensions;

public class FromCentsExtensionTests
{
    [Fact]
    public void FromCents_ShouldCorrectlyParse()
    {
        // ARRANGE
        int cents = 10050;

        // ACT
        var result = cents.FromCents();

        // ASSERT
        Assert.Equal(100.5m, result);
    }
}
