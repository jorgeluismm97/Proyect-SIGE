using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class CompanyCertificateService : ICompanyCertificateService
    {
        private readonly ICompanyCertificateRepository _companyCertificateRepository;

        public CompanyCertificateService(ICompanyCertificateRepository companyCertificateRepository)
        {
            _companyCertificateRepository = companyCertificateRepository;
        }

        //Basic

        public async Task<int> AddAsync(CompanyCertificateModel companyCertificateModel)
        {
            return await _companyCertificateRepository.AddAsync(companyCertificateModel);
        }

        public async Task<int> UpdateAsync(CompanyCertificateModel companyCertificateModel)
        {
            return await _companyCertificateRepository.UpdateAsync(companyCertificateModel);
        }

        public async Task<CompanyCertificateModel> GetByIdAsync(int companyCertificateId)
        {
            return await _companyCertificateRepository.GetByIdAsync(companyCertificateId);
        }

        public async Task<List<CompanyCertificateModel>> GetAllAsync()
        {
            return await _companyCertificateRepository.GetAllAsync();
        }

    }
}
