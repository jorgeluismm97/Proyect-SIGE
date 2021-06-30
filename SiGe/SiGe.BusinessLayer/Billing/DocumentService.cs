using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SiGe
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        //Basic

        public async Task<int> AddAsync(DocumentModel documentModel)
        {
            return await _documentRepository.AddAsync(documentModel);
        }

        public async Task<int> UpdateAsync(DocumentModel documentModel)
        {
            return await _documentRepository.UpdateAsync(documentModel);
        }

        public async Task<DocumentModel> GetByIdAsync(int documentId)
        {
            return await _documentRepository.GetByIdAsync(documentId);
        }

        public async Task<List<DocumentModel>> GetAllAsync()
        {
            return await _documentRepository.GetAllAsync();
        }

        // Advanced

        public async Task<List<DocumentViewModel>> GetByCompanyIdAsync(int companyId)
        {
            return await _documentRepository.GetByCompanyIdAsync(companyId);
        }

        public async Task<int> GetNewNumberByDocumentTypeIdSerieIndicatorAsync(int companyId, int documentTypeId, string serie)
        {
            return await _documentRepository.GetNewNumberByDocumentTypeIdSerieIndicatorAsync(companyId, documentTypeId,serie);
        }
    }
}
