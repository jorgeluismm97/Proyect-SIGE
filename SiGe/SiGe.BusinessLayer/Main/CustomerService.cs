using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerProviderRepository;

        public CustomerService(ICustomerRepository customerProviderRepository)
        {
            _customerProviderRepository = customerProviderRepository;
        }

        //Basic

        public async Task<int> AddAsync(CustomerModel customerProviderModel)
        {
            return await _customerProviderRepository.AddAsync(customerProviderModel);
        }

        public async Task<int> UpdateAsync(CustomerModel customerProviderModel)
        {
            return await _customerProviderRepository.UpdateAsync(customerProviderModel);
        }

        public async Task<CustomerModel> GetByIdAsync(int customerProviderId)
        {
            return await _customerProviderRepository.GetByIdAsync(customerProviderId);
        }

        public async Task<List<CustomerModel>> GetAllAsync()
        {
            return await _customerProviderRepository.GetAllAsync();
        }

        // Advanced
        public async Task<CustomerModel> GetByIdentityDocumentNumberAsync(string identityDocumentNumber)
        {
            return await _customerProviderRepository.GetByIdentityDocumentNumberAsync(identityDocumentNumber);
        }

        public async Task<List<CustomerModel>> GetByCompanyIdAsync(int companyId)
        {
            return await _customerProviderRepository.GetByCompanyIdAsync(companyId);
        }

    }
}
