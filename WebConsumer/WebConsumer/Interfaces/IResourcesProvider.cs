namespace WebConsumer.Interfaces;

public interface IResourcesProvider
{
    IEnumerable<string> GetResources();
}