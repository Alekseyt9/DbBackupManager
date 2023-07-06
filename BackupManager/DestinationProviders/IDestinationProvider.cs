

namespace BackupManager
{
    public interface IDestinationProvider
    {
        Task Set(ProviderContext ctx, IDictionary<string, string> props, SourceData data);

    }
}
