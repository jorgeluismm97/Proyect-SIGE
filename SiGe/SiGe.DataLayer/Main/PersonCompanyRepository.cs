using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class PersonCompanyRepository : IPersonCompanyRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMainCommandText _mainCommandText;
        private readonly string _connectionString;

        public PersonCompanyRepository(IConfiguration configuracion, IMainCommandText mainCommandText)
        {
            _mainCommandText = mainCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(PersonCompanyModel personModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(personModel);
                dynamicParameters.Add("PersonCompanyId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.AddPersonCompany,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    personModel.PersonCompanyId = dynamicParameters.Get<int>("PersonCompanyId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(PersonCompanyModel personModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(personModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.UpdatePersonCompany,
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

        public async Task<PersonCompanyModel> GetByIdAsync(int personCompanyId)
        {
            var rs = new PersonCompanyModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<PersonCompanyModel>(
                                        _mainCommandText.GetPersonById,
                                        new { PersonCompanyId = personCompanyId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<PersonCompanyModel>> GetAllAsync()
        {
            var ls = new List<PersonCompanyModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<PersonCompanyModel>(
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
    }
}
