using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class CompanyCredentialRepository : ICompanyCredentialRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMainCommandText _mainCommandText;
        private readonly string _connectionString;

        public CompanyCredentialRepository(IConfiguration configuracion, IMainCommandText mainCommandText)
        {
            _mainCommandText = mainCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(CompanyCredentialModel companyCredentialModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(companyCredentialModel);
                dynamicParameters.Add("CompanyCredentialId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.AddCompanyCredential,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    companyCredentialModel.CompanyCredentialId = dynamicParameters.Get<int>("CompanyCredentialId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(CompanyCredentialModel companyCredentialModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(companyCredentialModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.UpdateCompanyCredential,
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

        public async Task<CompanyCredentialModel> GetByIdAsync(int companyCredentialId)
        {
            var rs = new CompanyCredentialModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<CompanyCredentialModel>(
                                        _mainCommandText.GetCompanyCredentialById,
                                        new { CompanyCredentialId = companyCredentialId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<CompanyCredentialModel>> GetAllAsync()
        {
            var ls = new List<CompanyCredentialModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<CompanyCredentialModel>(
                                        _mainCommandText.GetAllCompanyCredential,
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
