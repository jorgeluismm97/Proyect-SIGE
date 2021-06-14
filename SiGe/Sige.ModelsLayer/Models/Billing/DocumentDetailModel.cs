namespace SiGe
{
    public class DocumentDetailModel : BaseModel
    {
        public int DocumentDetailId { get; set; }
        public int DocumentId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
