using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface ICompanyCredentialRepository : IRepository<CompanyCredentialModel>
    {
        Task<List<CompanyCredentialModel>> GetByCompanyIdAsync(int companyId);
    }
}
