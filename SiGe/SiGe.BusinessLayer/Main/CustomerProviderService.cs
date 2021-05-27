using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class CustomerProviderService : ICustomerProviderService
    {
        private readonly ICustomerProviderRepository _customerProviderRepository;

        public CustomerProviderService(ICustomerProviderRepository customerProviderRepository)
        {
            _customerProviderRepository = customerProviderRepository;
        }

        //Basic

        public async Task<int> AddAsync(CustomerProviderModel customerProviderModel)
        {
            return await _customerProviderRepository.AddAsync(customerProviderModel);
        }

        public async Task<int> UpdateAsync(CustomerProviderModel customerProviderModel)
        {
            return await _customerProviderRepository.UpdateAsync(customerProviderModel);
        }

        public async Task<CustomerProviderModel> GetByIdAsync(int customerProviderId)
        {
            return await _customerProviderRepository.GetByIdAsync(customerProviderId);
        }

        public async Task<List<CustomerProviderModel>> GetAllAsync()
        {
            return await _customerProviderRepository.GetAllAsync();
        }
    }
}
