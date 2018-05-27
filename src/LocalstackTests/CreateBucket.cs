using System;
using System.Threading.Tasks;
using Amazon.S3;
using LocalstackS3SetupForNetCore.Configuration;

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
            Console.WriteLine($"{DateTime.Now} - Create '{_settings.Bucket}' bucket - DONE");
        }
    }
}