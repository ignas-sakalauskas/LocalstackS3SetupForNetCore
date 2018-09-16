using Amazon.S3;
using LocalstackS3SetupForNetCore.Configuration;

namespace LocalstackS3SetupForNetCore.S3Client
{
    public class LocalstackS3ClientFactory : IS3ClientFactory
    {
        private readonly LocalstackSettings _settings;

        public LocalstackS3ClientFactory(LocalstackSettings settings)
        {
            _settings = settings;
        }

        public IAmazonS3 CreateClient()
        {
            // Localstack service S3 URL
            var client = new AmazonS3Client(new AmazonS3Config
            {
                ServiceURL = _settings.ServiceUrl,
                // Localstack supports HTTP only
                UseHttp = true,
                // Force bucket name go *after* hostname 
                ForcePathStyle = true,
                // Use proxy to force aws client calls to localstack
                ProxyHost = _settings.ProxyHostname,
                ProxyPort = _settings.ProxyPort,
            });

            return client;
        }
    }
}