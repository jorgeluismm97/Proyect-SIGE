using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class CustomerProviderRepository : ICustomerProviderRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMainCommandText _mainCommandText;
        private readonly string _connectionString;

        public CustomerProviderRepository(IConfiguration configuracion, IMainCommandText mainCommandText)
        {
            _mainCommandText = mainCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(CustomerProviderModel customerProviderModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(customerProviderModel);
                dynamicParameters.Add("CustomerProviderId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.AddCustomerProvider,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    customerProviderModel.CustomerProviderId = dynamicParameters.Get<int>("CustomerProviderId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(CustomerProviderModel customerProviderModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(customerProviderModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.UpdateCustomerProvider,
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

        public async Task<CustomerProviderModel> GetByIdAsync(int customerProviderId)
        {
            var rs = new CustomerProviderModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<CustomerProviderModel>(
                                        _mainCommandText.GetCustomerProviderById,
                                        new { CustomerProviderId = customerProviderId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<CustomerProviderModel>> GetAllAsync()
        {
            var ls = new List<CustomerProviderModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<CustomerProviderModel>(
                                        _mainCommandText.GetAllCustomerProvider,
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
    }
}
