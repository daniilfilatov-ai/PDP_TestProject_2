using PDP_TestProject_2.Domain.Models;

namespace PDP_TestProject_2.Application.Interfaces;

public interface IOrderWriter
{
    void Write(List<Order> orders, string outputFilePath);
}
