namespace PDP_TestProject_2.Application.Interfaces;

public interface IOrderReader<TInputPath, TOutputData>
{
    Task<IEnumerable<TOutputData>> ReadAsync(TInputPath inputFilePath);
}
