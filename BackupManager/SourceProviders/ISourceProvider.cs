


namespace BackupManager
{

    internal interface ISourceProvider<T>
    {
        SourceData Get(T props);
    }
}
