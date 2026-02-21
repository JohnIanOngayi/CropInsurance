using cropinsurance.server.Models;
using cropinsurance.server.Services;
using Microsoft.AspNetCore.Mvc;

namespace cropinsurance.server.Controllers
{
    [Route("api/seasons/{seasonId}/crops")]
    [ApiController]
    public class CropsController : ControllerBase
    {
        private readonly IRepositoryWrapper repository;

        public CropsController(IRepositoryWrapper wrapper)
        {
            repository = wrapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crop>>> GetBySeasonAll(int seasonId)
        {
            IEnumerable<Crop> crops = await repository.Crops.GetCropsBySeasonAsync(seasonId);
            return Ok(crops);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Crop>> GetBySeasonOne(int seasonId, int id)
        {
            Crop? crop = await repository.Crops.GetCropByIdAsync(id);
            return crop == null ? NotFound() : crop;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> Create (int seasonId, [FromBody] Crop crop)
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

            crop.SeasonId = seasonId;
            var resp = await repository.Crops.CreateCropAsync(crop);

            return Ok(resp);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseModel>> Update (int seasonId, int id, [FromBody] Crop crop)
        {
            if (id != crop.CropId)
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

            var resp = await repository.Crops.UpdateCropAsync(crop);
            return Ok(resp);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseModel>> Delete(int seasonId, int id)
        {
            return Ok(await repository.Crops.DeleteCropAsync(id));
        }
    }
}
