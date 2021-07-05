using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface INoteDetailService : IService<NoteDetailModel>
    {
        Task<List<NoteProductModelDetail>> GetNoteDetailProductByNoteIdAsync(int noteId);
    }
}
