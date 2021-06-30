using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class DocumentElectronicService : IDocumentElectronicService
    {
        private readonly IDocumentElectronicRepository _documentElectronicRepository;

        public DocumentElectronicService(IDocumentElectronicRepository documentElectronicRepository)
        {
            _documentElectronicRepository = documentElectronicRepository;
        }

        //Basic

        public async Task<int> AddAsync(DocumentElectronicModel documentElectronicModel)
        {
            return await _documentElectronicRepository.AddAsync(documentElectronicModel);
        }

        public async Task<int> UpdateAsync(DocumentElectronicModel documentElectronicModel)
        {
            return await _documentElectronicRepository.UpdateAsync(documentElectronicModel);
        }

        public async Task<DocumentElectronicModel> GetByIdAsync(int documentElectronicId)
        {
            return await _documentElectronicRepository.GetByIdAsync(documentElectronicId);
        }

        public async Task<List<DocumentElectronicModel>> GetAllAsync()
        {
            return await _documentElectronicRepository.GetAllAsync();
        }

        // Advanced

        public async Task<DocumentElectronicModel> GetByDocumentIdAsync(int documentId)
        {
            return await _documentElectronicRepository.GetByDocumentIdAsync(documentId);
        }
    }
}
