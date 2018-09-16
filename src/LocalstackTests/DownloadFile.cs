using Amazon.S3;
using Amazon.S3.Model;
using LocalstackS3SetupForNetCore.Configuration;
using LocalstackS3SetupForNetCore.Infrastructure.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LocalstackS3SetupForNetCore.LocalstackTests
{
    public class DownloadFile : ILocalstackTest
    {
        private const string Filename = "uploadme.txt";

        private readonly IAmazonS3 _client;
        private readonly LocalstackSettings _settings;

        public DownloadFile(IAmazonS3 client, LocalstackSettings settings)
        {
            _client = client;
            _settings = settings;
        }

        public async Task Execute()
        {
            var request = new GetObjectRequest
            {
                BucketName = _settings.Bucket,
                Key = Filename
            };

            var result = await _client.GetObjectAsync(request);
            LogHelper.Log(LogLevel.INFO, $"Download File Status: {result.HttpStatusCode}");
            using (var sr = new StreamReader(result.ResponseStream))
            {
                var contents = await sr.ReadToEndAsync();
                LogHelper.Log(LogLevel.INFO, $"Downloaded File Contents: '{contents}'");
            }
        }
    }
}