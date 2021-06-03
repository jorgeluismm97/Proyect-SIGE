using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMainCommandText _mainCommandText;
        private readonly string _connectionString;

        public CustomerRepository(IConfiguration configuracion, IMainCommandText mainCommandText)
        {
            _mainCommandText = mainCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(CustomerModel customerProviderModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(customerProviderModel);
                dynamicParameters.Add("CustomerId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.AddCustomer,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    customerProviderModel.CustomerId = dynamicParameters.Get<int>("CustomerId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(CustomerModel customerProviderModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(customerProviderModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.UpdateCustomer,
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

        public async Task<CustomerModel> GetByIdAsync(int customerId)
        {
            var rs = new CustomerModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<CustomerModel>(
                                        _mainCommandText.GetCustomerById,
                                        new { CustomerId = customerId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<CustomerModel>> GetAllAsync()
        {
            var ls = new List<CustomerModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<CustomerModel>(
                                        _mainCommandText.GetAllCustomer,
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

        public async Task<CustomerModel> GetByIdentityDocumentNumberAsync(string identityDocumentNumber)
        {
            var rs = new CustomerModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<CustomerModel>(
                                        _mainCommandText.GetCustomerByIdentityDocumentNumber,
                                        new { IdentityDocumentNumber = identityDocumentNumber },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<CustomerModel>> GetByCompanyIdAsync(int companyId)
        {
            var ls = new List<CustomerModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<CustomerModel>(
                                        _mainCommandText.GetCustomerByCompanyId,
                                        new
                                        {
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
    }
}
