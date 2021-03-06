using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Dapper;

namespace SiGe
{
    public class NoteRepository : INoteRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogisticsCommandText _logisticsCommandText;
        private readonly string _connectionString;

        public NoteRepository(IConfiguration configuracion, ILogisticsCommandText logisticsCommandText)
        {
            _logisticsCommandText = logisticsCommandText;
            _configuration = configuracion;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Basic

        public async Task<int> AddAsync(NoteModel noteModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(noteModel);
                dynamicParameters.Add("NoteId", DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _logisticsCommandText.AddNote,
                                            dynamicParameters,
                                            commandType: CommandType.StoredProcedure
                                            );
                    noteModel.NoteId = dynamicParameters.Get<int>("NoteId");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return indicator;
        }

        public async Task<int> UpdateAsync(NoteModel noteModel)
        {
            int indicator = -1;

            try
            {
                var dynamicParameters = new DynamicParameters(noteModel);

                using (var connection = new SqlConnection(_connectionString))
                {
                    indicator = await connection.ExecuteAsync(
                                            _logisticsCommandText.UpdateNote,
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

        public async Task<NoteModel> GetByIdAsync(int noteId)
        {
            var rs = new NoteModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    rs = await connection.QueryFirstOrDefaultAsync<NoteModel>(
                                        _logisticsCommandText.GetNoteById,
                                        new { NoteId = noteId },
                                        commandType: CommandType.StoredProcedure
                                        );
                }
            }
            catch (Exception exception)
            {

            }

            return rs;
        }

        public async Task<List<NoteModel>> GetAllAsync()
        {
            var ls = new List<NoteModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<NoteModel>(
                                        _logisticsCommandText.GetAllNote,
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

        public async Task<List<NoteViewModel>> GetByCompanyIdActionTypeAsync(int companyId, int actionType)
        {
            var ls = new List<NoteViewModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<NoteViewModel>(
                                        _logisticsCommandText.GetNoteByCompanyIdActionType,
                                        new
                                        {
                                            CompanyId = companyId,
                                            ActionType = actionType
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

        public async Task<int> GetNewNumberByActionTypeAsync(int actionType)
        {
            int number = 0;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    number = await connection.ExecuteScalarAsync<int>(
                                            _logisticsCommandText.GetNoteNewNumber,
                                            new
                                            {
                                                ActionType = actionType
                                            },
                                            commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception exception)
            {

            }
            return number;
        }

        public async Task<List<ResulViewModel>> GetNoteBalanceKardexSimpleAsync(int productId, int branchOfficeId)
        {
            var ls = new List<ResulViewModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var  rs = await connection.QueryAsync<ResulViewModel>(
                                        _logisticsCommandText.GetNoteBalanceKardexSimple,
                                        new { ProductId = productId, BranchOfficeId = branchOfficeId },
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

        public async Task<List<ResulViewModel>> GetNoteKardexSimpleAsync(int productId, int branchOfficeId)
        {
            var ls = new List<ResulViewModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rs = await connection.QueryAsync<ResulViewModel>(
                                        _logisticsCommandText.GetNoteKardexSimple,
                                        new { ProductId = productId, BranchOfficeId = branchOfficeId },
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
