using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class DocumentTypeSettingService : IDocumentTypeSettingService
    {
        private readonly IDocumentTypeSettingRepository _documentTypeSettingRepository;

        public DocumentTypeSettingService(IDocumentTypeSettingRepository documentTypeSettingRepository)
        {
            _documentTypeSettingRepository = documentTypeSettingRepository;
        }

        //Basic

        public async Task<int> AddAsync(DocumentTypeSettingModel documentTypeSettingModel)
        {
            return await _documentTypeSettingRepository.AddAsync(documentTypeSettingModel);
        }

        public async Task<int> UpdateAsync(DocumentTypeSettingModel documentTypeSettingModel)
        {
            return await _documentTypeSettingRepository.UpdateAsync(documentTypeSettingModel);
        }

        public async Task<DocumentTypeSettingModel> GetByIdAsync(int documentTypeSettingId)
        {
            return await _documentTypeSettingRepository.GetByIdAsync(documentTypeSettingId);
        }

        public async Task<List<DocumentTypeSettingModel>> GetAllAsync()
        {
            return await _documentTypeSettingRepository.GetAllAsync();
        }
    }
}
