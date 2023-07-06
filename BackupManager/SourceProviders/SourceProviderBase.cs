

namespace BackupManager
{
    internal abstract class SourceProviderBase<T> : ISourceProvider
    {
        public Task<SourceData> Get(ProviderContext ctx, IDictionary<string, string> props)
        {
            return Get(ctx, CreateProps(props));
        }

        public abstract Task<SourceData> Get(ProviderContext ctx, T props);

        public abstract T CreateProps(IDictionary<string, string> props);

    }
}
