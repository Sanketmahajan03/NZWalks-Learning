using Microsoft.Data.SqlClient;
using NZWalks_Learning.Data;
using NZWalks_Learning.Model.Domain;
using Dapper;
using System.Data;

namespace NZWalks_Learning.Repository
{
    public class WalkDifficultyRepositorycs : IWalkDifficultyRepository
    {
        private readonly IConfiguration configuration;
        private readonly NZWalksDbContext nZWalksDbContext;
        private readonly string connectionString;

        public WalkDifficultyRepositorycs(IConfiguration configuration, NZWalksDbContext nZWalksDbContext)
        {
            this.configuration = configuration;
            this.nZWalksDbContext = nZWalksDbContext;
            this.connectionString = configuration.GetConnectionString("NZWalks");
        }
        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficultiesAsync()
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<WalkDifficulty>("sp_GetAllWalkDifficulty",
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<WalkDifficulty> GetWalkDifficultyByIdAsync(Guid Id)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                var Parameters = new DynamicParameters();
                Parameters.Add("@Id", Id);
                return await connection.QueryFirstOrDefaultAsync<WalkDifficulty>("sp_GetWalkDifficultyById",
                    Parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
