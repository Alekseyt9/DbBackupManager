
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

            //await ClearByParam(diskApi, data, props.MaxCount, dir);
        }

        public override YandexDiskSettings CreateProps(IDictionary<string, string> props)
        {
            return new YandexDiskSettings(props);
        }

        private async Task ClearByParam(DiskHttpApi diskApi, SourceData data, int maxCount, string dir)
        {
            //$"{props.DatabaseName}_{props.PeriodName}_{DateTime.Now:dd_MM_yy_HH_mm_ss}.psql"
            var arr = Path.GetFileName(data.FilePath).Split("_");
            var pref = string.Join("_", arr.Skip(2));
            var fileInfos = await diskApi.MetaInfo.GetFilesInfoAsync(new FilesResourceRequest()
            {
                Limit = 100, 
                Path = dir
            });
            var list = new List<Resource>();
            foreach (var item in fileInfos.Items.Where(x => x.Name.StartsWith(pref)).OrderByDescending(x => x.Created).Skip(maxCount))
            {
                list.Add(item);
            }
        }

    }
}
