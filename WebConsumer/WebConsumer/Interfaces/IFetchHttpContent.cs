namespace WebConsumer.Interfaces;

public interface IFetchHttpContent
{
    Task DownloadAsync(CancellationToken cancellationToken);
}