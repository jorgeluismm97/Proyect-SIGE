using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IBillingCommandText _billingCommandText;
        private readonly string _connectionString;

        public DocumentRepository(IConfiguration configuracion, IBillingCommandText billingCommandText)
        {
            _billingCommandText = billingCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(DocumentModel documentModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(documentModel);
                dynamicParameters.Add("DocumentId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.AddDocument,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    documentModel.DocumentId = dynamicParameters.Get<int>("DocumentId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(DocumentModel documentModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(documentModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.UpdateDocument,
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

        public async Task<DocumentModel> GetByIdAsync(int documentId)
        {
            var rs = new DocumentModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<DocumentModel>(
                                        _billingCommandText.GetDocumentById,
                                        new { DocumentId = documentId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<DocumentModel>> GetAllAsync()
        {
            var ls = new List<DocumentModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<DocumentModel>(
                                        _billingCommandText.GetAllDocument,
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

        public async Task<List<DocumentViewModel>> GetByCompanyIdAsync(int companyId)
        {
            var ls = new List<DocumentViewModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<DocumentViewModel>(
                                        _billingCommandText.GetDocumentByCompanyId,
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

        public async Task<List<DocumentViewModel>> GetByCompanyIdDateAsync(int companyId, DateTime startingDate, DateTime endingDate)
        {
            var ls = new List<DocumentViewModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<DocumentViewModel>(
                                        _billingCommandText.GetDocumentByCompanyIdDate,
                                        new
                                        {
                                            CompanyId = companyId,
                                            StartingDate = startingDate,
                                            EndingDate = endingDate
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

        public async Task<List<MethodPaymentViewModel>> GetMethodPaymentByCompanyIdDateAsync(int companyId, DateTime startingDate, DateTime endingDate)
        {
            var ls = new List<MethodPaymentViewModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<MethodPaymentViewModel>(
                                        _billingCommandText.GetMethodPaymentByCompanyIdDate,
                                        new
                                        {
                                            CompanyId = companyId,
                                            StartingDate = startingDate,
                                            EndingDate = endingDate
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

        public async Task<int> GetNewNumberByDocumentTypeIdSerieIndicatorAsync(int companyId, int documentTypeId, string serie)
        {
            int number = 0;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    number = await connection.ExecuteScalarAsync<int>(
                                            _billingCommandText.GetDocumentNewNumber,
                                            new
                                            {
                                                CompanyId = companyId,
                                                DocumentTypeId = documentTypeId,
                                                Serie = serie
                                            },
                                            commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception exception)
            {
                
            }
            return number;
        }

    }
}
