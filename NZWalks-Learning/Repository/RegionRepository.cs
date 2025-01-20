using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.Regions.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x=> x.Id == id);

            if (region == null)
            {
                return null;
            }
            // Delete the Region
            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
           return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetRegionByIdAsync(Guid id)
        {
           return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> updateRegionAsync(Guid id, Region region)
        {
           var existingRegion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Population = region.Population;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;  
            existingRegion.Code = region.Code;

            await nZWalksDbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
