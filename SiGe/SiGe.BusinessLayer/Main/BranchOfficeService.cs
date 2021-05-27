using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class BranchOfficeService : IBranchOfficeService
    {
        private readonly IBranchOfficeRepository _branchOfficeRepository;

        public BranchOfficeService(IBranchOfficeRepository branchOfficeRepository)
        {
            _branchOfficeRepository = branchOfficeRepository;
        }

        //Basic

        public async Task<int> AddAsync(BranchOfficeModel branchOfficeModel)
        {
            return await _branchOfficeRepository.AddAsync(branchOfficeModel);
        }

        public async Task<int> UpdateAsync(BranchOfficeModel branchOfficeModel)
        {
            return await _branchOfficeRepository.UpdateAsync(branchOfficeModel);
        }

        public async Task<BranchOfficeModel> GetByIdAsync(int branchOfficeId)
        {
            return await _branchOfficeRepository.GetByIdAsync(branchOfficeId);
        }

        public async Task<List<BranchOfficeModel>> GetAllAsync()
        {
            return await _branchOfficeRepository.GetAllAsync();
        }

        // Advanced

        public async Task<List<BranchOfficeModel>> GetByCompanyId(int companyId)
        {
            return await _branchOfficeRepository.GetByCompanyId(companyId);
        }

        public async Task<List<BranchOfficeModel>> GetByCode(string code)
        {
            return await _branchOfficeRepository.GetByCode(code);
        }
    }
}
