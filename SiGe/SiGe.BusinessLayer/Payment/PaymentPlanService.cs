using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class PaymentPlanService : IPaymentPlanService
    {
        private readonly IPaymentPlanRepository _paymentPlanRepository;
        public PaymentPlanService(IPaymentPlanRepository paymentPlanRepository)
        {
            _paymentPlanRepository = paymentPlanRepository;
        }

        //Basic

        public async Task<int> AddAsync(PaymentPlanModel paymentPlanModel)
        {
            return await _paymentPlanRepository.AddAsync(paymentPlanModel);
        }

        public async Task<int> UpdateAsync(PaymentPlanModel paymentPlanModel)
        {
            return await _paymentPlanRepository.UpdateAsync(paymentPlanModel);
        }

        public async Task<PaymentPlanModel> GetByIdAsync(int paymentPlanId)
        {
            return await _paymentPlanRepository.GetByIdAsync(paymentPlanId);
        }

        public async Task<List<PaymentPlanModel>> GetAllAsync()
        {
            return await _paymentPlanRepository.GetAllAsync();
        }
    }
}
