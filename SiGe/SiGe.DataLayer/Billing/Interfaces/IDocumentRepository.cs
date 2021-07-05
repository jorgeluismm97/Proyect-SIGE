using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IDocumentRepository : IRepository<DocumentModel>
    {
        Task<List<DocumentViewModel>> GetByCompanyIdAsync(int companyId);
        Task<List<DocumentViewModel>> GetByCompanyIdDateAsync(int companyId, DateTime startingDate, DateTime endingDate);
        Task<List<MethodPaymentViewModel>> GetMethodPaymentByCompanyIdDateAsync(int companyId, DateTime startingDate, DateTime endingDate);
        Task<int> GetNewNumberByDocumentTypeIdSerieIndicatorAsync(int companyId, int documentTypeId, string serie);
    }
}
