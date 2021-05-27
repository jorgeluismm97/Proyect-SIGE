using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IBillingCommandText _billingCommandText;
        private readonly string _connectionString;

        public DocumentTypeRepository(IConfiguration configuracion, IBillingCommandText billingCommandText)
        {
            _billingCommandText = billingCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(DocumentTypeModel documentTypeModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(documentTypeModel);
                dynamicParameters.Add("DocumentTypeId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.AddDocumentType,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    documentTypeModel.DocumentTypeId = dynamicParameters.Get<int>("DocumentTypeId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(DocumentTypeModel documentTypeModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(documentTypeModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.UpdateDocumentType,
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

        public async Task<DocumentTypeModel> GetByIdAsync(int documentTypeId)
        {
            var rs = new DocumentTypeModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<DocumentTypeModel>(
                                        _billingCommandText.GetDocumentTypeById,
                                        new { DocumentTypeId = documentTypeId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<DocumentTypeModel>> GetAllAsync()
        {
            var ls = new List<DocumentTypeModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<DocumentTypeModel>(
                                        _billingCommandText.GetAllDocumentType,
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
