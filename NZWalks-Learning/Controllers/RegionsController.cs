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


        [HttpGet]
        [Route("{Id:guid}")]
        [ActionName("GetRegionById")]
        public async Task<IActionResult> GetRegionById(Guid Id)
        {
            var regionDomain = await regionRepository.GetRegionByIdAsync(Id);

            if(regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomain);
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(AddRegionRequestDto addRegionRequestDto)
        {
            //fetch request(DTO) to Domain
            var region = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                Lat = addRegionRequestDto.Lat,
                Long = addRegionRequestDto.Long,
                Area = addRegionRequestDto.Area,
                Population = addRegionRequestDto.Population,
            };
            //Pass the details to Repository
            region = await regionRepository.AddRegionAsync(region);

            //Return domain to dto
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Long = region.Long,
                Area = region.Area,
                Population = region.Population,
                Lat = region.Lat
            };

            return CreatedAtAction(nameof(GetRegionById), new {Id = regionDto.Id }, regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
           //Get Region from database 
           var region = await regionRepository.DeleteRegionAsync(id);
            //if null return notfound

            if(region == null)
            {
                return NotFound();
            }
            //convert responce back to DTO

            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Long = region.Long,
                Area = region.Area,
                Population = region.Population,
                Lat = region.Lat
            };
            //return OK
            return Ok(regionDto);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute]Guid id, [FromBody]updateRegionRequestDto regionRequestDto)
        {
            //convert Dto to Domain
            var region = new Region()
            {
                Code = regionRequestDto.Code,
                Name = regionRequestDto.Name,
                Lat = regionRequestDto.Lat,
                Long = regionRequestDto.Long,
                Area = regionRequestDto.Area,
                Population = regionRequestDto.Population
            };
            //update Region using repository
            var regions = await regionRepository.updateRegionAsync(id, region);

            //if null return not found
            if(region == null)
            {
                return NotFound();
            }
            //convert back dto to domain

            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Long = region.Long,
                Area = region.Area,
                Population = region.Population,
                Lat = region.Lat
            };
            //return ok
            return Ok(regionDto);
        }
    }
}
