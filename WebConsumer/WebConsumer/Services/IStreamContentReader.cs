namespace WebConsumer.Services;

public interface IStreamContentReader
{
    Task<byte[]> FetchStreamAsync(Stream inputStream, CancellationToken cancellationToken);
}