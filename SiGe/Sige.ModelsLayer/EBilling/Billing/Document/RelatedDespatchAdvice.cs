namespace SiGe
{
    public class RelatedDespatchAdvice
    {
        public TaxPayer Origin { get; set; }
        public TaxPayer Destination { get; set; }
        public string Ruc { get; set; }
        public string DocumentType { get; set; }
        public string Name { get; set; }
        public string NumberLicense { get; set; }
        public string VehiclePlate { get; set; }
        public string CodeAutorization { get; set; }
        public string VehicleBrand { get; set; }
        public string TransportMode { get; set; }
        public string Unit { get; set; }
        public decimal Weigth { get; set; }
    }
}
