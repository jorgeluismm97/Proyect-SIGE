namespace SiGe
{
    public class SignedRequest
    {
        public string DigitalCertificate { get; set; }
        public string CertificatePassword { get; set; }
        public string StringXmlUnsigned { get; set; }
        public bool SingleNode { get; set; }
    }
}
