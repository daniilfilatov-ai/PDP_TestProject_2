using PDP_TestProject_2.Domain.Models;

namespace PDP_TestProject_2.Application.Interfaces;

public interface IOrderMapper<T>
{
    List<Order> Map(List<T> rawData);
}
