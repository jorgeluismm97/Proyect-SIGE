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

        // Advanced

        public async Task<List<NoteViewModel>> GetByCompanyIdActionTypeAsync(int companyId, int actionType)
        {
            return await _noteRepository.GetByCompanyIdActionTypeAsync(companyId, actionType);
        }

        public async Task<int> GetNewNumberByActionTypeAsync(int actionType)
        {
            return await _noteRepository.GetNewNumberByActionTypeAsync(actionType);
        }

        public async Task<List<ResulViewModel>> GetNoteBalanceKardexSimpleAsync(int productId, int branchOfficeId)
        {
            return await _noteRepository.GetNoteBalanceKardexSimpleAsync(productId, branchOfficeId);
        }

        public async Task<List<ResulViewModel>> GetNoteKardexSimpleAsync(int productId, int branchOfficeId)
        {
            return await _noteRepository.GetNoteKardexSimpleAsync(productId, branchOfficeId);
        }


    }
}
