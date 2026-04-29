using PDP_TestProject_2.Domain.Models;

namespace PDP_TestProject_2.Application.Interfaces;

public interface IFileWriter<TData>
{
    Task WriteAsync(IEnumerable<TData> orders, string filePath, CancellationToken cancellationToken = default);
}
