
using BackupManager;

namespace BackupManagerTests
{
    internal class TestLogger : ILogger
    {
        private List<string> _list = new List<string>();

        public void LogError(string msg)
        {
            _list.Add(msg);
        }

    }
}
