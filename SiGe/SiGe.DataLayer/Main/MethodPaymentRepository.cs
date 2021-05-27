using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class MethodPaymentRepository : IMethodPaymentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMainCommandText _mainCommandText;
        private readonly string _connectionString;

        public MethodPaymentRepository(IConfiguration configuracion, IMainCommandText mainCommandText)
        {
            _mainCommandText = mainCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(MethodPaymentModel methodPaymentModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(methodPaymentModel);
                dynamicParameters.Add("MethodPaymentId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.AddMethodPayment,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    methodPaymentModel.MethodPaymentId = dynamicParameters.Get<int>("MethodPaymentId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(MethodPaymentModel methodPaymentModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(methodPaymentModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.UpdateMethodPayment,
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

        public async Task<MethodPaymentModel> GetByIdAsync(int methodPaymentId)
        {
            var rs = new MethodPaymentModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<MethodPaymentModel>(
                                        _mainCommandText.GetMethodPaymentById,
                                        new { MethodPaymentId = methodPaymentId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<MethodPaymentModel>> GetAllAsync()
        {
            var ls = new List<MethodPaymentModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<MethodPaymentModel>(
                                        _mainCommandText.GetAllMethodPayment,
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
