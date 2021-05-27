using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<UserModel> ValidateAsync(string userName, string password);
        Task<List<UserViewModel>> GetByCompanyId(int companyId);
    }
}
