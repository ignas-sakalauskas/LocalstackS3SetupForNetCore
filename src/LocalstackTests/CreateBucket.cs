using Amazon.S3;
using LocalstackS3SetupForNetCore.Configuration;
using LocalstackS3SetupForNetCore.Infrastructure.Logging;
using System.Threading.Tasks;

namespace LocalstackS3SetupForNetCore.LocalstackTests
{
    public class CreateBucket : ILocalstackTest
    {
        private readonly IAmazonS3 _client;
        private readonly LocalstackSettings _settings;

        public CreateBucket(IAmazonS3 client, LocalstackSettings settings)
        {
            _client = client;
            _settings = settings;
        }

        public async Task Execute()
        {
            await _client.EnsureBucketExistsAsync(_settings.Bucket);
            LogHelper.Log(LogLevel.INFO, $"Create '{_settings.Bucket}' bucket - DONE");
        }
    }
}