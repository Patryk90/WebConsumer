using Moq;
using WebConsumer.Exceptions;
using WebConsumer.Interfaces;
using WebConsumer.Services;

namespace WebConsumer.Tests;

public class DownloadHttpContentProcessorTests
{
    private Mock<IResourcesProvider> _mockResourceProvider = new();

    private Mock<IDownloadContentService> _mockDownloadContentService = new();

    private Mock<IStreamContentReader> _mockStreamContentReader = new();

    [Fact]
    public async Task Should_Download_Resources_Test()
    {
        _mockResourceProvider.Setup(f => f.GetResources())
            .Returns(TestResources);

        var sut = new DownloadHttpContentProcessor(_mockDownloadContentService.Object,
            _mockStreamContentReader.Object, _mockResourceProvider.Object);

        await sut.DownloadAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Should_Skip_Empty_Resources_Test()
    {
        _mockResourceProvider.Setup(f => f.GetResources())
            .Returns(Array.Empty<string>());

        var sut = new DownloadHttpContentProcessor(_mockDownloadContentService.Object,
            _mockStreamContentReader.Object, _mockResourceProvider.Object);

        await sut.DownloadAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Should_Handle_Cancellation_Test()
    {
        _mockResourceProvider.Setup(f => f.GetResources())
            .Returns(TestResources);

        var sut = new DownloadHttpContentProcessor(_mockDownloadContentService.Object,
            _mockStreamContentReader.Object, _mockResourceProvider.Object);

        var cts = new CancellationTokenSource();

        cts.Cancel(true);

        var actual = await Assert.ThrowsAsync<ErrorCodeException>(() => sut.DownloadAsync(cts.Token));

        Assert.Equal(ErrorCode.TaskCancelled, actual.ErrorCode);
    }

    private string[] TestResources =>
    [
        "https://api.restful-api.dev/objects",
        "https://api.restful-api.dev/objects/2",
        "https://api.restful-api.dev/objects/3"
    ];
}