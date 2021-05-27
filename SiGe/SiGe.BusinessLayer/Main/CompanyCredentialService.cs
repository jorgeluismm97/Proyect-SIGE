using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class CompanyCredentialService : ICompanyCredentialService
    {
        private readonly ICompanyCredentialRepository _companyCredentialRepository;

        public CompanyCredentialService(ICompanyCredentialRepository companyCredentialRepository)
        {
            _companyCredentialRepository = companyCredentialRepository;
        }

        //Basic

        public async Task<int> AddAsync(CompanyCredentialModel companyCredentialModel)
        {
            return await _companyCredentialRepository.AddAsync(companyCredentialModel);
        }

        public async Task<int> UpdateAsync(CompanyCredentialModel companyCredentialModel)
        {
            return await _companyCredentialRepository.UpdateAsync(companyCredentialModel);
        }

        public async Task<CompanyCredentialModel> GetByIdAsync(int companyCredentialId)
        {
            return await _companyCredentialRepository.GetByIdAsync(companyCredentialId);
        }

        public async Task<List<CompanyCredentialModel>> GetAllAsync()
        {
            return await _companyCredentialRepository.GetAllAsync();
        }

    }
}
