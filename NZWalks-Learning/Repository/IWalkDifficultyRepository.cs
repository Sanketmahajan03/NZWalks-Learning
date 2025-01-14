using NZWalks_Learning.Model.Domain;

namespace NZWalks_Learning.Repository
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>>GetAllWalkDifficultiesAsync();

        Task<WalkDifficulty> GetWalkDifficultyByIdAsync(Guid Id);
    }
}
