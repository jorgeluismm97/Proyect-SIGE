using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface INoteDetailRepository : IRepository<NoteDetailModel>
    {
        Task<List<NoteProductModelDetail>> GetNoteDetailProductByNoteIdAsync(int noteId);
    }
}
