using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;

        public DocumentTypeService(IDocumentTypeRepository documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;
        }

        //Basic

        public async Task<int> AddAsync(DocumentTypeModel documentTypeModel)
        {
            return await _documentTypeRepository.AddAsync(documentTypeModel);
        }

        public async Task<int> UpdateAsync(DocumentTypeModel documentTypeModel)
        {
            return await _documentTypeRepository.UpdateAsync(documentTypeModel);
        }

        public async Task<DocumentTypeModel> GetByIdAsync(int documentTypeId)
        {
            return await _documentTypeRepository.GetByIdAsync(documentTypeId);
        }

        public async Task<List<DocumentTypeModel>> GetAllAsync()
        {
            return await _documentTypeRepository.GetAllAsync();
        }
    }
}
