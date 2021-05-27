using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IPersonRepository : IRepository<PersonModel>
    {
        Task<PersonModel> GetByIdentityDocumentTypeIdIdentityDocumentNumberAsync(int identityDocumentTypeId, string identityDocumentNumber);
        Task<List<PersonModel>> GetByCompanyIdAsync(int companyId);
        Task<List<PersonModel>> GetWithOutUserByCompanyIdAsync(int companyId);
    }
}
