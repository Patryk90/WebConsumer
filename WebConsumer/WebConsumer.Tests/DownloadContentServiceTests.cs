using Moq.Protected;
using Moq;
using WebConsumer.Exceptions;
using WebConsumer.Services;

namespace WebConsumer.Tests;

public class DownloadContentServiceTests
{
    [Fact]
    public async Task Should_Handle_Http_Failure_Test()
    {
        var sut = new DownloadContentService(CreateHttpFactoryMock().Object);

        var actual = await Assert.ThrowsAsync<ErrorCodeException>(() =>
            sut.GetUrlContentStreamAsync("https://www.someRandomUrl.at/", CancellationToken.None));

        Assert.Equal(ErrorCode.ConnectionError, actual.ErrorCode);
    }

    private Mock<IHttpClientFactory> CreateHttpFactoryMock()
    {
        Mock<HttpMessageHandler> mockHttpMessageHandler = new(MockBehavior.Strict);

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Throws<HttpRequestException>()
            .Verifiable();

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);

        var mockHttpFactory = new Mock<IHttpClientFactory>();

        mockHttpFactory.Setup(factory => factory.CreateClient("ServiceHttpClient"))
            .Returns(httpClient);

        return mockHttpFactory;
    }
}