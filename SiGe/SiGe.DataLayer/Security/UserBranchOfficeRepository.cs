using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class UserBranchOfficeRepository : IUserBranchOfficeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ISecurityCommandText _securityCommandText;
        private readonly string _connectionString;

        public UserBranchOfficeRepository(IConfiguration configuracion, ISecurityCommandText securityCommandText)
        {
            _securityCommandText = securityCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(UserBranchOfficeModel userBranchOfficeModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(userBranchOfficeModel);
                dynamicParameters.Add("UserBranchOfficeId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _securityCommandText.AddUserBranchOffice,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    userBranchOfficeModel.UserBranchOfficeId = dynamicParameters.Get<int>("UserBranchOfficeId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(UserBranchOfficeModel userBranchOfficeModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(userBranchOfficeModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _securityCommandText.UpdateUserBranchOffice,
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

        public async Task<UserBranchOfficeModel> GetByIdAsync(int userId)
        {
            var rs = new UserBranchOfficeModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<UserBranchOfficeModel>(
                                        _securityCommandText.GetUserBranchOfficeById,
                                        new { UserBranchOfficeId = userId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<UserBranchOfficeModel>> GetAllAsync()
        {
            var ls = new List<UserBranchOfficeModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<UserBranchOfficeModel>(
                                        _securityCommandText.GetAllUserBranchOffice,
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
