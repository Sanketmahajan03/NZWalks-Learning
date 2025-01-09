using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_Learning.Model.Domain;
using NZWalks_Learning.Model.DTO;
using NZWalks_Learning.Repository;

namespace NZWalks_Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task <IActionResult> GetAllRegions()
        {
            //Fetch all region data
            var regions = await regionRepository.GetAllRegionsAsync();

            //var regionsDto = new List<RegionDto>();
            //foreach (var region in regions)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population
            //    });
            //}

            //Use AutoMapper to map the region data to regionDto
            var regionsDto = mapper.Map<List<RegionDto>>(regions);

            return Ok(regionsDto);
        }
    }
}
