using Amazon.S3;
using LocalstackS3SetupForNetCore.Configuration;
using LocalstackS3SetupForNetCore.S3Client;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LocalstackS3SetupForNetCore.Infrastructure.Logging;

namespace LocalstackS3SetupForNetCore.Infrastructure.LocalStack
{
    public class LocalstackSetup
    {
        private readonly LocalstackSettings _settings;
        private readonly IAmazonS3 _s3Client;

        public LocalstackSetup(LocalstackSettings settings, IS3ClientFactory s3ClientFactory)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            if (s3ClientFactory == null)
                throw new ArgumentNullException(nameof(s3ClientFactory));

            _s3Client = s3ClientFactory.CreateClient();
        }

        public async Task WaitForInit(TimeSpan timeout)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(timeout);

            var request = new GraphRequest
            {
                AwsEnvironment = "dev",
                NameFilter = _settings.Bucket
            };

            while (!cts.IsCancellationRequested)
            {
                try
                {
                    using (var httpCliet = new HttpClient())
                    {
                        LogHelper.Log(LogLevel.INFO, "Checking LocalStack health...");
                        
                        httpCliet.Timeout = TimeSpan.FromSeconds(3);
                        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        var response = await httpCliet.PostAsync(_settings.DashboardGraphUrl, content, cts.Token);
                        if (response.IsSuccessStatusCode)
                        {
                            await _s3Client.EnsureBucketExistsAsync(_settings.Bucket);
                            LogHelper.Log(LogLevel.INFO, $"Bucket {_settings.Bucket} exists!");
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Log(LogLevel.WARN, $"Check response: {ex.Message}");
                    await Task.Delay(1000, cts.Token);
                }
            }
        }
    }
}