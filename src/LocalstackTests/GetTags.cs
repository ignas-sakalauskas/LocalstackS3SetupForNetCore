using System;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using LocalstackS3SetupForNetCore.Configuration;
using LocalstackS3SetupForNetCore.Infrastructure.Logging;

namespace LocalstackS3SetupForNetCore.LocalstackTests
{
    public class GetTags : ILocalstackTest
    {
        private const string Filename = "uploadme.txt";

        private readonly IAmazonS3 _client;
        private readonly LocalstackSettings _settings;

        public GetTags(IAmazonS3 client, LocalstackSettings settings)
        {
            _client = client;
            _settings = settings;
        }

        public async Task Execute()
        {
            var tagRequest = new GetObjectTaggingRequest()
            {
                BucketName = _settings.Bucket,
                Key = Filename
            };

            var result = await _client.GetObjectTaggingAsync(tagRequest);

            LogHelper.Log(LogLevel.INFO, "Retrieving tags - DONE. Tags:");
            foreach (var tag in result.Tagging)
            {
                LogHelper.Log(LogLevel.INFO, $"\tKey: {tag.Key}, Value: {tag.Value}");
            }
        }
    }
}