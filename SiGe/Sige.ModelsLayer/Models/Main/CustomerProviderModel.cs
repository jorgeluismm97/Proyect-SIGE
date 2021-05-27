namespace SiGe
{
    public class CustomerProviderModel: BaseModel
    {
        public int CustomerProviderId { get; set; }
        public int IdentityDocumentTypeId { get; set; }
        public string IdentityDocumentNumber { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }

    }
}
