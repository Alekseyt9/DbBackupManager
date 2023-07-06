

namespace BackupManager
{
    public class TaskModel
    {
        public string Name { get; set; }

        public ProviderModel Source { get; set; }

        public ProviderModel Destination { get; set; }

        public ScheduleModel Schedule { get; set; }
    }
}
