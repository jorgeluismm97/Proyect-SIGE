namespace SiGe
{
    public class ProductModel : BaseModel
    {
        public int ProductId { get; set; }
        public int CompanyId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
    }
}
