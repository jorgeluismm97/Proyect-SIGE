using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe

{
    public class MethodPaymentService : IMethodPaymentService
    {
        private readonly IMethodPaymentRepository _methodPaymentRepository;

        public MethodPaymentService(IMethodPaymentRepository methodPaymentRepository)
        {
            _methodPaymentRepository = methodPaymentRepository;
        }

        //Basic

        public async Task<int> AddAsync(MethodPaymentModel methodPaymentModel)
        {
            return await _methodPaymentRepository.AddAsync(methodPaymentModel);
        }

        public async Task<int> UpdateAsync(MethodPaymentModel methodPaymentModel)
        {
            return await _methodPaymentRepository.UpdateAsync(methodPaymentModel);
        }

        public async Task<MethodPaymentModel> GetByIdAsync(int methodPaymentId)
        {
            return await _methodPaymentRepository.GetByIdAsync(methodPaymentId);
        }

        public async Task<List<MethodPaymentModel>> GetAllAsync()
        {
            return await _methodPaymentRepository.GetAllAsync();
        }
    }
}
