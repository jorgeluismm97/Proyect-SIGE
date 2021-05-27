using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        //Basic

        public async Task<int> AddAsync(PaymentModel paymentModel)
        {
            return await _paymentRepository.AddAsync(paymentModel);
        }

        public async Task<int> UpdateAsync(PaymentModel paymentModel)
        {
            return await _paymentRepository.UpdateAsync(paymentModel);
        }

        public async Task<PaymentModel> GetByIdAsync(int paymentId)
        {
            return await _paymentRepository.GetByIdAsync(paymentId);
        }

        public async Task<List<PaymentModel>> GetAllAsync()
        {
            return await _paymentRepository.GetAllAsync();
        }

        // Advanced

        public async Task<List<PaymentModel>> GetByCompanyIdAsync(int companyId)
        {
            return await _paymentRepository.GetByCompanyIdAsync(companyId);
        }

        public async Task<List<PaymentModel>> GetByCompanyIdDateAsync(int companyId, DateTime date)
        {
            return await _paymentRepository.GetByCompanyIdDateAsync(companyId,date);
        }
    }
}
