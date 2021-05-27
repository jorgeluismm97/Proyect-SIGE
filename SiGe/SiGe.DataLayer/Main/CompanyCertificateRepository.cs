using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class CompanyCertificateRepository : ICompanyCertificateRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMainCommandText _mainCommandText;
        private readonly string _connectionString;

        public CompanyCertificateRepository(IConfiguration configuracion, IMainCommandText mainCommandText)
        {
            _mainCommandText = mainCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(CompanyCertificateModel companyCertificateModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(companyCertificateModel);
                dynamicParameters.Add("CompanyCertificateId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.AddCompanyCertificate,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    companyCertificateModel.CompanyCertificateId = dynamicParameters.Get<int>("CompanyCertificateId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(CompanyCertificateModel companyCertificateModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(companyCertificateModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.UpdateCompanyCertificate,
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

        public async Task<CompanyCertificateModel> GetByIdAsync(int companyCertificateId)
        {
            var rs = new CompanyCertificateModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<CompanyCertificateModel>(
                                        _mainCommandText.GetCompanyCertificateById,
                                        new { CompanyCertificateId = companyCertificateId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<CompanyCertificateModel>> GetAllAsync()
        {
            var ls = new List<CompanyCertificateModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<CompanyCertificateModel>(
                                        _mainCommandText.GetAllCompanyCertificate,
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
