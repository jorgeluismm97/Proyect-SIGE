using System;

namespace SiGe
{
    public class CompanyCredentialModel : BaseModel
    {
        public int CompanyCredentialId { get; set; }
        public int CompanyId { get; set; }
        public string Value { get; set; }
        public string Password { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
