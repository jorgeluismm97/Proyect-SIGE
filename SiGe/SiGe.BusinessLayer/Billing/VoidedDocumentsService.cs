using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class VoidedDocumentsService : IVoidedDocumentsService
    {
        private readonly IVoidedDocumentsRepository _voidedDocumentsRepository;

        public VoidedDocumentsService(IVoidedDocumentsRepository voidedDocumentsRepository)
        {
            _voidedDocumentsRepository = voidedDocumentsRepository;
        }

        //Basic

        public async Task<int> AddAsync(VoidedDocumentsModel voidedDocumentsModel)
        {
            return await _voidedDocumentsRepository.AddAsync(voidedDocumentsModel);
        }

        public async Task<int> UpdateAsync(VoidedDocumentsModel voidedDocumentsModel)
        {
            return await _voidedDocumentsRepository.UpdateAsync(voidedDocumentsModel);
        }

        public async Task<VoidedDocumentsModel> GetByIdAsync(int voidedDocumentsId)
        {
            return await _voidedDocumentsRepository.GetByIdAsync(voidedDocumentsId);
        }

        public async Task<List<VoidedDocumentsModel>> GetAllAsync()
        {
            return await _voidedDocumentsRepository.GetAllAsync();
        }
    }
}
