using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class DocumentTypeSettingRepository : IDocumentTypeSettingRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IBillingCommandText _billingCommandText;
        private readonly string _connectionString;

        public DocumentTypeSettingRepository(IConfiguration configuracion, IBillingCommandText billingCommandText)
        {
            _billingCommandText = billingCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(DocumentTypeSettingModel documentTypeSettingModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(documentTypeSettingModel);
                dynamicParameters.Add("DocumentTypeSettingId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.AddDocumentTypeSetting,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    documentTypeSettingModel.DocumentTypeSettingId = dynamicParameters.Get<int>("DocumentTypeSettingId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(DocumentTypeSettingModel documentTypeSettingModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(documentTypeSettingModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.UpdateDocumentTypeSetting,
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

        public async Task<DocumentTypeSettingModel> GetByIdAsync(int documentTypeSettingId)
        {
            var rs = new DocumentTypeSettingModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<DocumentTypeSettingModel>(
                                        _billingCommandText.GetDocumentTypeSettingById,
                                        new { DocumentTypeSettingId = documentTypeSettingId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<DocumentTypeSettingModel>> GetAllAsync()
        {
            var ls = new List<DocumentTypeSettingModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<DocumentTypeSettingModel>(
                                        _billingCommandText.GetAllDocumentTypeSetting,
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
