namespace SiGe
{
    public class PaymentOperationModel : BaseModel
    {
        public int PaymentOperationId { get; set; }
        public int PaymentId { get; set; }
        public string StringPaymentId { get; set; }
        public string StringPayerId { get; set; }
    }
}
