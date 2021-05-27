using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        //Basic

        public async Task<int> AddAsync(NoteModel noteModel)
        {
            return await _noteRepository.AddAsync(noteModel);
        }

        public async Task<int> UpdateAsync(NoteModel noteModel)
        {
            return await _noteRepository.UpdateAsync(noteModel);
        }

        public async Task<NoteModel> GetByIdAsync(int noteId)
        {
            return await _noteRepository.GetByIdAsync(noteId);
        }

        public async Task<List<NoteModel>> GetAllAsync()
        {
            return await _noteRepository.GetAllAsync();
        }
    }
}
