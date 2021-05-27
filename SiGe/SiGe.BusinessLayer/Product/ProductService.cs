using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        //Basic

        public async Task<int> AddAsync(ProductModel productModel)
        {
            return await _productRepository.AddAsync(productModel);
        }

        public async Task<int> UpdateAsync(ProductModel productModel)
        {
            return await _productRepository.UpdateAsync(productModel);
        }

        public async Task<ProductModel> GetByIdAsync(int productId)
        {
            return await _productRepository.GetByIdAsync(productId);
        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        // Advanced

        public async Task<List<ProductModel>> GetByCompanyIdAsync(int companyId)
        {
            return await _productRepository.GetByCompanyIdAsync(companyId);
        }

        public async Task<ProductModel> GetByCodeAsync(string code)
        {
            return await _productRepository.GetByCodeAsync(code);
        }
    }
}
