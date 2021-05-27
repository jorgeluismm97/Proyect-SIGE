using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class SessionRepository : ISessionRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ISecurityCommandText _securityCommandText;
        private readonly string _connectionString;

        public SessionRepository(IConfiguration configuracion, ISecurityCommandText securityCommandText)
        {
            _securityCommandText = securityCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(SessionModel sessionModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(sessionModel);
                dynamicParameters.Add("SessionId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _securityCommandText.AddSession,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    sessionModel.SessionId = dynamicParameters.Get<int>("SessionId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(SessionModel sessionModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(sessionModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _securityCommandText.UpdateSession,
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

        public async Task<SessionModel> GetByIdAsync(int sessionId)
        {
            var rs = new SessionModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<SessionModel>(
                                        _securityCommandText.GetSessionById,
                                        new { SessionId = sessionId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<SessionModel>> GetAllAsync()
        {
            var ls = new List<SessionModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<SessionModel>(
                                        _securityCommandText.GetAllSession,
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
