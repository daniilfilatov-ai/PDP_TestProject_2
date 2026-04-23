using PDP_TestProject_2.Application.Interfaces;
using PDP_TestProject_2.Domain.Models;

namespace PDP_TestProject_2.Application.Service;

public sealed class OrderService<T>(
    IOrderReader<T> reader,
    IOrderMapper<T> mapper,
    IOrderWriter writer)
{
    public void Process(string inputFilePath, string outputFilePath)
    {
        List<T> rawData = reader.Read(inputFilePath);

        List<Order> orders = mapper.Map(rawData);

        writer.Write(orders, outputFilePath);
    }
}
