namespace LocalstackS3SetupForNetCore.Configuration
{
    public class LocalstackSettings
    {
        public string ServiceUrl => $"http://{ProxyHostname}:{ProxyPort}/";
        public string ProxyHostname { get; set; } = "localstack-s3test";
        public int ProxyPort { get; set; } = 4572;
        public string Bucket { get; set; } = "mybucket";
        public string DashboardGraphUrl { get; set; } = "http://localstack-s3test:8080/graph";
    }
}