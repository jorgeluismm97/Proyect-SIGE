using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface INoteService : IService<NoteModel>
    {
        Task<List<NoteViewModel>> GetByCompanyIdActionTypeAsync(int companyId, int actionType);
        Task<int> GetNewNumberByActionTypeAsync(int actionType);
        Task<List<ResulViewModel>> GetNoteBalanceKardexSimpleAsync(int productId, int branchOfficeId);
        Task<List<ResulViewModel>> GetNoteKardexSimpleAsync(int productId, int branchOfficeId);
    }
}
