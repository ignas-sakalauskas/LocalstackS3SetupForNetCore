namespace LocalstackS3SetupForNetCore.Configuration
{
    public class LocalstackSettings
    {
        public string ServiceUrl => $"http://{ProxyHostname}:{ProxyPort}/";
        public string ProxyHostname { get; set; } = "localstack";
        public int ProxyPort { get; set; } = 4572;
        public string Bucket { get; set; } = "mybucket";
    }
}