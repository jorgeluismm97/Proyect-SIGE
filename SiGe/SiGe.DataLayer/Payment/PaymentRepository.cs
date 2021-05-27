using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;


namespace SiGe
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IPaymentCommandText _paymentCommandText;
        private readonly string _connectionString;

        public PaymentRepository(IConfiguration configuracion, IPaymentCommandText paymentCommandText)
        {
            _paymentCommandText = paymentCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(PaymentModel paymentModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(paymentModel);
                dynamicParameters.Add("PaymentId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _paymentCommandText.AddPayment,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    paymentModel.PaymentId = dynamicParameters.Get<int>("PaymentId");
                }
            }
            catch (Exception exception)
            {

            }

            return indicator;
        }

        public async Task<int> UpdateAsync(PaymentModel paymentModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(paymentModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _paymentCommandText.UpdatePayment,
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

        public async Task<PaymentModel> GetByIdAsync(int paymentId)
        {
            var rs = new PaymentModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<PaymentModel>(
                                        _paymentCommandText.GetPaymentById,
                                        new { PaymentId = paymentId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<PaymentModel>> GetAllAsync()
        {
            var ls = new List<PaymentModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<PaymentModel>(
                                        _paymentCommandText.GetAllPayment,
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

        public async Task<List<PaymentModel>> GetByCompanyIdAsync(int companyId)
        {
            var ls = new List<PaymentModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<PaymentModel>(
                                        _paymentCommandText.GetPaymentByCompanyId,
                                        new { CompanyId = companyId },
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

        public async Task<List<PaymentModel>> GetByCompanyIdDateAsync(int companyId, DateTime date)
        {
            var ls = new List<PaymentModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<PaymentModel>(
                                        _paymentCommandText.GetPaymentByCompanyIdDate,
                                        new { 
                                            CompanyId = companyId ,
                                            Date = date
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
    }
}
