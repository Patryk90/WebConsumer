using System.Diagnostics;
using WebConsumer.Exceptions;
using WebConsumer.Services;

try
{
    var stopwatch = Stopwatch.StartNew();

    var cts = new CancellationTokenSource();
    cts.CancelAfter(15000);

    var processor =
        new DownloadHttpContentProcessor(new DownloadContentService(), new StreamContentReader(), new ResourcesHandler());

    await processor.DownloadAsync(cts.Token);

    stopwatch.Stop();

    Console.WriteLine($"Content downloaded in {stopwatch.Elapsed} seconds");
}
catch (ErrorCodeException e)
{
    Console.WriteLine($"Execution failed with '{e.ErrorCode}' error code. Message: {e.Message}");
}

Console.ReadKey();