using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface INoteTypeService : IService<NoteTypeModel>
    {
        Task<List<NoteTypeModel>> GetByActionType(int actiontype);

    }
}
