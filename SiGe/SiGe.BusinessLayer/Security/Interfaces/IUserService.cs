using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IUserService : IService<UserModel>
    {
        Task<UserModel> ValidateAsync(string userName, string password);
        Task<List<UserViewModel>> GetByCompanyId(int companyId);
    }
}
