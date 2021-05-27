using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class VoidedDocumentsDetailRepository: IVoidedDocumentsDetailRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IBillingCommandText _billingCommandText;
        private readonly string _connectionString;

        public VoidedDocumentsDetailRepository(IConfiguration configuracion, IBillingCommandText billingCommandText)
        {
            _billingCommandText = billingCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(VoidedDocumentsDetailModel voidedDocumentsDetailModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(voidedDocumentsDetailModel);
                dynamicParameters.Add("VoidedDocumentsDetailId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.AddVoidedDocumentsDetail,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    voidedDocumentsDetailModel.VoidedDocumentsDetailId = dynamicParameters.Get<int>("VoidedDocumentsDetailId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(VoidedDocumentsDetailModel voidedDocumentsDetailModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(voidedDocumentsDetailModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _billingCommandText.UpdateVoidedDocumentsDetail,
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

        public async Task<VoidedDocumentsDetailModel> GetByIdAsync(int voidedDocumentsDetailId)
        {
            var rs = new VoidedDocumentsDetailModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<VoidedDocumentsDetailModel>(
                                        _billingCommandText.GetVoidedDocumentsDetailById,
                                        new { VoidedDocumentsDetailId = voidedDocumentsDetailId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<VoidedDocumentsDetailModel>> GetAllAsync()
        {
            var ls = new List<VoidedDocumentsDetailModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<VoidedDocumentsDetailModel>(
                                        _billingCommandText.GetAllVoidedDocumentsDetail,
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
