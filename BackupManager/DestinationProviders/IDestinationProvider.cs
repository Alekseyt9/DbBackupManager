

namespace BackupManager
{
    public interface IDestinationProvider<T>
    {
        void Set(T props, SourceData data);
    }
}
