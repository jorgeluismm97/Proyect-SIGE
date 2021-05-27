namespace SiGe
{
    public class CompanyCertificateModel : BaseModel
    {
        public int CompanyCertificateId { get; set; }
        public int CompanyId { get; set; }
        public string UserSOL { get; set; }
        public string PasswordSOL { get; set; }
    }
}
