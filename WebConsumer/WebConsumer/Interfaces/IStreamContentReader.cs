namespace WebConsumer.Interfaces;

public interface IStreamContentReader
{
    Task<byte[]> FetchStreamAsync(Stream inputStream, CancellationToken cancellationToken);
}