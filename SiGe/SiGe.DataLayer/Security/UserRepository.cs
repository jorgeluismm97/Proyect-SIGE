using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ISecurityCommandText _securityCommandText;
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuracion, ISecurityCommandText securityCommandText)
        {
            _securityCommandText = securityCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(UserModel userModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(userModel);
                dynamicParameters.Add("UserId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _securityCommandText.AddUser,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    userModel.UserId = dynamicParameters.Get<int>("UserId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(UserModel userModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(userModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _securityCommandText.UpdateUser,
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

        public async Task<UserModel> GetByIdAsync(int userId)
        {
            var rs = new UserModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<UserModel>(
                                        _securityCommandText.GetUserById,
                                        new { UserId = userId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            var ls = new List<UserModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<UserModel>(
                                        _securityCommandText.GetAllUser,
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
        public async Task<UserModel> ValidateAsync(string userName, string password)
        {
            var rs = new UserModel();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<UserModel>(
                                            _securityCommandText.ValidateUser,
                                            new
                                            {
                                                UserName = userName,
                                                Password = UtilitarianUTL.EncriptarCadena(password)
                                            },
                                            commandType: CommandType.StoredProcedure
                                            );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rs;
        }

        public async Task<List<UserViewModel>> GetByCompanyId(int companyId)
        {
            var ls = new List<UserViewModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<UserViewModel>(
                                        _securityCommandText.GetUserByCompanyId,
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

    }
}
