using WebConsumer.Exceptions;
using WebConsumer.Interfaces;

namespace WebConsumer.Services;

public class StreamContentReader : IStreamContentReader
{
    public async Task<byte[]> FetchStreamAsync(Stream inputStream, CancellationToken cancellationToken)
    {
        try
        {
            var responseBytes = new List<byte>();

            int chunkSize;

            do
            {
                var buffer = new byte[2048];

                chunkSize = await inputStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);

                if (chunkSize > 0)
                {
                    var bytesUsed = buffer.Take(chunkSize);

                    responseBytes.AddRange(bytesUsed);
                }
            } while (chunkSize > 0);

            return responseBytes.ToArray();
        }
        catch (OperationCanceledException)
        {
            throw new ErrorCodeException(ErrorCode.TaskCancelled, $"{nameof(FetchStreamAsync)} call was cancelled");
        }
        catch (Exception e)
        {
            throw new ErrorCodeException(ErrorCode.Unknown,
                $"Unexpected error occurred during {nameof(FetchStreamAsync)} call. Exception content: {e.Message}");
        }
    }
}