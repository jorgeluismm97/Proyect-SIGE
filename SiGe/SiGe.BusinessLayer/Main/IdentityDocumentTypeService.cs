using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class IdentityDocumentTypeService : IIdentityDocumentTypeService
    {
        private readonly IIdentityDocumentTypeRepository _identityDocumentTypeRepository;

        public IdentityDocumentTypeService(IIdentityDocumentTypeRepository IdentityDocumentTypeRepository)
        {
            _identityDocumentTypeRepository = IdentityDocumentTypeRepository;
        }

        //Basic

        public async Task<int> AddAsync(IdentityDocumentTypeModel identityDocumentTypeModel)
        {
            return await _identityDocumentTypeRepository.AddAsync(identityDocumentTypeModel);
        }

        public async Task<int> UpdateAsync(IdentityDocumentTypeModel identityDocumentTypeModel)
        {
            return await _identityDocumentTypeRepository.UpdateAsync(identityDocumentTypeModel);
        }

        public async Task<IdentityDocumentTypeModel> GetByIdAsync(int identityDocumentTypeId)
        {
            return await _identityDocumentTypeRepository.GetByIdAsync(identityDocumentTypeId);
        }

        public async Task<List<IdentityDocumentTypeModel>> GetAllAsync()
        {
            return await _identityDocumentTypeRepository.GetAllAsync();
        }
    }
}
