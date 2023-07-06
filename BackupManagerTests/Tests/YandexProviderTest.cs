
using BackupManager;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace BackupManagerTests.Tests
{
    public class YandexProviderTest
    {
        private readonly IConfiguration _configuration;

        public YandexProviderTest()
        {
            var builder = new ConfigurationBuilder().AddJsonFile(@"appsettings.json");
            _configuration = builder.Build();
        }

        [Fact]
        public async Task Test()
        {
            var ctx = new ProviderContext()
            {
                Logger = new TestLogger()
            };

            var prov = new YandexDiskDestinationProvider();
            var fileName = "test111.txt";

            await prov.Set(ctx, new YandexDiskSettings()
                {
                    DiskFolder = "/backups/test1/",
                    Token = _configuration["yandex_token"]
                },
                new SourceData()
                {
                    FilePath = $@"..\..\..\data\{fileName}"
                }
            );

        }

    }
}
