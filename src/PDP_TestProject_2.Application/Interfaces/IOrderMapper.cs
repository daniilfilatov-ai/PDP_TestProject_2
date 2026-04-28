using PDP_TestProject_2.Domain.Models;

namespace PDP_TestProject_2.Application.Interfaces;

public interface IOrderMapper<TInputData, TOutputData>
{
    IEnumerable<TOutputData> Map(IEnumerable<TInputData> rawData);
}
