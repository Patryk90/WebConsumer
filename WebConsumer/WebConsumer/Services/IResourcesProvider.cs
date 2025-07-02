namespace WebConsumer.Services;

public interface IResourcesProvider
{
    IEnumerable<string> GetResources();
}