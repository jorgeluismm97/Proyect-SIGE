using System;
using System.Collections.Generic;

namespace SiGe
{
    public class ElectronicDocument 
    {
        public string Document { get; set; }
        public string DocumentType { get; set; }
        public TaxPayer Company { get; set; }
        public TaxPayer Customer { get; set; }
        public DateTime IssueDate { get; set; }
        public string Currency { get; set; }
        public decimal TaxedAmount { get; set; }
        public decimal AllowanceAmount { get; set; }
        public decimal UnaffectedAmount { get; set; }
        public decimal ExoneratedAmount { get; set; }
        public decimal GlobalDiscount { get; set; }
        public decimal GlobalDiscountAmount { get; set; }
        public List<ElectronicDocumentDetail> Items { get; set; }
        public decimal Amount { get; set; }
        public string LettersAmount { get; set; }
        public RelatedDocument RelatedDocument { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxAmount { get; set; }

        public ElectronicDocument()
        {
            Company = new TaxPayer();
            Customer = new TaxPayer();
            Items = new List<ElectronicDocumentDetail>();
            RelatedDocument = new RelatedDocument();
            Currency = "PEN";
            Tax = 0.18M;
        }
    }
}
