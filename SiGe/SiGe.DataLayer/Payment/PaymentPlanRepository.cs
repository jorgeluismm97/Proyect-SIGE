using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class PaymentPlanRepository : IPaymentPlanRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IPaymentCommandText _paymentCommandText;
        private readonly string _connectionString;

        public PaymentPlanRepository(IConfiguration configuracion, IPaymentCommandText paymentCommandText)
        {
            _paymentCommandText = paymentCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(PaymentPlanModel paymentPlanModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(paymentPlanModel);
                dynamicParameters.Add("PaymentPlanId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _paymentCommandText.AddPaymentPlan,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    paymentPlanModel.PaymentPlanId = dynamicParameters.Get<int>("PaymentPlanId");
                }
            }
            catch (Exception exception)
            {

            }

            return indicator;
        }

        public async Task<int> UpdateAsync(PaymentPlanModel paymentPlanModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(paymentPlanModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _paymentCommandText.UpdatePaymentPlan,
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

        public async Task<PaymentPlanModel> GetByIdAsync(int paymentPlanId)
        {
            var rs = new PaymentPlanModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<PaymentPlanModel>(
                                        _paymentCommandText.GetPaymentPlanById,
                                        new { PaymentPlanId = paymentPlanId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<PaymentPlanModel>> GetAllAsync()
        {
            var ls = new List<PaymentPlanModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<PaymentPlanModel>(
                                        _paymentCommandText.GetAllPaymentPlan,
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