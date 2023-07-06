
using BackupManager;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace BackupManagerTests
{
    public class PostgresProviderTest
    {
        private readonly IConfiguration _configuration;

        public PostgresProviderTest()
        {
            var builder = new ConfigurationBuilder().AddJsonFile(@"appsettings.json");
            _configuration = builder.Build();
        }

        [Fact]
        public void Test()
        {
            var prov = new PostgresSourceProvider();
            var ctx = new ProviderContext()
            {
                Logger = new TestLogger()
            };

            var data = prov.Get(ctx, new PgProviderSettings()
            {
                DatabaseName = "LangSequenceTraining",
                FilePath = @"c:\temp\",
                Host = "37.77.105.224",
                Password = _configuration["pg_password"],
                PgDumpPath = @"d:\Program Files\PgAdmin_v7\runtime\pg_dump.exe",
                Port = "5432",
                User = "postgres"
            });
        }

    }
}
