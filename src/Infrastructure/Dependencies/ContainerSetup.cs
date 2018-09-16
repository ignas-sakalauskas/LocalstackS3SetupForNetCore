using Amazon.S3;
using Lamar;
using LocalstackS3SetupForNetCore.Configuration;
using LocalstackS3SetupForNetCore.Infrastructure.LocalStack;
using LocalstackS3SetupForNetCore.Infrastructure.Logging;
using LocalstackS3SetupForNetCore.LocalstackTests;
using LocalstackS3SetupForNetCore.S3Client;

namespace LocalstackS3SetupForNetCore.Infrastructure.Dependencies
{
    public static class ContainerSetup
    {
        public static IContainer Create()
        {
            LogHelper.Log(LogLevel.INFO, "Configuring DI Container.");

            var container = new Container(config =>
            {
                config.ForSingletonOf<LocalstackSettings>();
                config.ForSingletonOf<LocalstackSetup>();
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
    }
}