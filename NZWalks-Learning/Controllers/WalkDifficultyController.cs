using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_Learning.Model.Domain;
using NZWalks_Learning.Model.DTO;
using NZWalks_Learning.Repository;

namespace NZWalks_Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkDifficultyController : ControllerBase
    {
        private readonly IWalkDifficultyRepository walkDifficulty;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficulty)
        {
            this.walkDifficulty = walkDifficulty;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            //fetch all walk difficulty domain data
            var walkDifficulties = await walkDifficulty.GetAllWalkDifficultiesAsync();

            var walkDifficultiesDto = new List<UpdateWalkDifficultyDto>();
            foreach (var walkDifficulty in walkDifficulties)
            {
                walkDifficultiesDto.Add(new UpdateWalkDifficultyDto()
                {
                    Id = walkDifficulty.Id,
                    Code = walkDifficulty.Code,
                });
            }
            return Ok(walkDifficultiesDto);
        }

        [HttpGet]
        [Route("{Id:guid}")]

        public async Task<ActionResult<WalkDifficulty>> GetWalkDifficultyById(Guid Id)
        {
            var walkDifficultyDomain = await walkDifficulty.GetWalkDifficultyByIdAsync(Id);
            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }
            return Ok(walkDifficultyDomain);
        }


        [HttpPost]
        public async Task<IActionResult> AddWalkDiffculty([FromBody] UpdateWalkDifficultyDto walkDifficultyDto)
        {
            //Validate Add Walk Difficulty
            //if (!ValidateAddWalkDifficulty(walkDifficultyDto))
            //{
            //    return BadRequest(ModelState);
            //}
            //Convert walk difficulty DTO to walk difficulty domain
            var walkDifficultyDomain = new WalkDifficulty()
            {
                Code = walkDifficultyDto.Code,
            };

            //Add walk difficulty domain to the database
            var addedWalkDifficulty = await walkDifficulty.AddWalkDifficultyAsync(walkDifficultyDomain);

            //Convert walk difficulty domain to walk difficulty DTO
            var addedWalkDifficultyDto = new UpdateWalkDifficultyDto()
            {
                Id = addedWalkDifficulty.Id,
                Code = addedWalkDifficulty.Code,
            };

            return CreatedAtAction(nameof(GetWalkDifficultyById), new { Id = addedWalkDifficultyDto.Id }, addedWalkDifficultyDto);
        }

        [HttpPut]
        [Route("{Id:guid}")]

        public async Task<IActionResult> UpdateWalkDifficulty(Guid Id, [FromBody] UpdateWalkDifficultyDto updatewalkDifficultyDto)
        {
            //Validate update walk difficulty
            //if (!ValidateUpdateWalkDifficulty(walkDifficultyDto))
            //{
            //    return BadRequest(ModelState);
            //}
            //Convert walk difficulty DTO to walk difficulty domain
            var walkDifficultyDomain = new WalkDifficulty()
            {
                Code = updatewalkDifficultyDto.Code,
            };
            //Update walk difficulty domain in the database
            var updatedWalkDifficulty = await walkDifficulty.UpdateWalkDifficultyAsync(Id, walkDifficultyDomain);
            //Convert walk difficulty domain to walk difficulty DTO
            var updatedWalkDifficultyDto = new UpdateWalkDifficultyDto()
            {
                Id = updatedWalkDifficulty.Id,
                Code = updatedWalkDifficulty.Code,
            };
            return Ok(updatedWalkDifficultyDto);
        }

        [HttpDelete]
        [Route("{Id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync([FromRoute] Guid Id)
        {
            // get Domain data from database 
            //var walkDifficultyDomain = await walkDifficulty.DeleteWalkDifficultyAsync(Id);
            var walkDifficultyDomain = await walkDifficulty.DeleteWalkDifficultyAsync(Id);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            //Return Domain to DTO 

            var walkDifficultyDto = new UpdateWalkDifficultyDto()
            {
                Code = walkDifficultyDomain.Code,
            };

            return Ok(walkDifficultyDto);
        }

        #region Private Methods
        private bool ValidateAddWalkDifficulty(UpdateWalkDifficultyDto walkDifficultyDto)
        {
            if (walkDifficultyDto == null)
            {
                ModelState.AddModelError("walkDifficultyDto", "Walk Difficulty is required");
                return false;
            }
            if (string.IsNullOrEmpty(walkDifficultyDto.Code))
            {
                ModelState.AddModelError("Code", "Code is required");
                return false;
            }
            return true;
        }

        private bool ValidateUpdateWalkDifficulty(UpdateWalkDifficultyDto walkDifficultyDto)
        {
            if (walkDifficultyDto == null)
            {
                ModelState.AddModelError("walkDifficultyDto", "Walk Difficulty is required");
                return false;
            }
            if (string.IsNullOrEmpty(walkDifficultyDto.Code))
            {
                ModelState.AddModelError("Code", "Code is required");
                return false;
            }
            return true;
        }

        #endregion
    }
}
