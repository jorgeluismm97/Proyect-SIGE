using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class BranchOfficeRepository : IBranchOfficeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMainCommandText _mainCommandText;
        private readonly string _connectionString;

        public BranchOfficeRepository(IConfiguration configuracion, IMainCommandText mainCommandText)
        {
            _mainCommandText = mainCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(BranchOfficeModel branchOfficeModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(branchOfficeModel);
                dynamicParameters.Add("BranchOfficeId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.AddBranchOffice,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    branchOfficeModel.BranchOfficeId = dynamicParameters.Get<int>("BranchOfficeId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(BranchOfficeModel branchOfficeModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(branchOfficeModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.UpdateBranchOffice,
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

        public async Task<BranchOfficeModel> GetByIdAsync(int branchOfficeId)
        {
            var rs = new BranchOfficeModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<BranchOfficeModel>(
                                        _mainCommandText.GetBranchOfficeById,
                                        new { BranchOfficeId = branchOfficeId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<BranchOfficeModel>> GetAllAsync()
        {
            var ls = new List<BranchOfficeModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<BranchOfficeModel>(
                                        _mainCommandText.GetAllPerson,
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

        public async Task<List<BranchOfficeModel>> GetByCompanyId(int companyId)
        {
            var ls = new List<BranchOfficeModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<BranchOfficeModel>(
                                        _mainCommandText.GetBranchOfficeByCompanyId,
                                        new{
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

        public async Task<List<BranchOfficeModel>> GetByCode(string code)
        {
            var ls = new List<BranchOfficeModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<BranchOfficeModel>(
                                        _mainCommandText.GetBranchOfficeByCode,
                                        new
                                        {
                                            Code = code
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
