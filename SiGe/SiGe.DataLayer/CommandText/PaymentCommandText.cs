namespace SiGe
{
    public class PaymentCommandText : IPaymentCommandText
    {
        // PaymentPlan

        public string AddPaymentPlan => "Usp_Pay_I_Pay_PaymentPlan";
        public string UpdatePaymentPlan => "Usp_Pay_U_Pay_PaymentPlan";
        public string GetPaymentPlanById => "Usp_Pay_S_Pay_PaymentPlan_Get_By_Id";
        public string GetAllPaymentPlan => "Usp_Pay_S_Pay_PaymentPlan";

        // Payment

        public string AddPayment => "Usp_Pay_I_Pay_Payment";
        public string UpdatePayment => "Usp_Pay_U_Pay_Payment";
        public string GetPaymentById => "Usp_Pay_S_Pay_Payment_Get_By_Id";
        public string GetAllPayment => "Usp_Pay_S_Pay_Payment";
        public string GetPaymentByCompanyId => "Usp_Pay_S_Pay_Payment_Get_By_CompanyId";
        public string GetPaymentByCompanyIdDate => "Usp_Pay_S_Pay_Payment_Get_By_CompanyId_Date";

        // PaymentOperation

        public string AddPaymentOperation => "Usp_Pay_I_Pay_PaymentOperation";
        public string UpdatePaymentOperation => "Usp_Pay_U_Pay_PaymentOperation";
        public string GetPaymentOperationById => "Usp_Pay_S_Pay_PaymentOperation_Get_By_Id";
        public string GetAllPaymentOperation => "Usp_Pay_S_Pay_PaymentOperation";

    }
}
