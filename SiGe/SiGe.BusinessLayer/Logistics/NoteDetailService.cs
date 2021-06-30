using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class NoteDetailService : INoteDetailService
    {
        private readonly INoteDetailRepository _noteDetailRepository;

        public NoteDetailService(INoteDetailRepository noteDetailRepository)
        {
            _noteDetailRepository = noteDetailRepository;
        }

        //Basic

        public async Task<int> AddAsync(NoteDetailModel noteDetailModel)
        {
            return await _noteDetailRepository.AddAsync(noteDetailModel);
        }

        public async Task<int> UpdateAsync(NoteDetailModel noteDetailModel)
        {
            return await _noteDetailRepository.UpdateAsync(noteDetailModel);
        }

        public async Task<NoteDetailModel> GetByIdAsync(int noteDetailId)
        {
            return await _noteDetailRepository.GetByIdAsync(noteDetailId);
        }

        public async Task<List<NoteDetailModel>> GetAllAsync()
        {
            return await _noteDetailRepository.GetAllAsync();
        }

        // Advanced
        public async Task<List<NoteProductModelDetail>> GetNoteDetailProductByNoteIdAsync(int noteId)
        {
            return await _noteDetailRepository.GetNoteDetailProductByNoteIdAsync(noteId);
        }
    }
}
