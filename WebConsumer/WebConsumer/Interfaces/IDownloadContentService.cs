namespace WebConsumer.Interfaces;

public interface IDownloadContentService
{
    Task<Stream> GetUrlContentStreamAsync(string url, CancellationToken cancellationToken);
}