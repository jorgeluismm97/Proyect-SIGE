using System.Collections.Generic;

namespace SiGe
{
    public class EmailRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string BusinessName { get; set; }
        public string Addressee { get; set; }
        public string Affair { get; set; }
        public string Content { get; set; }
        public List<EmailDetailRequest> Files{ get; set; }

        public EmailRequest()
        {
            Files = new List<EmailDetailRequest>();
        }
    }
}
