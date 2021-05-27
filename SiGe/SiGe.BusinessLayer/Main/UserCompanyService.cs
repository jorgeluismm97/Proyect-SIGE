using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class UserCompanyService : IUserCompanyService
    {
        private readonly IUserCompanyRepository _usercompanyRepository;

        public UserCompanyService(IUserCompanyRepository usercompanyRepository)
        {
            _usercompanyRepository = usercompanyRepository;
        }

        //Basic

        public async Task<int> AddAsync(UserCompanyModel userCompanyModel)
        {
            return await _usercompanyRepository.AddAsync(userCompanyModel);
        }

        public async Task<int> UpdateAsync(UserCompanyModel userCompanyModel)
        {
            return await _usercompanyRepository.UpdateAsync(userCompanyModel);
        }

        public async Task<UserCompanyModel> GetByIdAsync(int userCompanyId)
        {
            return await _usercompanyRepository.GetByIdAsync(userCompanyId);
        }

        public async Task<List<UserCompanyModel>> GetAllAsync()
        {
            return await _usercompanyRepository.GetAllAsync();
        }
    }
}
