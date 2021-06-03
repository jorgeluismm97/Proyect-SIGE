using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiGe
{
    public class CompanyCertificateViewModel
    {
        public int CompanyCertificateId { get; set; }
        public int CompanyId { get; set; }
        public IFormFile File { get; set; }
        public string Password { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
