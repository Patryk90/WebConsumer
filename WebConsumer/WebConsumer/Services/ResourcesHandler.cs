using WebConsumer.Exceptions;
using WebConsumer.Interfaces;

namespace WebConsumer.Services;

public class ResourcesHandler : IResourcesProvider
{
    public IEnumerable<string> GetResources()
    {
        try
        {
            var resultList = new List<string>();
                
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "ImportUrlsDataFile.csv");

            var rows = File.ReadAllLines(filePath);

            foreach (var row in rows)
            {
                if (!string.IsNullOrEmpty(row))
                {
                    var formattedRow = row.Trim().Replace("\"", "");
                    resultList.Add(formattedRow);
                }
            }

            return resultList;
        }
        catch (Exception e)
        {
            throw new ErrorCodeException(ErrorCode.ResourceAccessError,
                $"Unexpected error occurred during {nameof(GetResources)} call. Exception content: {e.Message}");
        }
    }
}