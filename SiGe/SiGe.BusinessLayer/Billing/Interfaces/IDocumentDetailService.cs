using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IDocumentDetailService: IService<DocumentDetailModel>
    {
        Task<List<NoteProductModelDetail>> GetDocumentDetailProductByDocumentIdAsync(int documentId);
        Task<List<DocumentDetailModel>> GetByDocumentIdAsync(int documentId);
    }
}
