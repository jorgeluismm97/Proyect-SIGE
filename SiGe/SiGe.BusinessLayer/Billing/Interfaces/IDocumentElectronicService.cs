using System.Threading.Tasks;

namespace SiGe
{
    public interface IDocumentElectronicService : IService<DocumentElectronicModel>
    {
        Task<DocumentElectronicModel> GetByDocumentIdAsync(int documentId);
    }
}
