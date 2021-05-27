using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //Basic

        public async Task<int> AddAsync(UserModel userModel)
        {
            return await _userRepository.AddAsync(userModel);
        }

        public async Task<int> UpdateAsync(UserModel userModel)
        {
            return await _userRepository.UpdateAsync(userModel);
        }

        public async Task<UserModel> GetByIdAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        // Advanced


        public async Task<UserModel> ValidateAsync(string userName, string password)
        {
            return await _userRepository.ValidateAsync(userName, password);
        }

        public async Task<List<UserViewModel>> GetByCompanyId(int companyId)
        {
         return await _userRepository.GetByCompanyId(companyId);
        }

    }
}