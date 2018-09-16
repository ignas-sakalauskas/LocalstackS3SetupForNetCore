using Newtonsoft.Json;

namespace LocalstackS3SetupForNetCore.Infrastructure.LocalStack
{
    public class GraphRequest
    {
        [JsonProperty(PropertyName = "awsEnvironment")]
        public string AwsEnvironment { get; set; }

        [JsonProperty(PropertyName = "nameFilter")]
        public string NameFilter { get; set; }
    }
}
