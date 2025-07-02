using WebConsumer.Exceptions;
using WebConsumer.Interfaces;

namespace WebConsumer.Services;

public class DownloadHttpContentProcessor(
    IDownloadContentService downloadHttpContentService,
    IStreamContentReader streamContentReader,
    IResourcesProvider resourcesProvider)
    : IFetchHttpContent
{
    public Task DownloadAsync(CancellationToken cancellationToken)
    {
        var urls = resourcesProvider.GetResources().ToArray();

        if (urls.Length <= 0)
        {
            return Task.CompletedTask;
        }

        var tasks = new List<Task>(urls.Length);

        foreach (var url in urls)
        {
            var task = GetHttpContent(url, cancellationToken);

            tasks.Add(task);
        }

        Task.WaitAll(tasks.ToArray(), cancellationToken);

        return Task.CompletedTask;
    }

    public async Task GetHttpContent(string url, CancellationToken cancellationToken)
    {
        try
        {
            using (var stream = await downloadHttpContentService.GetUrlContentStreamAsync(url, cancellationToken))
            {
                var content = await streamContentReader.FetchStreamAsync(stream, cancellationToken);

                Console.WriteLine($"Http call successful - the GET {url} responded with {content.Length} bytes");
            }
        }
        catch (ErrorCodeException)
        {
            throw;
        }
        catch (TaskCanceledException)
        {
            throw new ErrorCodeException(ErrorCode.TaskCancelled, $"{nameof(GetHttpContent)} call was cancelled");
        }
        catch (Exception e)
        {
            throw new ErrorCodeException(ErrorCode.Unknown,
                $"Unexpected error occurred during {nameof(GetHttpContent)} call. Exception content: {e.Message}");
        }
    }
}