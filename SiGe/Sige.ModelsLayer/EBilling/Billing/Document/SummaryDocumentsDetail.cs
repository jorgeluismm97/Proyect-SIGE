namespace SiGe
{
    public class SummaryDocumentsDetail
    {
        public string Correlative { get; set; }
        public int DocumentId { get; set; }
        public string DocumentTypeCode { get; set; }
        public string Serie { get; set; }
        public string Number { get; set; }
        public string IdentityDocumentTypeCode { get; set; }
        public string IdentityDocumentNumber { get; set; }
        public bool Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}
