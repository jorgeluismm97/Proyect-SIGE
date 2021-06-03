using System;
using System.Collections.Generic;

namespace SiGe
{
    public class VoidedDocuments  
    {
        public TaxPayer Company { get; set; }
        public string DocumentTypeCode { get; set; }
        public int Correlative { get; set; }
        public int Number { get; set; }
        public DateTime IssueDate { get; set; }
        public List<VoidedDocumentsDetail> Items { get; set; }

        public VoidedDocuments()
        {
            Company = new TaxPayer();
            Items = new List<VoidedDocumentsDetail>();
        }
    }
}
