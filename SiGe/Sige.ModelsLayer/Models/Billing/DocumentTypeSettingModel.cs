namespace SiGe
{
    public class DocumentTypeSettingModel :BaseModel
    {
        public int DocumentTypeSettingId { get; set; }
        public int DocumentTypeId { get; set; }
        public bool Emit { get; set; }
        public bool Receive { get; set; }
    }
}
