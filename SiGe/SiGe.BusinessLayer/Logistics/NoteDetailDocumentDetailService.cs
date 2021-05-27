using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class NoteDetailDocumentDetailService : INoteDetailDocumentDetailService
    {
        private readonly INoteDetailDocumentDetailRepository _noteDetailDocumentDetailRepository;

        public NoteDetailDocumentDetailService(INoteDetailDocumentDetailRepository noteDetailDocumentDetailRepository)
        {
            _noteDetailDocumentDetailRepository = noteDetailDocumentDetailRepository;
        }

        //Basic

        public async Task<int> AddAsync(NoteDetailDocumentDetailModel noteDetailDocumentDetailModel)
        {
            return await _noteDetailDocumentDetailRepository.AddAsync(noteDetailDocumentDetailModel);
        }

        public async Task<int> UpdateAsync(NoteDetailDocumentDetailModel noteDetailDocumentDetailModel)
        {
            return await _noteDetailDocumentDetailRepository.UpdateAsync(noteDetailDocumentDetailModel);
        }

        public async Task<NoteDetailDocumentDetailModel> GetByIdAsync(int noteDetailDocumentDetailId)
        {
            return await _noteDetailDocumentDetailRepository.GetByIdAsync(noteDetailDocumentDetailId);
        }

        public async Task<List<NoteDetailDocumentDetailModel>> GetAllAsync()
        {
            return await _noteDetailDocumentDetailRepository.GetAllAsync();
        }
    }
}
