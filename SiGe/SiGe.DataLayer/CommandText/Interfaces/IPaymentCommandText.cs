namespace SiGe
{
    public interface IPaymentCommandText
    {
        // PaymentPlan
        string AddPaymentPlan { get; }
        string UpdatePaymentPlan { get; }
        string GetPaymentPlanById { get; }
        string GetAllPaymentPlan { get; }

        // Payment

        string AddPayment { get; }
        string UpdatePayment { get; }
        string GetPaymentById { get; }
        string GetAllPayment { get; }
        string GetPaymentByCompanyId { get; }
        string GetPaymentByCompanyIdDate { get; }

        // PaymentOperation

        string AddPaymentOperation { get; }
        string UpdatePaymentOperation { get; }
        string GetPaymentOperationById { get; }
        string GetAllPaymentOperation { get; }
    }
}
