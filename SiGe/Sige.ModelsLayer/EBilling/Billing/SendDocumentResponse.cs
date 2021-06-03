using System;

namespace SiGe
{
    public class SendDocumentResponse : FileCommonResponse
    {
        public DateTime IssueDate { get; set; }
        public string CodeResponse { get; set; }
        public string MessageResponse { get; set; }
        public string StringZipCdr { get; set; }
        public string TicketCdr { get; set; }
    }
}
