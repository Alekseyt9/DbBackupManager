
namespace BackupManager;

public interface ILogger
{
    void LogError(string msg);

    void LogMessage(string msg);
}