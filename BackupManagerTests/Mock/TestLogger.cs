
using BackupManager;

namespace BackupManagerTests
{
    internal class TestLogger : ILogger
    {
        private readonly List<string> _list = new();

        public void LogError(string msg)
        {
            _list.Add(msg);
        }

        public void LogMessage(string msg)
        {
            _list.Add(msg);
        }

    }
}
