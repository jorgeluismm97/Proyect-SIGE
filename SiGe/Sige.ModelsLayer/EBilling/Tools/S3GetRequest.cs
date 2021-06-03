using System.Collections.Generic;

namespace SiGe
{
    public class S3GetRequest
    {
        public string AwsAccessKeyId { get; set; }
        public string AwsSecretAccessKey { get; set; }
        public string BucketName { get; set; }
        public string KeyName { get; set; }
        public List<S3Detail> Files { get; set; }
        public S3GetRequest()
        {
            Files = new List<S3Detail>();
        }
    }
}
