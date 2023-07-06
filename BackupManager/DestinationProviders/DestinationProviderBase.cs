

namespace BackupManager
{
    internal abstract class DestinationProviderBase<T> : IDestinationProvider
    {

        public abstract Task Set(ProviderContext ctx, T props, SourceData data);

        public Task Set(ProviderContext ctx, IDictionary<string, string> props, SourceData data)
        {
            return Set(ctx, CreateProps(props), data);
        }

        public abstract T CreateProps(IDictionary<string, string> props);

    }
}
