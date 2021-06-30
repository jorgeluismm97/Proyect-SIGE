using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe
{
    public class NoteTypeService : INoteTypeService
    {
        private readonly INoteTypeRepository _noteTypeRepository;

        public NoteTypeService(INoteTypeRepository noteTypeRepository)
        {
            _noteTypeRepository = noteTypeRepository;
        }

        //Basic

        public async Task<int> AddAsync(NoteTypeModel noteTypeModel)
        {
            return await _noteTypeRepository.AddAsync(noteTypeModel);
        }

        public async Task<int> UpdateAsync(NoteTypeModel noteTypeModel)
        {
            return await _noteTypeRepository.UpdateAsync(noteTypeModel);
        }
        
        public async Task<NoteTypeModel> GetByIdAsync(int noteTypeId)
        {
            return await _noteTypeRepository.GetByIdAsync(noteTypeId);
        }

        public async Task<List<NoteTypeModel>> GetAllAsync()
        {
            return await _noteTypeRepository.GetAllAsync();
        }



        // Advanced

        public async Task<List<NoteTypeModel>> GetByActionType(int actiontype)
        {
            var noteType = await GetAllAsync();

            var noteTypeResult = noteType.Where(x => x.ActionType == actiontype && x.NoteTypeId != 3 && x.NoteTypeId != 8).ToList();

            return noteTypeResult;
        }
    }
}
