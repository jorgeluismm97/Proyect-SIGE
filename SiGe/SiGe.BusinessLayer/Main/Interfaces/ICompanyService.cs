using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface ICompanyService : IService<CompanyModel>
    {
        Task<CompanyModel> GetByIdentityDocumentNumberAsync(string identityDocumnetNumber);
        Task<List<CompanyModel>> GetByUserIdAsync(int userId);
        Task<CompanyModel> GetByUserNameIdentityDocumentNumberAsync(string userName, string identityDocumentNumber);
    }
}
