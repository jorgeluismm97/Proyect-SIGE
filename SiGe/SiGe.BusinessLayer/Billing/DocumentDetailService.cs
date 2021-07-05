using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class DocumentDetailService : IDocumentDetailService
    {
        private readonly IDocumentDetailRepository _documentDetailRepository;

        public DocumentDetailService(IDocumentDetailRepository documentDetailRepository)
        {
            _documentDetailRepository = documentDetailRepository;
        }

        //Basic

        public async Task<int> AddAsync(DocumentDetailModel documentDetailModel)
        {
            return await _documentDetailRepository.AddAsync(documentDetailModel);
        }

        public async Task<int> UpdateAsync(DocumentDetailModel documentDetailModel)
        {
            return await _documentDetailRepository.UpdateAsync(documentDetailModel);
        }

        public async Task<DocumentDetailModel> GetByIdAsync(int documentDetailId)
        {
            return await _documentDetailRepository.GetByIdAsync(documentDetailId);
        }

        public async Task<List<DocumentDetailModel>> GetAllAsync()
        {
            return await _documentDetailRepository.GetAllAsync();
        }

        // Advanced
        public async Task<List<NoteProductModelDetail>> GetDocumentDetailProductByDocumentIdAsync(int documentId)
        {
            return await _documentDetailRepository.GetDocumentDetailProductByDocumentIdAsync(documentId);
        }

        public async Task<List<DocumentDetailModel>> GetByDocumentIdAsync(int documentId)
        {
            return await _documentDetailRepository.GetByDocumentIdAsync(documentId);
        }
    }
}
