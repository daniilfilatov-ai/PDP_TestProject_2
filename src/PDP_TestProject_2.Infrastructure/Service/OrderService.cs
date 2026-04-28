using PDP_TestProject_2.Application.Interfaces;
namespace PDP_TestProject_2.Infrastructure.Service;

public sealed class OrderService<TInputData, TOutputData>(
    IOrderReader<string, TInputData> reader,
    IOrderMapper<TInputData, TOutputData> mapper,
    IOrderWriter<TOutputData, string> writer) : IOrderService<string, string>
{
    public async Task ProcessAsync(string inputFilePath, string outputFilePath)
    {
        var rawData = await reader.ReadAsync(inputFilePath);

        var orders = mapper.Map(rawData);

        await writer.WriteAsync(orders, outputFilePath);
    }
}
