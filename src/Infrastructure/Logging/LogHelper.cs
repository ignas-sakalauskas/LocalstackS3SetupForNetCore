using System;

namespace LocalstackS3SetupForNetCore.Infrastructure.Logging
{
    public static class LogHelper
    {
        public static void Log(LogLevel level, string message)
        {
            Console.WriteLine($"{DateTime.Now} - {level}: {message}");
        }
    }
}