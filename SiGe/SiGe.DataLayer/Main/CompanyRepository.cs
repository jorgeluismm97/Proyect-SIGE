using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMainCommandText _mainCommandText;
        private readonly string _connectionString;

        public CompanyRepository(IConfiguration configuracion, IMainCommandText mainCommandText)
        {
            _mainCommandText = mainCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(CompanyModel companyModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(companyModel);
                dynamicParameters.Add("CompanyId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.AddCompany,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    companyModel.CompanyId = dynamicParameters.Get<int>("CompanyId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(CompanyModel companyModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(companyModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.UpdateCompany,
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

        public async Task<CompanyModel> GetByIdAsync(int companyId)
        {
            var rs = new CompanyModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<CompanyModel>(
                                        _mainCommandText.GetCompanyById,
                                        new { CompanyId = companyId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<CompanyModel>> GetAllAsync()
        {
            var ls = new List<CompanyModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<CompanyModel>(
                                        _mainCommandText.GetAllCompany,
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

        public async Task<CompanyModel> GetByIdentityDocumentNumberAsync(string identityDocumentNumber)
        {
            var rs = new CompanyModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<CompanyModel>(
                                        _mainCommandText.GetCompanyByIdentityDocumentNumber,
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

        public async Task<List<CompanyModel>> GetByUserIdAsync(int userId)
        {
            var ls = new List<CompanyModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<CompanyModel>(
                                        _mainCommandText.GetCompanyByUserId,
                                        new { UserId = userId },
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

        public async Task<CompanyModel> GetByUserNameIdentityDocumentNumberAsync(string userName, string identityDocumentNumber)
        {
            var rs = new CompanyModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<CompanyModel>(
                                        _mainCommandText.GetCompanyByUserNameIdentityDocumentNumber,
                                        new {
                                            UserName = userName,
                                            IdentityDocumentNumber = identityDocumentNumber 
                                        },
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
