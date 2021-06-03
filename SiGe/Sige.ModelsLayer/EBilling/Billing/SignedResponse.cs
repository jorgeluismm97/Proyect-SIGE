namespace SiGe
{
    public class SignedResponse : CommonResponse
    {
        public string StringXmlSigned { get; set; }
        public string SignatureSummary { get; set; }
        public string SignatureValue { get; set; }
    }
}
