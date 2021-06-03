using System;
using System.ComponentModel.DataAnnotations;

namespace SiGe
{
    public class CompanyCertificateModel : BaseModel
    {
        public int CompanyCertificateId { get; set; }
        public int CompanyId { get; set; }
        public string Value { get; set; }
        public string Password { get; set; }
        public DateTime ExpiredDate { get; set; }

    }
}
