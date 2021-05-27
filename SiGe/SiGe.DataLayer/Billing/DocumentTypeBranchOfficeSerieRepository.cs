using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class DocumentTypeBranchOfficeSerieRepository : IDocumentTypeBranchOfficeSerieRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IBillingCommandText _billingCommandText;
        private readonly string _connectionString;

        public DocumentTypeBranchOfficeSerieRepository(IConfiguration configuracion, IBillingCommandText billingCommandText)
        {
            _billingCommandText = billingCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(DocumentTypeBranchOfficeSerieModel documentTypeBranchOfficeSerieModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(documentTypeBranchOfficeSerieModel);
                dynamicParameters.Add("DocumentTypeBranchOfficeSerieId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.AddDocumentTypeBranchOfficeSerie,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    documentTypeBranchOfficeSerieModel.DocumentTypeBranchOfficeSerieId = dynamicParameters.Get<int>("DocumentTypeBranchOfficeSerieId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(DocumentTypeBranchOfficeSerieModel documentTypeBranchOfficeSerieModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(documentTypeBranchOfficeSerieModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.UpdateDocumentTypeBranchOfficeSerie,
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

        public async Task<DocumentTypeBranchOfficeSerieModel> GetByIdAsync(int documentTypeBranchOfficeSerieId)
        {
            var rs = new DocumentTypeBranchOfficeSerieModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<DocumentTypeBranchOfficeSerieModel>(
                                        _billingCommandText.GetDocumentTypeBranchOfficeSerieById,
                                        new { DocumentTypeBranchOfficeSerieId = documentTypeBranchOfficeSerieId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<DocumentTypeBranchOfficeSerieModel>> GetAllAsync()
        {
            var ls = new List<DocumentTypeBranchOfficeSerieModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<DocumentTypeBranchOfficeSerieModel>(
                                        _billingCommandText.GetAllDocumentTypeBranchOfficeSerie,
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
