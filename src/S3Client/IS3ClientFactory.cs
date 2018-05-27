using Amazon.S3;

namespace LocalstackS3SetupForNetCore.S3Client
{
    public interface IS3ClientFactory
    {
        IAmazonS3 CreateClient();
    }
}