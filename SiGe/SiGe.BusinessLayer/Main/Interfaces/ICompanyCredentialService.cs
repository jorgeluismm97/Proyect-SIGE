using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface ICompanyCredentialService : IService<CompanyCredentialModel>
    {
        Task<List<CompanyCredentialModel>> GetByCompanyIdAsync(int companyId);
    }
}
