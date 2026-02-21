using cropinsurance.server.Models;
using cropinsurance.server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace cropinsurance.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonsController : ControllerBase
    {
        private readonly IRepositoryWrapper repository;

        public SeasonsController(IRepositoryWrapper repositoryWrapper)
        {
            repository = repositoryWrapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Season>>> GetAll()
        {
            List<Season>? seasons = await repository.Seasons.GetAllSeasonsAsync() as List<Season>;
            return Ok(seasons);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Season>> Get(int id)
        {
            var season = await repository.Seasons.GetSeasonByIdAsync(id);
            return season == null ? NotFound() : Ok(season);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> Create([FromBody] Season season)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                string combinedErrors = string.Join(" | ", errorList);

                return BadRequest(new ResponseModel
                {
                    Status = "Error",
                    Message = combinedErrors
                });
            }

            var resp = await repository.Seasons.CreateSeasonAsync(season);
            return Ok(resp);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel>> Update(int id, [FromBody] Season season)
        {
            if (id != season.SeasonId)
                return BadRequest(new ResponseModel() { Status = "Error", Message = "ID Mismatch" });

            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                string combinedErrors = string.Join(" | ", errorList);

                return BadRequest(new ResponseModel
                {
                    Status = "Error",
                    Message = combinedErrors
                });
            }

            var response = await repository.Seasons.UpdateSeasonAsync(season);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await repository.Seasons.DeleteSeasonAsync(id));
        }
    }
}
