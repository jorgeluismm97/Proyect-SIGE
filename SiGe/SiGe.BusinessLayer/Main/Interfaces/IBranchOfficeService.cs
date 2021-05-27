using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IBranchOfficeService : IService<BranchOfficeModel>
    {
        Task<List<BranchOfficeModel>> GetByCompanyId(int companyId);
        Task<List<BranchOfficeModel>> GetByCode(string code);
    }
}
