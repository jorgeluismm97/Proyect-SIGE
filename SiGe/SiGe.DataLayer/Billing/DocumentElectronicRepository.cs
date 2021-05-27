using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class DocumentElectronicRepository : IDocumentElectronicRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IBillingCommandText _billingCommandText;
        private readonly string _connectionString;

        public DocumentElectronicRepository(IConfiguration configuracion, IBillingCommandText billingCommandText)
        {
            _billingCommandText = billingCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(DocumentElectronicModel documentElectronicModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(documentElectronicModel);
                dynamicParameters.Add("DocumentElectronicId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.AddDocumentElectronic,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    documentElectronicModel.DocumentElectronicId = dynamicParameters.Get<int>("DocumentElectronicId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(DocumentElectronicModel documentElectronicModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(documentElectronicModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.UpdateDocumentElectronic,
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

        public async Task<DocumentElectronicModel> GetByIdAsync(int documentElectronicId)
        {
            var rs = new DocumentElectronicModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<DocumentElectronicModel>(
                                        _billingCommandText.GetDocumentElectronicById,
                                        new { DocumentElectronicId = documentElectronicId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<DocumentElectronicModel>> GetAllAsync()
        {
            var ls = new List<DocumentElectronicModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<DocumentElectronicModel>(
                                        _billingCommandText.GetAllDocumentElectronic,
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
