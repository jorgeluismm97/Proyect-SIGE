using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IDocumentTypeBranchOfficeSerieRepository : IRepository<DocumentTypeBranchOfficeSerieModel>
    {
        Task<List<SerieViewModel>> GetByCompanyIdAsync(int companyId);
        Task<List<SerieViewModel>> GetByCompanyIdDocumentTypeIdAsync(int companyId, int documentTypeId);
    }
}
