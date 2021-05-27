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

    }
}
