using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IBranchOfficeRepository: IRepository<BranchOfficeModel>
    {
        Task<List<BranchOfficeModel>> GetByCompanyId(int companyId);
        Task<List<BranchOfficeModel>> GetByCode(string code);
    }
}
