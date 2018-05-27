using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using LocalstackS3SetupForNetCore.Configuration;

namespace LocalstackS3SetupForNetCore.LocalstackTests
{
    public class UploadFile : ILocalstackTest
    {
        private const string Filename = "uploadme.txt";

        private readonly IAmazonS3 _client;
        private readonly LocalstackSettings _settings;

        public UploadFile(IAmazonS3 client, LocalstackSettings settings)
        {
            _client = client;
            _settings = settings;
        }

        public async Task Execute()
        {
            using (var transferUtility = new TransferUtility(_client))
            {
                using (var sr = new StreamReader(Filename))
                {
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = sr.BaseStream,
                        ContentType = "content/text",
                        BucketName = _settings.Bucket,
                        Key = Filename,
                        TagSet = new List<Tag>
                        {
                            new Tag { Key = "key1", Value = "value1" },
                            new Tag { Key = "key2", Value = "value2" }
                        }
                    };

                    await transferUtility.UploadAsync(uploadRequest);
                    Console.WriteLine($"{DateTime.Now} - Upload File - DONE");
                }
            }
        }
    }
}