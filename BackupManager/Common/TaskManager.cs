
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
            _logger.LogMessage("1");
            using var reader = new StreamReader("appsettings.json");
            var str = reader.ReadToEnd();
            var tasksModel = JsonConvert.DeserializeObject<TasksModel>(str);
            _logger.LogMessage("2");

            var properties = new NameValueCollection();
            var scheduler = await SchedulerBuilder.Create(properties).BuildScheduler();

            _logger.LogMessage($"Task count: {tasksModel.Tasks.Count}");

            var i = 0;
            foreach (var task in tasksModel.Tasks)
            {
                var period = task.Schedule.Period;

                var job = JobBuilder.Create<Job>()
                    .WithIdentity($"job{i}", "group1")
                    .UsingJobData("params", JsonConvert.SerializeObject(task))
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity($"trigger{i}", "group1")
                    .StartNow()
                    .WithCronSchedule(period)
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }

            await scheduler.Start();
        }


    }
}
