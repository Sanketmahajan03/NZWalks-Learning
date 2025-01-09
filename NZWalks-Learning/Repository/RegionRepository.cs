using Microsoft.EntityFrameworkCore;
using NZWalks_Learning.Data;
using NZWalks_Learning.Model.Domain;

namespace NZWalks_Learning.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
           var region = await nZWalksDbContext.Regions.ToListAsync();

            return region;
        }
    }
}
