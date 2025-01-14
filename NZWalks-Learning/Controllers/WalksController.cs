using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_Learning.Model.Domain;
using NZWalks_Learning.Model.DTO;
using NZWalks_Learning.Repository;

namespace NZWalks_Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;

        public WalksController(IWalkRepository walkRepository)
        {
            this.walkRepository = walkRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            //fetch all walk domain data
            var walks = await walkRepository.GetAllWalksAsync();

            //convert walk domain data to walk DTO
            var walksDto = new List<WalksDto>();
            foreach (var walk in walks) {
                walksDto.Add(new WalksDto()
                {
                    Id = walk.Id,
                    Name = walk.Name,
                    WalkDifficulty = walk.WalkDifficulty,
                    Length = walk.Length,
                    RegionId = walk.RegionId,
                    Region = walk.Region,
                    WalkDifficultyId = walk.WalkDifficultyId

                });
            }
            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{Id:guid}")]
        [ActionName("GetWalkById")]
        public async Task<ActionResult<Walks>> GetWalkById(Guid Id)
        {
            //var walkDomain = await walkRepository.GetWalkByIdAsync(Id);
            var walkDomain = await walkRepository.GetWalkByIdAsync(Id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(walkDomain);
        }

        [HttpPost]
        public async Task<ActionResult<Walks>> CreateWalk([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            //var newWalk = await walkRepository.CreateWalkAsync(addWalksRequestDto);
            //return CreatedAtAction(nameof(GetWalkById), new { Id = newWalk.Id }, newWalk);

            var walk = new Walks()
            {
                Name = addWalksRequestDto.Name,
                Length = addWalksRequestDto.Length,
                RegionId = addWalksRequestDto.RegionId,
                WalkDifficultyId = addWalksRequestDto.WalkDifficultyId
            };

            walk = await walkRepository.CreateWalkAsync(walk);

            if (walk == null)
            {
                return BadRequest("Failed to create walk");
            }

            //return domain to dto
            var walkDto = new WalksDto()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };

            return CreatedAtAction(nameof(GetWalkById), new { Id = walk.Id }, walkDto);
        }

        [HttpPut]
        [Route("{Id:guid}")]
        public async Task<ActionResult<Walks>> UpdateWalk(Guid Id, [FromBody] UpdateWalkRequestDto updateWalksRequestDto)
        {
            var walk = new Walks()
            {
                Id = Id,
                Name = updateWalksRequestDto.Name,
                Length = updateWalksRequestDto.Length,
                RegionId = updateWalksRequestDto.RegionId,
                WalkDifficultyId = updateWalksRequestDto.WalkDifficultyId,
                IsDeleted = updateWalksRequestDto.IsDeleted
            };

            walk = await walkRepository.UpdateWalkAsync(Id, walk);

            if (walk == null)
            {
                return BadRequest("Failed to update walk");
            }
            var walkDto = new WalksDto()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId,
                IsDeleted = walk.IsDeleted
            };

            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{Id:guid}")]

        public async Task<ActionResult<Walks>> DeleteWalk(Guid Id)
        {
            var walk = await walkRepository.DeleteWalkAsync(Id);
            if (walk == null)
            {
                return NotFound();
            }
            var walkDto = new WalksDto()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId,
                IsDeleted = walk.IsDeleted
            };
            return Ok(walkDto);
        }
    }
}
