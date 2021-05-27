using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class UserBranchOfficeService : IUserBranchOfficeService
    {
        private readonly IUserBranchOfficeRepository _userBranchOfficeRepository;

        public UserBranchOfficeService(IUserBranchOfficeRepository userBranchOfficeRepository)
        {
            _userBranchOfficeRepository = userBranchOfficeRepository;
        }

        //Basic

        public async Task<int> AddAsync(UserBranchOfficeModel userBranchOfficeModel)
        {
            return await _userBranchOfficeRepository.AddAsync(userBranchOfficeModel);
        }

        public async Task<int> UpdateAsync(UserBranchOfficeModel userBranchOfficeModel)
        {
            return await _userBranchOfficeRepository.UpdateAsync(userBranchOfficeModel);
        }

        public async Task<UserBranchOfficeModel> GetByIdAsync(int userBranchOfficeId)
        {
            return await _userBranchOfficeRepository.GetByIdAsync(userBranchOfficeId);
        }

        public async Task<List<UserBranchOfficeModel>> GetAllAsync()
        {
            return await _userBranchOfficeRepository.GetAllAsync();
        }

        // Advanced
    }
}
