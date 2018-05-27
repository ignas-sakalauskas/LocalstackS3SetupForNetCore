using System;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using LocalstackS3SetupForNetCore.Configuration;

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

            Console.WriteLine($"{DateTime.Now} - Location request status: {result.HttpStatusCode}");
        }
    }
}