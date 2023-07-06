

namespace BackupManager
{
    public class Logger : ILogger
    {
        public void LogError(string msg)
        {
            Console.WriteLine("[error]: {msg)}");
        }

    }
}
