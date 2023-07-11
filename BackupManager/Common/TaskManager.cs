
using System.Collections.Specialized;
using Newtonsoft.Json;
using Quartz;

namespace BackupManager
{
    internal class TaskManager
    {
        private ILogger _logger;

        public TaskManager(ILogger logger)
        {
            _logger = logger;
            Task.Run(RunTasks);
        }

        private async Task RunTasks()
        {
            try
            {
                using var reader = new StreamReader( Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"));
                var str = await reader.ReadToEndAsync();

                var tasksModel = JsonConvert.DeserializeObject<TasksModel>(str);

                var properties = new NameValueCollection();
                var scheduler = await SchedulerBuilder.Create(properties).BuildScheduler();

                _logger.LogMessage($"Task count: {tasksModel.Tasks.Count}");

                /*
                var i = 0;
                foreach (var task in tasksModel.Tasks)
                {
                    foreach (var per in task.Schedule.Periods)
                    {
                        var job = JobBuilder.Create<BackupJob>()
                            .WithIdentity($"job{i}_{per.Name}", "group1")
                            .UsingJobData("params", JsonConvert.SerializeObject(new BackupJobParams()
                            {
                                Task = task,
                                Period = per
                            }))
                            .Build();

                        var trigger = TriggerBuilder.Create()
                            .WithIdentity($"trigger{i}_{per.Name}", "group1")
                            .StartNow()
                            .WithCronSchedule(per.Cron)
                            .Build();

                        await scheduler.ScheduleJob(job, trigger);
                    }
                }*/

                await scheduler.Start();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }


    }
}
