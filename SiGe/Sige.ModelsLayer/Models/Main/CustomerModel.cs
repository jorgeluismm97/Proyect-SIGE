namespace SiGe
{
    public class CustomerModel: BaseModel
    {
        public int CustomerId { get; set; }
        public int CompanyId { get; set; }
        public int IdentityDocumentTypeId { get; set; }
        public string IdentityDocumentNumber { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

    }
}
