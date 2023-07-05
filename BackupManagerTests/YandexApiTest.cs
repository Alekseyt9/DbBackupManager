
using Xunit;
using YandexDisk.Client;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace BackupManagerTests
{
    public class YandexApiTest
    {
        [Fact]
        public async Task Test()
        {
            var dir = "/backups/test1/";

            var oauthToken = "y0_AgAAAAALO2EBAAoi3wAAAADnCMTWyUDoupLASx2guxg8GITw4V8EA0s";
            var diskApi = new DiskHttpApi(oauthToken);

            try
            {
                var dirInfo = await diskApi.MetaInfo.GetInfoAsync(new ResourceRequest() { Path = dir });
            }
            catch (YandexApiException ex)
            {
                await diskApi.Commands.CreateDictionaryAsync(dir);
            }

            await diskApi.Files.UploadFileAsync(path: $"{dir}test111.txt",
                overwrite: true,
                localFile: @"..\..\..\data\test111.txt",
                cancellationToken: CancellationToken.None);
        }

    }
}
