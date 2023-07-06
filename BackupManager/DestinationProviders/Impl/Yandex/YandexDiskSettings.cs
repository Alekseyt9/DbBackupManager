

namespace BackupManager
{
    public class YandexDiskSettings
    {
        public YandexDiskSettings()
        {
        }

        public YandexDiskSettings(IDictionary<string, string> props)
        {
            DiskFolder = props["diskFolder"];
            Token = props["token"];
        }

        public string DiskFolder { get; set; }

        public string Token { get; set; }

    }

}
