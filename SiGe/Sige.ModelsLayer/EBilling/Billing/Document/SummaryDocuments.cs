using System;
using System.Collections.Generic;
using System.Text;

namespace SiGe
{
    public class SummaryDocuments
    {
        public TaxPayer Company { get; set; }
        public string DocumentTypeCode { get; set; }
        public int Correlative { get; set; }
        public int Number { get; set; }
        public DateTime IssueDate { get; set; }
        public List<SummaryDocumentsDetail> Items { get; set; }

        public SummaryDocuments()
        {
            Company = new TaxPayer();
            Items = new List<SummaryDocumentsDetail>();
        }
    }
}
