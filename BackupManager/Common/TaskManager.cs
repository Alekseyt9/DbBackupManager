﻿
using System.Collections.Specialized;
using Newtonsoft.Json;
using Quartz;

namespace BackupManager
{
    internal class TaskManager
    {

        public TaskManager()
        {
            Task.Run(RunTasks);
        }

        private async Task RunTasks()
        {
            using var reader = new StreamReader("appsettings.json");
            var str = await reader.ReadToEndAsync();
            var tasksModel = JsonConvert.DeserializeObject<TasksModel>(str);

            var properties = new NameValueCollection();
            var scheduler = await SchedulerBuilder.Create(properties).BuildScheduler();

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
