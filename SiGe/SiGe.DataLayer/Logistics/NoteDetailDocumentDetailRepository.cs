using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class NoteDetailDocumentDetailRepository : INoteDetailDocumentDetailRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogisticsCommandText _logisticsCommandText;
        private readonly string _connectionString;

        public NoteDetailDocumentDetailRepository(IConfiguration configuracion, ILogisticsCommandText logisticsCommandText)
        {
            _logisticsCommandText = logisticsCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(NoteDetailDocumentDetailModel noteDetailDocumentDetailModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(noteDetailDocumentDetailModel);
                dynamicParameters.Add("NoteDetailDocumentDetailId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _logisticsCommandText.AddNoteDetailDocumentDetail,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    noteDetailDocumentDetailModel.NoteDetailDocumentDetailId = dynamicParameters.Get<int>("NoteDetailDocumentDetailId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(NoteDetailDocumentDetailModel noteDetailDocumentDetailModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(noteDetailDocumentDetailModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _logisticsCommandText.UpdateNoteDetailDocumentDetail,
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

        public async Task<NoteDetailDocumentDetailModel> GetByIdAsync(int noteDetailDocumentDetailId)
        {
            var rs = new NoteDetailDocumentDetailModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<NoteDetailDocumentDetailModel>(
                                        _logisticsCommandText.GetNoteDetailDocumentDetailById,
                                        new { NoteDetailDocumentDetailId = noteDetailDocumentDetailId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<NoteDetailDocumentDetailModel>> GetAllAsync()
        {
            var ls = new List<NoteDetailDocumentDetailModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<NoteDetailDocumentDetailModel>(
                                        _logisticsCommandText.GetAllNoteDetailDocumentDetail,
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
