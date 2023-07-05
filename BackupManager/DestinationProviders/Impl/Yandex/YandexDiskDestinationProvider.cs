

namespace BackupManager
{
    [DestinationProvider("yandex_disk")]
    internal class YandexDiskDestinationProvider : IDestinationProvider<YandexDiskSettings>
    {
        public void Set(YandexDiskSettings props, SourceData data)
        {
            throw new NotImplementedException();
        }
    }
}
