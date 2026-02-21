using cropinsurance.server.Models;
using cropinsurance.server.Repository.Contracts;
using Dapper;

namespace cropinsurance.server.Repository
{
    public class CropRepository : ICropRepository
    {
        private readonly DbContext _context;
        public CropRepository(DbContext context) => _context = context;

        public async Task<IEnumerable<Crop>> GetAllCropsAsync()
        {
            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<Crop>("SELECT c.CropId, c.CropName, c.SeasonId, s.SeasonName FROM Crops c INNER JOIN Seasons s ON c.SeasonId = s.SeasonId");
        }

        public async Task<IEnumerable<Crop>> GetCropsBySeasonAsync(int seasonId)
        {
            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<Crop>("SELECT CropId, CropName, SeasonId FROM Crops WHERE SeasonId = @seasonId", new { seasonId });
        }

        public async Task<Crop?> GetCropByIdAsync(int id)
        {
            using var conn = _context.CreateConnection();
            return await conn.QuerySingleOrDefaultAsync<Crop>("SELECT CropId, CropName, SeasonId FROM Crops WHERE CropId = @id", new { id });
        }

        public async Task<ResponseModel> CreateCropAsync(Crop crop)
        {
            const string sql = "INSERT INTO Crops (CropName, SeasonId) VALUES (@CropName, @SeasonId); SELECT CAST(SCOPE_IDENTITY() as int);";
            using var conn = _context.CreateConnection();
            var id = await conn.ExecuteScalarAsync<int>(sql, crop);
            return new() { Status = "Success", Message = "Crop Created", NewId = id };
        }

        public async Task<ResponseModel> UpdateCropAsync(Crop crop)
        {
            const string sql = "UPDATE Crops SET CropName = @CropName, SeasonId = @SeasonId WHERE CropId = @CropId";
            using var conn = _context.CreateConnection();
            await conn.ExecuteAsync(sql, crop);
            return new() { Status = "Success", Message = "Crop Updated" };
        }

        public async Task<ResponseModel> DeleteCropAsync(int id)
        {
            using var conn = _context.CreateConnection();
            try
            {
                await conn.ExecuteAsync("DELETE FROM Crops WHERE CropId = @id", new { id });
                return new() { Status = "Success", Message = "Crop Deleted" };
            }
            catch
            {
                return new() { Status = "Error", Message = "Cannot delete crop: it is currently used in applications." };
            }
        }
    }
}