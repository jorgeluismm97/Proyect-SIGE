using System.Collections.Generic;

namespace SiGe
{
    public class S3GetResponse : CommonResponse
    {
        public List<S3Detail> Files { get; set; }
        public S3GetResponse()
        {
            Files = new List<S3Detail>();
        }
    }
}
