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
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalksController(IWalkRepository walkRepository, 
            IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.walkRepository = walkRepository;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
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
            //Validate the walk data
            if(ValidateAddWalkAsync(addWalksRequestDto) == false)
            {
                return BadRequest(ModelState);
            }

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
            //Validate the walk data
            if (!ValidateUpdateWalkAsync(updateWalksRequestDto))
            {
                return BadRequest(ModelState);
            }
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


        #region Private Methods
        private bool ValidateAddWalkAsync(AddWalksRequestDto addWalksRequestDto)
        {
            //if (addWalksRequestDto == null)
            //{
            //    return false;
            //}
            //if (string.IsNullOrEmpty(addWalksRequestDto.Name))
            //{
            //    ModelState.AddModelError(nameof(addWalksRequestDto.Name),
            //        $"{nameof(addWalksRequestDto.Name)} cannot be null, Empty OR white spaces.");
            //}
            //if (addWalksRequestDto.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(addWalksRequestDto.Length),
            //        $"{nameof(addWalksRequestDto.Length)} cannot be less than or equal to 0.");
            //}
            var region = regionRepository.GetRegionByIdAsync(addWalksRequestDto.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalksRequestDto.RegionId),
                    $"{nameof(addWalksRequestDto.RegionId)} does not exist.");
            }

            var walkDifficulty = walkDifficultyRepository.GetWalkDifficultyByIdAsync(addWalksRequestDto.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(addWalksRequestDto.WalkDifficultyId),
                    $"{nameof(addWalksRequestDto.WalkDifficultyId)} does not exist.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateWalkAsync(UpdateWalkRequestDto updateWalksRequestDto)
        {
            //if (updateWalksRequestDto == null)
            //{
            //    return false;
            //}
            //if (string.IsNullOrEmpty(updateWalksRequestDto.Name))
            //{
            //    ModelState.AddModelError(nameof(updateWalksRequestDto.Name),
            //        $"{nameof(updateWalksRequestDto.Name)} cannot be null, Empty OR white spaces.");
            //}
            //if (updateWalksRequestDto.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(updateWalksRequestDto.Length),
            //        $"{nameof(updateWalksRequestDto.Length)} cannot be less than or equal to 0.");
            //}
            var region = regionRepository.GetRegionByIdAsync(updateWalksRequestDto.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalksRequestDto.RegionId),
                    $"{nameof(updateWalksRequestDto.RegionId)} does not exist.");
            }
            var walkDifficulty = walkDifficultyRepository.GetWalkDifficultyByIdAsync(updateWalksRequestDto.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalksRequestDto.WalkDifficultyId),
                    $"{nameof(updateWalksRequestDto.WalkDifficultyId)} does not exist.");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
