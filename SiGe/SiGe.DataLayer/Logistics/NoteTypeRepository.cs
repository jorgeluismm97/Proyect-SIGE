using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class NoteTypeRepository : INoteTypeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogisticsCommandText _logisticsCommandText;
        private readonly string _connectionString;

        public NoteTypeRepository(IConfiguration configuracion, ILogisticsCommandText logisticsCommandText)
        {
            _logisticsCommandText = logisticsCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(NoteTypeModel noteTypeModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(noteTypeModel);
                dynamicParameters.Add("NoteTypeId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _logisticsCommandText.AddNoteType,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    noteTypeModel.NoteTypeId = dynamicParameters.Get<int>("NoteTypeId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(NoteTypeModel noteTypeModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(noteTypeModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _logisticsCommandText.UpdateNoteType,
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

        public async Task<NoteTypeModel> GetByIdAsync(int noteTypeId)
        {
            var rs = new NoteTypeModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<NoteTypeModel>(
                                        _logisticsCommandText.GetNoteTypeById,
                                        new { NoteTypeId = noteTypeId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<NoteTypeModel>> GetAllAsync()
        {
            var ls = new List<NoteTypeModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<NoteTypeModel>(
                                        _logisticsCommandText.GetAllNoteType,
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
