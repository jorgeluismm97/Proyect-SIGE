using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMainCommandText _mainCommandText;
        private readonly string _connectionString;

        public PersonRepository(IConfiguration configuracion, IMainCommandText mainCommandText)
        {
            _mainCommandText = mainCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(PersonModel personModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(personModel);
                dynamicParameters.Add("PersonId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.AddPerson,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    personModel.PersonId = dynamicParameters.Get<int>("PersonId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(PersonModel personModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(personModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _mainCommandText.UpdatePerson,
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

        public async Task<PersonModel> GetByIdAsync(int personId)
        {
            var rs = new PersonModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<PersonModel>(
                                        _mainCommandText.GetPersonById,
                                        new { PersonId = personId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<PersonModel>> GetAllAsync()
        {
            var ls = new List<PersonModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<PersonModel>(
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

        public async Task<PersonModel> GetByIdentityDocumentTypeIdIdentityDocumentNumberAsync(int identityDocumentTypeId, string identityDocumentNumber)
        {
            var rs = new PersonModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<PersonModel>(
                                        _mainCommandText.GetPersonByIdentityDocumentTypeIdIdentityDocumentNumber,
                                        new
                                        {
                                            IdentityDocumentTypeId = identityDocumentTypeId,
                                            IdentityDocumentNumber = identityDocumentNumber
                                        },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<PersonModel>> GetByCompanyIdAsync(int companyId)
        {
            var ls = new List<PersonModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<PersonModel>(
                                        _mainCommandText.GetPersonByCompanyId,
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

        public async Task<List<PersonModel>> GetWithOutUserByCompanyIdAsync(int companyId)
        {
            var ls = new List<PersonModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<PersonModel>(
                                        _mainCommandText.GetPersonWithOutUserByCompanyId,
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
