using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class VoidedDocumentsDetailService : IVoidedDocumentsDetailService
    {
        private readonly IVoidedDocumentsDetailRepository _voidedDocumentsDetailRepository;

        public VoidedDocumentsDetailService(IVoidedDocumentsDetailRepository voidedDocumentsDetailRepository)
        {
            _voidedDocumentsDetailRepository = voidedDocumentsDetailRepository;
        }

        //Basic

        public async Task<int> AddAsync(VoidedDocumentsDetailModel voidedDocumentsDetailModel)
        {
            return await _voidedDocumentsDetailRepository.AddAsync(voidedDocumentsDetailModel);
        }

        public async Task<int> UpdateAsync(VoidedDocumentsDetailModel voidedDocumentsDetailModel)
        {
            return await _voidedDocumentsDetailRepository.UpdateAsync(voidedDocumentsDetailModel);
        }

        public async Task<VoidedDocumentsDetailModel> GetByIdAsync(int voidedDocumentsDetailId)
        {
            return await _voidedDocumentsDetailRepository.GetByIdAsync(voidedDocumentsDetailId);
        }

        public async Task<List<VoidedDocumentsDetailModel>> GetAllAsync()
        {
            return await _voidedDocumentsDetailRepository.GetAllAsync();
        }
    }
}
