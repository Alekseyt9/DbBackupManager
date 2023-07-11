

namespace BackupManager
{
    internal class PgProviderSettings
    {
        public PgProviderSettings() {}

        public PgProviderSettings(IDictionary<string, string> props)
        {
            PgDumpPath = props["pgDumpPath"];
            FilePath = props["filePath"];
            DatabaseName = props["databaseName"];
            Port = props["port"];
            Host = props["host"];
            User = props["user"];
            Password = props["password"];
            PeriodName = props["periodName"];
        }

        public string PeriodName { get; set; }

        public string PgDumpPath { get; set; }

        public string FilePath { get; set; }

        public string DatabaseName { get; set; }

        public string Port { get; set; }

        public string Host { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

    }

}
