using WebConsumer.Exceptions;
using WebConsumer.Interfaces;

namespace WebConsumer.Services;

public class DownloadContentService : IDownloadContentService
{
    public async Task<Stream> GetUrlContentStreamAsync(string url, CancellationToken cancellationToken)
    {
        HttpResponseMessage? response = null;

        try
        {
            var httpClient = new HttpClient();

            response = await httpClient.GetAsync(url, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
        catch (TaskCanceledException)
        {
            throw new ErrorCodeException(ErrorCode.TaskCancelled, $"{nameof(GetUrlContentStreamAsync)} call was cancelled");
        }
        catch (HttpRequestException)
        {
            throw new ErrorCodeException(ErrorCode.ConnectionError,
                $"Could not download resources from Uri: {url} - Status code: {response?.StatusCode}");
        }
        catch (Exception e)
        {
            throw new ErrorCodeException(ErrorCode.Unknown,
                $"Unexpected error occurred during {nameof(GetUrlContentStreamAsync)} call. Exception content: {e.Message}");
        }
    }
}