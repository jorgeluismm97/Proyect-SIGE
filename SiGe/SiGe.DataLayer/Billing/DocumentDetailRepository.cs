using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class DocumentDetailRepository : IDocumentDetailRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IBillingCommandText _billingCommandText;
        private readonly string _connectionString;

        public DocumentDetailRepository(IConfiguration configuracion, IBillingCommandText billingCommandText)
        {
            _billingCommandText = billingCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(DocumentDetailModel documentDetailModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(documentDetailModel);
                dynamicParameters.Add("DocumentDetailId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.AddDocumentDetail,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    documentDetailModel.DocumentDetailId = dynamicParameters.Get<int>("DocumentDetailId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(DocumentDetailModel documentDetailModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(documentDetailModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.UpdateDocumentDetail,
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

        public async Task<DocumentDetailModel> GetByIdAsync(int documentDetailId)
        {
            var rs = new DocumentDetailModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<DocumentDetailModel>(
                                        _billingCommandText.GetDocumentDetailById,
                                        new { DocumentDetailId = documentDetailId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<DocumentDetailModel>> GetAllAsync()
        {
            var ls = new List<DocumentDetailModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<DocumentDetailModel>(
                                        _billingCommandText.GetAllDocumentDetail,
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
        public async Task<List<NoteProductModelDetail>> GetDocumentDetailProductByDocumentIdAsync(int documentId)
        {
            var ls = new List<NoteProductModelDetail>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<NoteProductModelDetail>(
                                        _billingCommandText.GetDocumentDetailProductByDocumentId,
                                        new { DocumentId = documentId },
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

        public async Task<List<DocumentDetailModel>> GetByDocumentIdAsync(int documentId)
        {
            var ls = new List<DocumentDetailModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<DocumentDetailModel>(
                                        _billingCommandText.GetDocumentDetailByDocumentId,
                                        new { DocumentId = documentId },
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
