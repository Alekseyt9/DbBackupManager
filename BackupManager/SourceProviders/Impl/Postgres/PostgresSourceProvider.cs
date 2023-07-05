

namespace BackupManager
{
    [SourceProvider("Postgres")]
    internal class PostgresSourceProvider : ISourceProvider<PgProviderSettings>
    {
        public SourceData Get(PgProviderSettings props)
        {
            throw new NotImplementedException();
        }
    }
}
