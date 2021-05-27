namespace SiGe
{
    public class PaymentPlanModel : BaseModel
    {
        public int PaymentPlanId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
