

namespace BackupManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tm = new TaskManager(new Logger());
            while(Console.ReadLine() != "/q")
            {

            }
        }
    }
}