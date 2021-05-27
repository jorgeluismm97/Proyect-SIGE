using System;

namespace SiGe
{
    public class DocumentElectronicModel : BaseModel
    {
        public int DocumentElectronicId { get; set; }
        public int DocumentId { get; set; }
        public DateTime IssueDate { get; set; }
        public string SummaryValue { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string NumberTicketCDR { get; set; }
        public string StringQR { get; set; }
        public string FileName {get; set; }
        public bool IsSent { get; set; }
    }
}
