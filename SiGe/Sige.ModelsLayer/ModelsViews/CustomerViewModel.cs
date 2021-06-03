using System;
using System.Collections.Generic;
using System.Text;

namespace SiGe
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public List<IdentityDocumentTypeModel> IdentityDocumentTypes { get; set; }
        public int IdentityDocumentTypeId { get; set; }
        public string IdentityDocumentNumber { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
