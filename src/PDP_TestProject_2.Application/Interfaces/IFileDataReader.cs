namespace PDP_TestProject_2.Application.Interfaces;

public interface IFileDataReader<TData>
{
    Task<IEnumerable<TData>> ReadAsync(string filePath, CancellationToken cancellationToken = default);
}
