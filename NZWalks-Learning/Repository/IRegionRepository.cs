using NZWalks_Learning.Model.Domain;

namespace NZWalks_Learning.Repository
{
    public interface IRegionRepository
    {
        Task <IEnumerable<Region>> GetAllRegionsAsync();
    }
}
