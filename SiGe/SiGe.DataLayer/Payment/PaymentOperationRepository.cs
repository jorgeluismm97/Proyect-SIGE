using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class PaymentOperationRepository : IPaymentOperationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IPaymentCommandText _paymentCommandText;
        private readonly string _connectionString;

        public PaymentOperationRepository(IConfiguration configuracion, IPaymentCommandText paymentCommandText)
        {
            _paymentCommandText = paymentCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(PaymentOperationModel paymentOperationModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(paymentOperationModel);
                dynamicParameters.Add("PaymentOperationId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _paymentCommandText.AddPaymentOperation,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    paymentOperationModel.PaymentOperationId = dynamicParameters.Get<int>("PaymentOperationId");
                }
            }
            catch (Exception exception)
            {

            }

            return indicator;
        }

        public async Task<int> UpdateAsync(PaymentOperationModel paymentOperationModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(paymentOperationModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _paymentCommandText.UpdatePaymentOperation,
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

        public async Task<PaymentOperationModel> GetByIdAsync(int paymentOperationId)
        {
            var rs = new PaymentOperationModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<PaymentOperationModel>(
                                        _paymentCommandText.GetPaymentOperationById,
                                        new { PaymentOperationId = paymentOperationId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<PaymentOperationModel>> GetAllAsync()
        {
            var ls = new List<PaymentOperationModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<PaymentOperationModel>(
                                        _paymentCommandText.GetAllPaymentOperation,
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
