

namespace BackupManager
{
    public interface ISourceProvider
    {
        Task<SourceData> Get(ProviderContext ctx, IDictionary<string, string> props);

        void Finish(SourceData data);
    }
}
