namespace WebConsumer.Services;

public interface IFetchHttpContent
{
    Task DownloadAsync(CancellationToken cancellationToken);
}