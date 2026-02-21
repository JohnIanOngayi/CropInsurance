using cropinsurance.server.Models;
using cropinsurance.server.Repository.Contracts;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace cropinsurance.server.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly DbContext _context;

        public ApplicationRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> CreateApplicationAsync(InsuranceApplication insuranceApplication)
        {
            using var connection = (SqlConnection)_context.CreateConnection();
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();
            try
            {
                // Business Rule 6: Check for Duplicate Aadhaar for the SAME crop
                const string checkSql = "SELECT COUNT(1) FROM InsuranceApplications WHERE AadNo = @AadNo AND CropId = @CropId";
                int exists = await connection.ExecuteScalarAsync<int>(checkSql, new { insuranceApplication.AadNo, insuranceApplication.CropId }, transaction);

                if (exists > 0)
                {
                    return new() { Status = "Error", Message = "Duplicate Aadhaar found for selected crop." }; //
                }

                const string insertSql = @"
                    INSERT INTO InsuranceApplications (
                        SeasonId, CropId, FarmerName, AadNo, FatherName, CompleteAddress, FarmerCategory, SubmissionDate
                    ) VALUES (
                        @SeasonId, @CropId, @FarmerName, @AadNo, @FatherName, @CompleteAddress, @FarmerCategory, @SubmissionDate
                    );
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var insertId = await connection.ExecuteScalarAsync<int>(insertSql, insuranceApplication, transaction);

                transaction.Commit();
                return new() { Status = "Success", Message = "Application submitted successfully.", NewId = insertId };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex);
                return new() { Status = "Exception", Message = "An exception occurred", Error = ex.Message };
            }
        }

        public async Task<ResponseModel> EditApplicationAsync(InsuranceApplication app)
        {
            using var connection = (SqlConnection)_context.CreateConnection();
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();
            try
            {
                const string updateSql = @"
                    UPDATE InsuranceApplications 
                    SET SeasonId = @SeasonId, 
                        CropId = @CropId, 
                        FarmerName = @FarmerName, 
                        AadNo = @AadNo, 
                        FatherName = @FatherName, 
                        CompleteAddress = @CompleteAddress, 
                        FarmerCategory = @FarmerCategory
                    WHERE ApplicationId = @ApplicationId";

                int affected = await connection.ExecuteAsync(updateSql, app, transaction);
                transaction.Commit();

                if (affected == 0) return new() { Status = "Error", Message = "Record not found" };
                return new() { Status = "Success", Message = "Application updated successfully" };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new() { Status = "Exception", Message = "Update failed", Error = ex.Message };
            }
        }

        public async Task<ResponseModel> DeleteApplicationAsync(int applicationId)
        {
            using var connection = (SqlConnection)_context.CreateConnection();
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();
            try
            {
                const string deleteSql = @"DELETE FROM InsuranceApplications WHERE ApplicationId = @Id;";
                int delCount = await connection.ExecuteAsync(deleteSql, new { Id = applicationId }, transaction);

                transaction.Commit();

                if (delCount == 0)
                    return new() { Status = "Error", Message = $"Application with Id {applicationId} does not exist" };

                return new() { Status = "Success", Message = "Application deleted successfully" };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new() { Status = "Exception", Message = "Deletion failed", Error = ex.Message };
            }
        }

        public async Task<IEnumerable<InsuranceApplication>> GetAllApplicationsAsync()
        {
            try
            {
                const string querySql = @"
                    SELECT ci.*, s.SeasonName, c.CropName
                    FROM InsuranceApplications as ci
                    INNER JOIN Seasons as s ON ci.SeasonId = s.SeasonId
                    INNER JOIN Crops as c ON c.CropId = ci.CropId;";

                using var connection = _context.CreateConnection();
                return await connection.QueryAsync<InsuranceApplication>(querySql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Enumerable.Empty<InsuranceApplication>();
            }
        }

        public async Task<InsuranceApplication?> GetApplicationAsync(int applicationId)
        {
            try
            {
                const string querySql = @"
                    SELECT ci.*, s.SeasonName, c.CropName
                    FROM InsuranceApplications as ci
                    INNER JOIN Seasons as s ON ci.SeasonId = s.SeasonId
                    INNER JOIN Crops as c ON c.CropId = ci.CropId
                    WHERE ci.ApplicationId = @ApplicationId;";

                using var connection = _context.CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<InsuranceApplication>(querySql, new { ApplicationId = applicationId });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}