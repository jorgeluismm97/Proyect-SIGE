using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IDocumentService : IService<DocumentModel>
    {
        Task<List<DocumentViewModel>> GetByCompanyIdAsync(int companyId);
        Task<int> GetNewNumberByDocumentTypeIdSerieIndicatorAsync(int companyId, int documentTypeId, string serie);
    }
}
