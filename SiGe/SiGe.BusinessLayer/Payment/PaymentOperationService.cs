using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class PaymentOperationService : IPaymentOperationService
    {
        private readonly IPaymentOperationRepository _paymentOperationRepository;

        public PaymentOperationService(IPaymentOperationRepository paymentOperationRepository)
        {
            _paymentOperationRepository = paymentOperationRepository;
        }

        //Basic

        public async Task<int> AddAsync(PaymentOperationModel paymentOperationModel)
        {
            return await _paymentOperationRepository.AddAsync(paymentOperationModel);
        }

        public async Task<int> UpdateAsync(PaymentOperationModel paymentOperationModel)
        {
            return await _paymentOperationRepository.UpdateAsync(paymentOperationModel);
        }

        public async Task<PaymentOperationModel> GetByIdAsync(int paymentOperationId)
        {
            return await _paymentOperationRepository.GetByIdAsync(paymentOperationId);
        }

        public async Task<List<PaymentOperationModel>> GetAllAsync()
        {
            return await _paymentOperationRepository.GetAllAsync();
        }
    }
}
