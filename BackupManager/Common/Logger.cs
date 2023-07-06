

namespace BackupManager
{
    public class Logger : ILogger
    {
        public void LogError(string msg)
        {
            Console.WriteLine($"[error]: {msg}");
        }

        public void LogMessage(string msg)
        {
            Console.WriteLine($"[message]: {msg}");
        }
    }
}
