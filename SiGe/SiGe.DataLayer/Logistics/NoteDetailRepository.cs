using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class NoteDetailRepository :INoteDetailRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogisticsCommandText _logisticsCommandText;
        private readonly string _connectionString;

        public NoteDetailRepository(IConfiguration configuracion, ILogisticsCommandText logisticsCommandText)
        {
            _logisticsCommandText = logisticsCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(NoteDetailModel noteDetailModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(noteDetailModel);
                dynamicParameters.Add("NoteDetailId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _logisticsCommandText.AddNoteDetail,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    noteDetailModel.NoteDetailId = dynamicParameters.Get<int>("NoteDetailId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(NoteDetailModel noteDetailModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(noteDetailModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _logisticsCommandText.UpdateNoteDetail,
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

        public async Task<NoteDetailModel> GetByIdAsync(int noteDetailId)
        {
            var rs = new NoteDetailModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<NoteDetailModel>(
                                        _logisticsCommandText.GetNoteDetailById,
                                        new { NoteDetailId = noteDetailId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<NoteDetailModel>> GetAllAsync()
        {
            var ls = new List<NoteDetailModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<NoteDetailModel>(
                                        _logisticsCommandText.GetAllNoteDetail,
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
