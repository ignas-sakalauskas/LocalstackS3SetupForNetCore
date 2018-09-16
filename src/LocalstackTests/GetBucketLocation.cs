using Amazon.S3;
using Amazon.S3.Model;
using LocalstackS3SetupForNetCore.Configuration;
using LocalstackS3SetupForNetCore.Infrastructure.Logging;
using System.Threading.Tasks;

namespace LocalstackS3SetupForNetCore.LocalstackTests
{
    public class GetBucketLocation : ILocalstackTest
    {
        private readonly IAmazonS3 _client;
        private readonly LocalstackSettings _settings;

        public GetBucketLocation(IAmazonS3 client, LocalstackSettings settings)
        {
            _client = client;
            _settings = settings;
        }

        public async Task Execute()
        {
            var result = await _client.GetBucketLocationAsync(new GetBucketLocationRequest
            {
                BucketName = _settings.Bucket
            });

            LogHelper.Log(LogLevel.INFO, $"Location request status: {result.HttpStatusCode}");
        }
    }
}