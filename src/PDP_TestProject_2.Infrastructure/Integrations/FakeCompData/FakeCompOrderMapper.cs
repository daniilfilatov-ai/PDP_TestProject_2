using Microsoft.Extensions.Logging;
using PDP_TestProject_2.Application.Interfaces;
using PDP_TestProject_2.Domain.Models;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Extensions;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Models;

namespace PDP_TestProject_2.Infrastructure.Integrations.FakeCompData;

public sealed class FakeCompOrderMapper(ILogger<FakeCompOrderMapper> logger) : IRawDataMapper<InputOrderModel, Order>
{
    public IEnumerable<Order> Map(IEnumerable<InputOrderModel> rawData)
    {
        logger.LogInformation("Initiating data mapping");

        var result = rawData.MapToOrders();

        logger.LogInformation("Success mapping");
        return result;
    }
}
