using NZWalks_Learning.Model.Domain;

namespace NZWalks_Learning.Repository
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>>GetAllWalkDifficultiesAsync();

        Task<WalkDifficulty> GetWalkDifficultyByIdAsync(Guid Id);

       Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty);

       Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid Id, WalkDifficulty walkDifficulty);

       Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id);
    }
}
