using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface ICompanyCertificateRepository: IRepository<CompanyCertificateModel>
    {
        Task<List<CompanyCertificateModel>> GetByCompanyIdAsync(int companyId);
    }
}
