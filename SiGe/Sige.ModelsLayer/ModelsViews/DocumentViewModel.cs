using System;
using System.Collections.Generic;
using System.Text;

namespace SiGe
{
    public class DocumentViewModel
    {
        public int DocumentId { get; set; }
        public DateTime IssueDate { get; set; }
        public string IdentityDocumentNumber { get; set; }
        public string Document { get; set; }
        public string BussinesName { get; set; }
        public decimal Total { get; set; }
    }
}
