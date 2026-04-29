
namespace PDP_TestProject_2.Application.Interfaces;

public interface IFileDataService
{
    Task ProcessAsync(string inputFilePath, string outputFilePath, CancellationToken cancellationToken = default);
}
