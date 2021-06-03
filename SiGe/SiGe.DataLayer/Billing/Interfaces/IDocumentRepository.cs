using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IDocumentRepository : IRepository<DocumentModel>
    {
        Task<List<DocumentViewModel>> GetByCompanyIdAsync(int companyId);
    }
}
