using cropinsurance.server.Models;
using cropinsurance.server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cropinsurance.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IRepositoryWrapper repository;

        public ApplicationsController(IRepositoryWrapper repositoryWrapper)
        {
            repository = repositoryWrapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsuranceApplication>>> GetAll()
        {
            IEnumerable<InsuranceApplication> applications = await repository.Applications.GetAllApplicationsAsync();
            return Ok(applications);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<InsuranceApplication>> Get(int id)
        {
            InsuranceApplication? application = await repository.Applications.GetApplicationAsync(id);
            return application == null ? NotFound() : Ok(application);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] InsuranceApplication app)
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

            ResponseModel response = await repository.Applications.CreateApplicationAsync(app);
            return response.Status == "Error" ? BadRequest(response) : Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel>> Update(int id, [FromBody] InsuranceApplication app)
        {
            if (id != app.ApplicationId)
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

            var response = await repository.Applications.EditApplicationAsync(app);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await repository.Applications.DeleteApplicationAsync(id));
        }
    }
}
