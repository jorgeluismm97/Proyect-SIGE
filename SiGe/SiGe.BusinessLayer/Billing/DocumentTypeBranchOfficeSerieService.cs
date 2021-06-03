using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class DocumentTypeBranchOfficeSerieService : IDocumentTypeBranchOfficeSerieService
    {
        private readonly IDocumentTypeBranchOfficeSerieRepository _documentTypeBranchOfficeSerieRepository;

        public DocumentTypeBranchOfficeSerieService(IDocumentTypeBranchOfficeSerieRepository documentTypeBranchOfficeSerieRepository)
        {
            _documentTypeBranchOfficeSerieRepository = documentTypeBranchOfficeSerieRepository;
        }

        //Basic

        public async Task<int> AddAsync(DocumentTypeBranchOfficeSerieModel documentTypeBranchOfficeSerieModel)
        {
            return await _documentTypeBranchOfficeSerieRepository.AddAsync(documentTypeBranchOfficeSerieModel);
        }

        public async Task<int> UpdateAsync(DocumentTypeBranchOfficeSerieModel documentTypeBranchOfficeSerieModel)
        {
            return await _documentTypeBranchOfficeSerieRepository.UpdateAsync(documentTypeBranchOfficeSerieModel);
        }

        public async Task<DocumentTypeBranchOfficeSerieModel> GetByIdAsync(int documentTypeBranchOfficeSerieId)
        {
            return await _documentTypeBranchOfficeSerieRepository.GetByIdAsync(documentTypeBranchOfficeSerieId);
        }

        public async Task<List<DocumentTypeBranchOfficeSerieModel>> GetAllAsync()
        {
            return await _documentTypeBranchOfficeSerieRepository.GetAllAsync();
        }

        // Advanced
        public async Task<List<SerieViewModel>> GetByCompanyIdAsync(int companyId)
        {
            return await _documentTypeBranchOfficeSerieRepository.GetByCompanyIdAsync(companyId);
        }

    }
}
