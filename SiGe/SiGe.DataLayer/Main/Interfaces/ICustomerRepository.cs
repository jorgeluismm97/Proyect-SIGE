using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{ 
    public interface ICustomerRepository  : IRepository<CustomerModel>
    {
        Task<CustomerModel> GetByIdentityDocumentNumberAsync(string identityDocumentNumber);
        Task<List<CustomerModel>> GetByCompanyIdAsync(int companyId);
    }
}
