using System;
using System.Collections.Generic;

namespace SiGe
{
    public class PdfRequest
    {
        public string Logo { get; set; }
        public string Color { get; set; }
        public DocumentHeader Document { get; set; }
        public DocumentHeader DocumentReference { get; set; }
        public Company Company { get; set; }
        public BranchOffice BranchOffice { get; set; }
        public CustomerProvider CustomerProvider { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public List<DocumentDetail> DocumentDetails { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Amount { get; set; }
        public bool IsElectronic { get; set; }
        public string MethodPayment { get; set; }
        public DocumentElectronic DocumentElectronic { get; set; }
        public CurrentAccount CurrentAccount { get; set; }
        public AdditionalData AdditionalData { get; set; }

        public PdfRequest()
        {
            Document = new DocumentHeader();
            DocumentReference = new DocumentHeader();
            Company = new Company();
            BranchOffice = new BranchOffice();
            CustomerProvider = new CustomerProvider();
            DocumentDetails = new List<DocumentDetail>();
            DocumentElectronic = new DocumentElectronic();
            CurrentAccount = new CurrentAccount();
            AdditionalData = new AdditionalData();
        }
    }
}
