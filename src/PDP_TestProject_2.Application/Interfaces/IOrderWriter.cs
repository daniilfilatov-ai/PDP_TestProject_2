using PDP_TestProject_2.Domain.Models;

namespace PDP_TestProject_2.Application.Interfaces;

public interface IOrderWriter<TOutputData, TOutputPath>
{
    Task WriteAsync(IEnumerable<TOutputData> orders, TOutputPath outputFilePath);
}
