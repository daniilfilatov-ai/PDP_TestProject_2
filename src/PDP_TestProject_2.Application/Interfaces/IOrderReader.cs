namespace PDP_TestProject_2.Application.Interfaces;

public interface IOrderReader<T>
{
    List<T> Read(string inputFilePath);
}
