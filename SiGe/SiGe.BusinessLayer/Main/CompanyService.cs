using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        //Basic

        public async Task<int> AddAsync(CompanyModel companyModel)
        {
            return await _companyRepository.AddAsync(companyModel);
        }

        public async Task<int> UpdateAsync(CompanyModel companyModel)
        {
            return await _companyRepository.UpdateAsync(companyModel);
        }

        public async Task<CompanyModel> GetByIdAsync(int companyId)
        {
            return await _companyRepository.GetByIdAsync(companyId);
        }

        public async Task<List<CompanyModel>> GetAllAsync()
        {
            return await _companyRepository.GetAllAsync();
        }

        // Advanced

        public async Task<CompanyModel> GetByIdentityDocumentNumberAsync(string identityDocumnetNumber)
        {
            return await _companyRepository.GetByIdentityDocumentNumberAsync(identityDocumnetNumber);
        }

        public async Task<List<CompanyModel>> GetByUserIdAsync(int userId)
        {
            return await _companyRepository.GetByUserIdAsync(userId);
        }
        public async  Task<CompanyModel> GetByUserNameIdentityDocumentNumberAsync(string userName, string identityDocumentNumber)
        {
            return await _companyRepository.GetByUserNameIdentityDocumentNumberAsync(userName, identityDocumentNumber);
        }
    }
}
