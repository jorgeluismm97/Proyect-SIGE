namespace SiGe
{
    public class NoteDetailModel : BaseModel
    {
        public int NoteDetailId { get; set; }
        public int NoteId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
