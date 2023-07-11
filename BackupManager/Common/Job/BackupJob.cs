
using Newtonsoft.Json;
using Quartz;
using System.Reflection;

namespace BackupManager
{
    public class BackupJob : IJob
    {
        private readonly Dictionary<string, ISourceProvider> _sourceProviders = new();
        private readonly Dictionary<string, IDestinationProvider> _destinationProviders = new();

        public async Task Execute(IJobExecutionContext context)
        {
            InitProviders();
            var parsStr = context.JobDetail.JobDataMap.GetString("params");
            var pars = JsonConvert.DeserializeObject<BackupJobParams>(parsStr);
            await RunTask(pars);
        }

        private async Task RunTask(BackupJobParams pars)
        {
            var logger = new Logger();
            try
            {
                var task = pars.Task;
                var srcProv = _sourceProviders[task.Source.Name];
                var destProv = _destinationProviders[task.Destination.Name];
                var ctx = new ProviderContext()
                {
                    Logger = logger
                };
                var props = new Dictionary<string, string>(task.Source.Properties);
                props.Add("periodName", pars.Period.Name);
                var data = await srcProv.Get(ctx, props);
                await destProv.Set(ctx, task.Destination.Properties, data);
                srcProv.Finish(data);

                logger.LogMessage($"task completed: {task.Name}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        private void InitProviders()
        {
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var type in asms.SelectMany(x => x.GetTypes()))
            {
                if (typeof(ISourceProvider).IsAssignableFrom(type))
                {
                    var attr = type.GetCustomAttribute<SourceProviderAttribute>();
                    if (attr != null)
                    {
                        _sourceProviders.Add(attr.Name, (ISourceProvider)Activator.CreateInstance(type));
                    }
                }

                if (typeof(IDestinationProvider).IsAssignableFrom(type))
                {
                    var attr = type.GetCustomAttribute<DestinationProviderAttribute>();
                    if (attr != null)
                    {
                        _destinationProviders.Add(attr.Name, (IDestinationProvider)Activator.CreateInstance(type));
                    }
                }
            }
        }

    }

}
