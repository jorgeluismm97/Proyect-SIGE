namespace SiGe
{
    public class ElectronicDocumentDetail
    {
        public int Correlative { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsExonerate { get; set; }
        public bool IsUnaffected { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ReferencePrice { get; set; }
        public decimal NetSaleValue { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SalesValue { get; set; }
        public decimal TaxAmount { get; set; }
    }
}
