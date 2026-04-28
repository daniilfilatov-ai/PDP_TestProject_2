
namespace PDP_TestProject_2.Application.Interfaces;

public interface IOrderService<TInputPath, TOutputPath>
{
    Task ProcessAsync(TInputPath inputFilePath, TOutputPath outputFilePath);
}
