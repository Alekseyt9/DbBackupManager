
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;
using YandexDisk.Client;
using YandexDisk.Client.Clients;

namespace BackupManager
{
    [DestinationProvider("yandex_disk")]
    internal class YandexDiskDestinationProvider : DestinationProviderBase<YandexDiskSettings>
    {
        public override async Task Set(ProviderContext ctx, YandexDiskSettings props, SourceData data)
        {
            var dir = props.DiskFolder;
            var oauthToken = props.Token;
            var diskApi = new DiskHttpApi(oauthToken);

            try
            {
                var dirInfo = await diskApi.MetaInfo.GetInfoAsync(new ResourceRequest() { Path = dir });
            }
            catch (YandexApiException ex)
            {
                await diskApi.Commands.CreateDictionaryAsync(dir);
            }

            await diskApi.Files.UploadFileAsync(path: $"{dir}{Path.GetFileName(data.FilePath)}",
                overwrite: true,
                localFile: data.FilePath,
                cancellationToken: CancellationToken.None);

            ClearByParam(props.Keep);
        }

        public override YandexDiskSettings CreateProps(IDictionary<string, string> props)
        {
            return new YandexDiskSettings(props);
        }

        private void ClearByParam(int keep)
        {

        }

    }
}
