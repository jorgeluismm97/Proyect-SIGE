using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IProductCommandText _productCommandText;
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuracion, IProductCommandText productCommandText)
        {
            _productCommandText = productCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(ProductModel productModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(productModel);
                dynamicParameters.Add("ProductId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _productCommandText.AddProduct,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    productModel.ProductId = dynamicParameters.Get<int>("ProductId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(ProductModel productModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(productModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _productCommandText.UpdateProduct,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                }
            }
            catch (Exception exception)
            {

            }

            return indicator;
        }

        public async Task<ProductModel> GetByIdAsync(int productId)
        {
            var rs = new ProductModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<ProductModel>(
                                        _productCommandText.GetProductById,
                                        new { ProductId = productId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            var ls = new List<ProductModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<ProductModel>(
                                        _productCommandText.GetAllProduct,
                                        commandType: CommandType.StoredProcedure
                                        );
                    ls.AddRange(rs);
                }
            }
            catch (Exception exception)
            {

            }

            return ls;
        }

        // Advanced

        public async Task<List<ProductModel>> GetByCompanyIdAsync( int companyId)
        {
            var ls = new List<ProductModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<ProductModel>(
                                        _productCommandText.GetProductByCompanyId,
                                        new { CompanyId = companyId },
                                        commandType: CommandType.StoredProcedure
                                        );
                    ls.AddRange(rs);
                }
            }
            catch (Exception exception)
            {

            }

            return ls;
        }

        public async Task<List<ProductModelView>> GetProductQuantityByCompanyIdAsync(int companyId)
        {
            var ls = new List<ProductModelView>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<ProductModelView>(
                                        _productCommandText.GetProductQuantityByCompanyId,
                                        new { CompanyId = companyId },
                                        commandType: CommandType.StoredProcedure
                                        );
                    ls.AddRange(rs);
                }
            }
            catch (Exception exception)
            {

            }

            return ls;
        }

        public async Task<List<ProductModel>> GetByDescriptionCompanyIdAsync(string description, int companyId)
        {
            var ls = new List<ProductModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<ProductModel>(
                                        _productCommandText.GetProductByDescriptionCompanyId,
                                        new { 
                                            Description = description,
                                            CompanyId = companyId 
                                        },
                                        commandType: CommandType.StoredProcedure
                                        );
                    ls.AddRange(rs);
                }
            }
            catch (Exception exception)
            {

            }

            return ls;
        }

        public async Task<ProductModel> GetByCodeAsync(string code)
        {
            var rs = new ProductModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<ProductModel>(
                                        _productCommandText.GetProductById,
                                        new { Code = code },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }
    }
}
