using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;


namespace SiGe
{
    public class UserCompanyRepository : IUserCompanyRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMainCommandText _mainCommandText;
        private readonly string _connectionString;

        public UserCompanyRepository(IConfiguration configuracion, IMainCommandText mainCommandText)
        {
            _mainCommandText = mainCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(UserCompanyModel userCompanyModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(userCompanyModel);
                dynamicParameters.Add("UserCompanyId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.AddUserCompany,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    userCompanyModel.UserCompanyId = dynamicParameters.Get<int>("UserCompanyId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(UserCompanyModel userCompanyModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(userCompanyModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.UpdateUserCompany,
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

        public async Task<UserCompanyModel> GetByIdAsync(int userCompanyId)
        {
            var rs = new UserCompanyModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<UserCompanyModel>(
                                        _mainCommandText.GetUserCompanyById,
                                        new { UserCompanyId = userCompanyId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<UserCompanyModel>> GetAllAsync()
        {
            var ls = new List<UserCompanyModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<UserCompanyModel>(
                                        _mainCommandText.GetAllUserCompany,
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
