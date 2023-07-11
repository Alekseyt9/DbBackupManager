﻿

namespace BackupManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger();

            logger.LogMessage("start");
            var tm = new TaskManager(logger);

            while (true)
            {
                Thread.Sleep(1000*10);
            }

        }
    }
}