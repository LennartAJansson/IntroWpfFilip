using Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Security;

using System.Threading.Tasks;

using WebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ILogger<PeopleController> logger;
        private readonly IPeopleService service;

        public PeopleController(ILogger<PeopleController> logger, IPeopleService service)
        {
            this.logger = logger;
            this.service = service;
        }

        // GET: api/<PeopleController>
        [HttpGet]
        public async Task<ActionResult<GetPeopleResponse>> GetAsync()
        {
            string token = Request.Headers["Authorization"];
            if (!string.IsNullOrWhiteSpace(token) && token.StartsWith("Bearer "))
            {
                token = token.Remove(0, "Bearer ".Length);
                logger.LogInformation("Using token {token}", token);
                UserJwtToken jwtToken = new UserJwtToken(token);
                if (jwtToken.IsValid)
                {
                    GetPeopleResponse response = await service.GetPeopleAsync();
                    return Ok(response);
                }
                else
                {
                    logger.LogWarning("Token is not valid");
                }
            }

            logger.LogWarning("No valid Auth header");
            return Unauthorized(null);
        }

        // GET api/<PeopleController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPersonResponse>> GetAsync(int id)
        {
            string token = Request.Headers["Authorization"];
            if (!string.IsNullOrWhiteSpace(token) && token.StartsWith("Bearer "))
            {
                token = token.Remove(0, "Bearer ".Length);
                logger.LogInformation("Using token {token}", token);
                UserJwtToken jwtToken = new UserJwtToken(token);
                if (jwtToken.IsValid)
                {
                    GetPersonResponse response = await service.GetPersonAsync(id);
                    return Ok(response);
                }
                else
                {
                    logger.LogWarning("Token is not valid");
                }
            }

            logger.LogWarning("No valid Auth header");
            return Unauthorized(null);
        }

        //TODO Define Post method in api
        // POST api/<PeopleController>
        //[HttpPost]
        //public Task<AddPersonResponse> PostAsync([FromBody] AddPersonRequest request)
        //{
        //    AddUserResponse response = new();
        //    return Task.FromResult(response);
        //}

        //TODO Define Put method in api
        // PUT api/<PeopleController>/5
        //[HttpPut]
        //public Task<UpdatePersonResponse> PutAsync([FromBody] UpdatePersonRequest request)
        //{
        //    UpdateUserResponse response = new ();
        //    return Task.FromResult(response);
        //}

        //TODO Define Delete method in api
        // DELETE api/<PeopleController>/5
        //[HttpDelete]
        //public Task<DeletePersonResponse> DeleteAsync([FromBody] DeletePersonRequest request)
        //{
        //    DeleteUserResponse response = new ();
        //    return Task.FromResult(response);
        //}
    }
}