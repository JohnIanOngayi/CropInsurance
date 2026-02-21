using cropinsurance.server.Models;
using cropinsurance.server.Repository.Contracts;
using Dapper;
using Microsoft.Data.SqlClient;

namespace cropinsurance.server.Repository
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly DbContext _context;
        public SeasonRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Season>> GetAllSeasonsAsync()
        {
            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<Season>("SELECT SeasonId, SeasonName FROM Seasons");
        }

        public async Task<Season?> GetSeasonByIdAsync(int id)
        {
            using var conn = _context.CreateConnection();
            return await conn.QuerySingleOrDefaultAsync<Season>("SELECT SeasonId, SeasonName FROM Seasons WHERE SeasonId = @id", new { id });
        }

        public async Task<ResponseModel> CreateSeasonAsync(Season season)
        {
            const string sql = "INSERT INTO Seasons (SeasonName) VALUES (@SeasonName); SELECT CAST(SCOPE_IDENTITY() as int);";
            using var conn = _context.CreateConnection();
            var id = await conn.ExecuteScalarAsync<int>(sql, season);
            return new() { Status = "Success", Message = "Season Created", NewId = id };
        }

        public async Task<ResponseModel> UpdateSeasonAsync(Season season)
        {
            const string sql = "UPDATE Seasons SET SeasonName = @SeasonName WHERE SeasonId = @SeasonId";
            using var conn = _context.CreateConnection();
            await conn.ExecuteAsync(sql, season);
            return new() { Status = "Success", Message = "Season Updated" };
        }

        public async Task<ResponseModel> DeleteSeasonAsync(int id)
        {
            using var conn = _context.CreateConnection();
            await conn.ExecuteAsync("DELETE FROM Seasons WHERE SeasonId = @id", new { id });
            return new() { Status = "Success", Message = "Season Deleted" };
        }
    }
}