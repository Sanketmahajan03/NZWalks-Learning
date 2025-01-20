using NZWalks_Learning.Model.Domain;

namespace NZWalks_Learning.Repository
{
    public interface IRegionRepository
    {
        Task <IEnumerable<Region>> GetAllRegionsAsync();
        Task<Region> GetRegionByIdAsync(Guid id);
        Task<Region> AddRegionAsync(Region region);
        Task<Region> DeleteRegionAsync(Guid id);
        Task<Region> updateRegionAsync(Guid id, Region region);
    }
}
