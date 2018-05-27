using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.S3;
using LocalstackS3SetupForNetCore.Configuration;
using LocalstackS3SetupForNetCore.LocalstackTests;
using LocalstackS3SetupForNetCore.S3Client;
using StructureMap;

namespace LocalstackS3SetupForNetCore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Setup DI container
            var container = ConfigureContainer();

            // Sync point, wait for localstack to load S3 service
            await WaitForLocalstack();
            
            // Run all tests
            var tests = container.GetInstance<IList<ILocalstackTest>>();
            foreach (var test in tests)
            {
                await test.Execute();
            }

            Console.WriteLine($"{DateTime.Now} - INFO: All localstack tests have run. Exiting...");
        }

        private static IContainer ConfigureContainer()
        {
            Console.WriteLine($"{DateTime.Now} - INFO: Configuring DI Container.");
            
            var container = new Container();

            container.Configure(config =>
            {
                config.ForSingletonOf<LocalstackSettings>();
                config.ForSingletonOf<IS3ClientFactory>().Use<LocalstackS3ClientFactory>();
                config.ForSingletonOf<IAmazonS3>().Use(ctx => ctx.GetInstance<IS3ClientFactory>().CreateClient());
                // Localstack tests
                config.ForSingletonOf<ILocalstackTest>().Add<CreateBucket>();
                config.ForSingletonOf<ILocalstackTest>().Add<GetBucketLocation>();
                config.ForSingletonOf<ILocalstackTest>().Add<UploadFile>();
                config.ForSingletonOf<ILocalstackTest>().Add<DownloadFile>();
                config.ForSingletonOf<ILocalstackTest>().Add<GetTags>();
                config.ForSingletonOf<ILocalstackTest>().Add<GetPreSignedUrl>();
            });

            container.AssertConfigurationIsValid();

            return container;
        }

        private static async Task WaitForLocalstack()
        {
            var timeDelay = TimeSpan.FromSeconds(5);
            Console.WriteLine($"{DateTime.Now} - INFO: Waiting {timeDelay.Seconds}s for Localstack to start...");
            await Task.Delay(timeDelay);
        }
    }
}
