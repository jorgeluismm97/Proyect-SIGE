using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IProductService : IService<ProductModel>
    {
        Task<List<ProductModel>> GetByCompanyIdAsync(int companyId);
        Task<ProductModel> GetByCodeAsync(string code);
    }
}
