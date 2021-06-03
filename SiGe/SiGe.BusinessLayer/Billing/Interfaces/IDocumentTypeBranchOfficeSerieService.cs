using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IDocumentTypeBranchOfficeSerieService : IService<DocumentTypeBranchOfficeSerieModel>
    {
        Task<List<SerieViewModel>> GetByCompanyIdAsync(int companyId);
    }
}
