using System.Threading.Tasks;

namespace SiGe
{
    public interface IDocumentElectronicRepository : IRepository<DocumentElectronicModel>
    {
        Task<DocumentElectronicModel> GetByDocumentIdAsync(int documentId);
    }
}
