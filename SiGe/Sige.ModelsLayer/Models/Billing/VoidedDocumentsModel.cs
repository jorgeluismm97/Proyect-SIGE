namespace SiGe
{
    public class VoidedDocumentsModel: BaseModel
    {
        public int VoidedDocumentsId { get; set; }
        public int Correlative { get; set; }
        public int Number { get; set; }
        public string NumberTicketCDR { get; set; }
        public string FileName { get; set; }
    }
}
