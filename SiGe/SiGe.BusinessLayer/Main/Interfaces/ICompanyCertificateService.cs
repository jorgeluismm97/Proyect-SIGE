using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface ICompanyCertificateService : IService<CompanyCertificateModel>
    {
        Task<List<CompanyCertificateModel>> GetByCompanyIdAsync(int companyId);
    }
}
