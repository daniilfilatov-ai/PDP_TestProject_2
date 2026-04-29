using PDP_TestProject_2.Application.Interfaces;
namespace PDP_TestProject_2.Infrastructure.Service;

public sealed class OrderService<TInputData, TOutputData>(
    IFileDataReader<TInputData> reader,
    IRawDataMapper<TInputData, TOutputData> mapper,
    IFileWriter<TOutputData> writer) : IService
{
    public async Task ProcessAsync(string inputFilePath, string outputFilePath, CancellationToken cancellationToken = default)
    {
        var rawData = await reader.ReadAsync(inputFilePath, cancellationToken);

        var orders = mapper.Map(rawData);

        await writer.WriteAsync(orders, outputFilePath, cancellationToken);
    }
}
