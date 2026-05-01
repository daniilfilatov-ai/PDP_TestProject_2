using AutoFixture;
using NSubstitute;
using PDP_TestProject_2.Application.Interfaces;
using PDP_TestProject_2.Domain.Models;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Models;
using PDP_TestProject_2.Infrastructure.Service;

namespace PDP_TestProject_2.Tests.Infrastructure.Service;

public class FileDataServiceTest
{
    private readonly IFixture _fixture;
    private readonly IFileDataReader<InputOrderModel> _readerMock;
    private readonly IRawDataMapper<InputOrderModel, Order> _mapperMock;
    private readonly IFileWriter<Order> _writerMock;
    private readonly FileDataService<InputOrderModel, Order> _sut;
    public FileDataServiceTest()
    {
        _fixture = new Fixture();

        _readerMock = Substitute.For<IFileDataReader<InputOrderModel>>();
        _mapperMock = Substitute.For<IRawDataMapper<InputOrderModel, Order>>();
        _writerMock = Substitute.For<IFileWriter<Order>>();

        _sut = new FileDataService<InputOrderModel, Order>(
            _readerMock,
            _mapperMock,
            _writerMock
            );
    }

    [Fact]
    public async Task ProcessAsync_ShouldCorrectlyWork_WithCorrectParametrs()
    {
        // ARRANGE
        var inputPath = "fake_input.json";
        var outputPath = "fake_output.json";
        var cancellationToken = new CancellationTokenSource().Token;

        var rawData = _fixture.CreateMany<InputOrderModel>(3).ToList();
        var mappedData = _fixture.CreateMany<Order>(3).ToList();

        _readerMock.ReadAsync(inputPath, cancellationToken).Returns(rawData);

        _mapperMock.Map(rawData).Returns(mappedData);

        // ACT
        await _sut.ProcessAsync(inputPath, outputPath, cancellationToken);

        // ASSERT
        await _readerMock.Received(1).ReadAsync(inputPath, cancellationToken);
        _mapperMock.Received(1).Map(rawData);
        await _writerMock.Received(1).WriteAsync(mappedData, outputPath, cancellationToken);
    }
}
