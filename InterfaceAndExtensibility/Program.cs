using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAndExtensibility
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleLogger = new ConsoleLogger();
            var dbMigrator = new DbMigrator(consoleLogger);
            dbMigrator.Migrate();

            var fileLogger = new FileLogger("log.txt");
            var dbMigrator2 = new DbMigrator(fileLogger);
            dbMigrator2.Migrate(); // Go to "bin\debug" to find the log.
        }
    }

    public class DbMigrator
    {
        private readonly ILogger _logger;

        public DbMigrator(ILogger logger)
        {
            _logger = logger;
        }
        public void Migrate()
        {
            _logger.LogInfo("Migrating started at {0}" + DateTime.Now);

            //Details of migrating the database
            _logger.LogInfo("Migrating finished at {0}" + DateTime.Now);
        }
    }

    public interface ILogger
    {
        void LogError(string message);
        void LogInfo(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }


        public void LogInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
        }
    }

    public class FileLogger : ILogger
    {
        private readonly string _path;

        public FileLogger(string path)
        {
            _path = path;
        }

        public void LogError(string message)
        {
            Log(message, "ERROR");
        }

        public void LogInfo(string message)
        {
            Log(message, "INFO");
        }
        private void Log(string message, string messagetype)
        {
            using (var streamWriter = new StreamWriter(_path, true))
            {
                streamWriter.WriteLine(messagetype + ": " + message);
            }
        }
    }
}
