// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var url1 = "https://api.restful-api.dev/objects";
var url2 = "https://api.restful-api.dev/objects/2";
var url3= "https://api.restful-api.dev/objects/3";

var task1 = DownloadHttpContentAsync(url1);
var task2 = DownloadHttpContentAsync(url2);
var task3 = DownloadHttpContentAsync(url3);

Task.WaitAll(task1, task2, task3);

Console.ReadKey();


async Task DownloadHttpContentAsync(string uri)
{
    var httpClient = new HttpClient();

    var response = await httpClient.GetAsync(uri);

    if (!response.IsSuccessStatusCode)
    {
        Console.WriteLine($"Could not download resources from Uri: {uri}");

        Console.WriteLine($"Status code: {response.StatusCode}");

        Console.ReadKey();
    }

    var urlContent = await response.Content.ReadAsStringAsync(CancellationToken.None);

    Console.WriteLine("Http call successful - response content as below: ");
    Console.WriteLine(urlContent);
}