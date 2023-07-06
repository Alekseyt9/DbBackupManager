
using System.Reflection;
using Newtonsoft.Json;

namespace BackupManager
{
    internal class TaskManager
    {
        private readonly Dictionary<string, ISourceProvider> _sourceProviders = new();
        private readonly Dictionary<string, IDestinationProvider> _destinationProviders = new();

        public TaskManager()
        {
            InitProviders();
            RunTasks();
        }

        private void RunTasks()
        {
            using var r = new StreamReader("appsettings.json");
            var str = r.ReadToEnd();
            var tasksModel = JsonConvert.DeserializeObject<TasksModel>(str);

            foreach (var task in tasksModel.Tasks)
            {
                var period = task.Schedule.Period;

            }
        }

        private async Task RunTask(TaskModel task)
        {
            var logger = new Logger();
            try
            {
                var srcProv = _sourceProviders[task.Source.Name];
                var destProv = _destinationProviders[task.Destination.Name];
                var ctx = new ProviderContext()
                {
                    Logger = logger
                };
                var data = await srcProv.Get(ctx, task.Source.Properties);
                await destProv.Set(ctx, task.Destination.Properties, data);
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
