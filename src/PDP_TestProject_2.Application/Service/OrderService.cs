using PDP_TestProject_2.Application.Interfaces;
using PDP_TestProject_2.Domain.Models;

namespace PDP_TestProject_2.Application.Service;

public sealed class OrderService<T>(
    IOrderReader<T> reader,
    IOrderMapper<T> mapper,
    IOrderWriter writer) : IOrderService
{
    public void Process(string inputFilePath, string outputFilePath)
    {
        var rawData = reader.Read(inputFilePath);

        var orders = mapper.Map(rawData);

        writer.Write(orders, outputFilePath);
    }
}
