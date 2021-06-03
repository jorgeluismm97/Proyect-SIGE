using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface ICustomerService : IService<CustomerModel>
    {
        Task<CustomerModel> GetByIdentityDocumentNumberAsync(string identityDocumentNumber);
        Task<List<CustomerModel>> GetByCompanyIdAsync(int companyId);
    }
}
