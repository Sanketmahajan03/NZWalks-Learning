using NZWalks_Learning.Model.Domain;

namespace NZWalks_Learning.Repository
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walks>> GetAllWalksAsync();

        Task<Walks> GetWalkByIdAsync(Guid Id);

        Task<Walks> CreateWalkAsync(Walks walks);

        Task<Walks> UpdateWalkAsync(Guid Id, Walks walks);

        Task<Walks> DeleteWalkAsync(Guid Id);
    }
}
