using System;
using System.ComponentModel.DataAnnotations;

namespace SiGe
{
    public class CompanyCredentialModel : BaseModel
    {
        public int CompanyCredentialId { get; set; }
        public int CompanyId { get; set; }
        [Required]
        public string UserSOL { get; set; }
        [Required]
        public string PasswordSOL { get; set; }
    }
}
