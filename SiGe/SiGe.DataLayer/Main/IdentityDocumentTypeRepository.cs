using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;


namespace SiGe
{
    public class IdentityDocumentTypeRepository : IIdentityDocumentTypeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMainCommandText _mainCommandText;
        private readonly string _connectionString;

        public IdentityDocumentTypeRepository(IConfiguration configuracion, IMainCommandText mainCommandText)
        {
            _mainCommandText = mainCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(IdentityDocumentTypeModel identityDocumentTypeModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(identityDocumentTypeModel);
                dynamicParameters.Add("IdentityDocumentTypeId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.AddIdentityDocumentType,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    identityDocumentTypeModel.IdentityDocumentTypeId = dynamicParameters.Get<int>("IdentityDocumentTypeId");
                }
            }
            catch (Exception exception)
            {

            }

            return indicator;
        }

        public async Task<int> UpdateAsync(IdentityDocumentTypeModel identityDocumentTypeModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(identityDocumentTypeModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.UpdateIdentityDocumentType,
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


        public async Task<IdentityDocumentTypeModel> GetByIdAsync(int identityDocumentTypeId)
        {
            var rs = new IdentityDocumentTypeModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<IdentityDocumentTypeModel>(
                                        _mainCommandText.GetIdentityDocumentTypeById,
                                        new { IdentityDocumentTypeId = identityDocumentTypeId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<IdentityDocumentTypeModel>> GetAllAsync()
        {
            var ls = new List<IdentityDocumentTypeModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<IdentityDocumentTypeModel>(
                                        _mainCommandText.GetAllIdentityDocumentType,
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
