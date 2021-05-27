using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class VoidedDocumentsRepository : IVoidedDocumentsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IBillingCommandText _billingCommandText;
        private readonly string _connectionString;

        public VoidedDocumentsRepository(IConfiguration configuracion, IBillingCommandText billingCommandText)
        {
            _billingCommandText = billingCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(VoidedDocumentsModel voidedDocumentsModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(voidedDocumentsModel);
                dynamicParameters.Add("VoidedDocumentsId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.AddVoidedDocuments,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    voidedDocumentsModel.VoidedDocumentsId = dynamicParameters.Get<int>("VoidedDocumentsId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(VoidedDocumentsModel voidedDocumentsModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(voidedDocumentsModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.UpdateVoidedDocuments,
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

        public async Task<VoidedDocumentsModel> GetByIdAsync(int voidedDocumentsId)
        {
            var rs = new VoidedDocumentsModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<VoidedDocumentsModel>(
                                        _billingCommandText.GetVoidedDocumentsById,
                                        new { VoidedDocumentsId = voidedDocumentsId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<VoidedDocumentsModel>> GetAllAsync()
        {
            var ls = new List<VoidedDocumentsModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<VoidedDocumentsModel>(
                                        _billingCommandText.GetAllVoidedDocuments,
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
