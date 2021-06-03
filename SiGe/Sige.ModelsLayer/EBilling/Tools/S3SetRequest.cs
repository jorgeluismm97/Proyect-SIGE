using System.Collections.Generic;

namespace SiGe
{
    public class S3SetRequest
    {
        public string AwsAccessKeyId { get; set; }
        public string AwsSecretAccessKey { get; set; }
        public string BucketName { get; set; }
        public string KeyName { get; set; }
        public List<S3Detail> Files { get; set; } = new List<S3Detail>();

        public S3SetRequest()
        {
            Files = new List<S3Detail>();
        }
    }
}
