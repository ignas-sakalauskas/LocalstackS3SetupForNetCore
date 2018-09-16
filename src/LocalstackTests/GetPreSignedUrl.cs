using System;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using LocalstackS3SetupForNetCore.Configuration;
using LocalstackS3SetupForNetCore.Infrastructure.Logging;

namespace LocalstackS3SetupForNetCore.LocalstackTests
{
    public class GetPreSignedUrl : ILocalstackTest
    {
        private const string Filename = "uploadme.txt";

        private readonly IAmazonS3 _client;
        private readonly LocalstackSettings _settings;

        public GetPreSignedUrl(IAmazonS3 client, LocalstackSettings settings)
        {
            _client = client;
            _settings = settings;
        }

        public async Task Execute()
        {
            var result = _client.GetPreSignedURL(new GetPreSignedUrlRequest
            {
                BucketName = _settings.Bucket,
                Key = Filename,
                Expires = DateTime.Now.AddHours(1),
                Protocol = Protocol.HTTP
            });
            
            LogHelper.Log(LogLevel.INFO, $"Get pre-signed URL result: {result}");
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(result);
                var contents = await response.Content.ReadAsStringAsync();
                LogHelper.Log(LogLevel.INFO, $"Testing the URL. Received File Contents: '{contents}'");
            }
        }
    }
}