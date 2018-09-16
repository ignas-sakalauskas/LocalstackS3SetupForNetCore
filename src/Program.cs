using LocalstackS3SetupForNetCore.Infrastructure.Dependencies;
using LocalstackS3SetupForNetCore.Infrastructure.LocalStack;
using LocalstackS3SetupForNetCore.LocalstackTests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalstackS3SetupForNetCore.Infrastructure.Logging;

namespace LocalstackS3SetupForNetCore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Setup DI container
            var container = ContainerSetup.Create();

            // Sync point, wait for localstack to load S3 service
            var localstackSetup = container.GetInstance<LocalstackSetup>();
            await localstackSetup.WaitForInit(TimeSpan.FromSeconds(60));

            // Run all tests
            var tests = container.GetInstance<IList<ILocalstackTest>>();
            foreach (var test in tests)
            {
                await test.Execute();
            }

            LogHelper.Log(LogLevel.INFO, "All localstack tests have run. Exiting...");
        }
    }
}
