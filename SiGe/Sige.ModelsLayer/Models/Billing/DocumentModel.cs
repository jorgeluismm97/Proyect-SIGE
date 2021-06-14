using System;

namespace SiGe
{
    public class DocumentModel : BaseModel
    {
        public int DocumentId { get; set; }
        public int DocumentTypeId { get; set; }
        public int CustomerId { get; set; }
        public string Serie { get; set; }
        public int Number { get; set; }
        public string Observation { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public DateTime IssueDate { get; set; }
        public int BranchOfficeId { get; set; }
        public int MethodPaymentId { get; set; }
        public int UserId { get; set; }
    }
}
