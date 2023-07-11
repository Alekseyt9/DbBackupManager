

namespace BackupManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger();

            logger.LogMessage("start");
            var tm = new TaskManager(logger);

            var str = Console.ReadLine();
            Console.WriteLine($"rl:{str}");
            Console.ReadLine();
        }
    }
}