using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NZWalks_Learning.Data;
using NZWalks_Learning.Model.Domain;
using System.Data;

namespace NZWalks_Learning.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public WalkRepository(NZWalksDbContext nZWalksDbContext, IConfiguration configuration)
        {
            this.nZWalksDbContext = nZWalksDbContext;
            this.configuration = configuration;
            this.connectionString = configuration.GetConnectionString("NZWalks");
        }
        public async Task<IEnumerable<Walks>> GetAllWalksAsync()
        {
            //try
            //{
            //    return await nZWalksDbContext.Walks
            //        .FromSqlRaw("EXEC GetAllWalks")
            //        .ToListAsync();
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception and handle it
            //    throw new Exception("Error fetching walks from the database.", ex);
            //}
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                //return await connection.QueryAsync<Walks>();
                return await connection.QueryAsync<Walks>("sp_GetAllWalks",
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Walks> GetWalkByIdAsync(Guid Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var Parameters = new DynamicParameters();
                Parameters.Add("@Id", Id);
                return await connection.QueryFirstOrDefaultAsync<Walks>("sp_GetWalkById",
                    Parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Walks> CreateWalkAsync(Walks walk)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var Parameters = new DynamicParameters();
                Parameters.Add("@Name", walk.Name);
                Parameters.Add("@Length", walk.Length);
                Parameters.Add("@RegionId", walk.RegionId);
                Parameters.Add("@WalkDifficultyId", walk.WalkDifficultyId);

                // Log the parameters
                Console.WriteLine($"Executing sp_CreateWalk with parameters: Name={walk.Name}, Length={walk.Length}, RegionId={walk.RegionId}, WalkDifficultyId={walk.WalkDifficultyId}");

                var result = await connection.QueryFirstOrDefaultAsync<Walks>("sp_CreateWalk",
                    Parameters,
                    commandType: CommandType.StoredProcedure);

                // Log the result
                Console.WriteLine($"Result from sp_CreateWalk: {result?.Id}");

                return result;
            }
        }

        public async Task<Walks> UpdateWalkAsync(Guid Id, Walks walks)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                var Parameters = new DynamicParameters();
                Parameters.Add("@Id", walks.Id);
                Parameters.Add("@Name", walks.Name);
                Parameters.Add("@Length", walks.Length);
                Parameters.Add("@RegionId", walks.RegionId);
                Parameters.Add("@WalkDifficultyId", walks.WalkDifficultyId);
                Parameters.Add("@IsDeleted", walks.IsDeleted);

                var result = await connection.QueryFirstOrDefaultAsync<Walks>("sp_UpdateWalk",
                    Parameters,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<Walks> DeleteWalkAsync(Guid Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var Parameters = new DynamicParameters();
                Parameters.Add("@Id", Id);
                var result = await connection.QueryFirstOrDefaultAsync<Walks>("sp_DeleteWalk",
                    Parameters,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
