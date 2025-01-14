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

            var walkDifficultiesDto = new List<WalkDifficultyDto>();
            foreach (var walkDifficulty in walkDifficulties)
            {
                walkDifficultiesDto.Add(new WalkDifficultyDto()
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
    }
}
