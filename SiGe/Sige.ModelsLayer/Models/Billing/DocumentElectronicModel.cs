using System;
using System.ComponentModel.DataAnnotations;

namespace SiGe
{
    public class DocumentElectronicModel : BaseModel
    {
        public int DocumentElectronicId { get; set; }
        public int DocumentId { get; set; }
        public DateTime IssueDate { get; set; }
        public string SummaryValue { get; set; }
        [Display(Name = "Código de Respuesta")]
        public string ResponseCode { get; set; }
        [Display(Name = "Mensaje de Respuesta")]
        public string ResponseMessage { get; set; }
        [Display(Name = "Número de CDR")]
        public string NumberTicketCDR { get; set; }
        public string StringQR { get; set; }
        public string FileName {get; set; }
    }
}
