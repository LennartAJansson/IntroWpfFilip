using Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

using WebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    //TODO: Map this against the IUserService/UserService
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> logger;
        private readonly IUsersService service;

        public UsersController(ILogger<UsersController> logger, IUsersService service)
        {
            this.logger = logger;
            this.service = service;
        }

        // POST api/<ValuesController>/login
        [HttpPost("login")]
        public async Task<ValidUserResponse> LoginAsync([FromBody] LoginUserRequest request)
        {
            logger.LogInformation("Logging in {email}", request.Email);
            ValidUserResponse response = await service.LoginAsync(request);
            return response;
        }

        // POST api/<ValuesController>/login
        //[HttpPost("logout")]
        //public async Task<LogoutUserResponse> LogoutAsync([FromBody] LogoutUserRequest request)
        //{
        //    LogoutUserResponse response = await service.LogoutAsync(request);
        //    return response;
        //}

        // POST api/<ValuesController>/login
        [HttpGet("refresh")]
        public async Task<ValidUserResponse> RefreshAsync()
        {
            string token = Request.Headers["Authorization"];
            ValidUserResponse response = null;
            if (!string.IsNullOrWhiteSpace(token) && token.StartsWith("Bearer "))
            {
                token = token.Remove(0, "Bearer ".Length);
                logger.LogInformation("Refreshing token {token}", token);
                response = await service.RefreshAsync(token);
            }

            return response;
        }

        //TODO Define Put method in api
        // PUT api/<ValuesController>/5
        //[HttpPut]
        //public Task<UpdateUserResponse> PutAsync(int id, [FromBody] UpdateUserRequest request)
        //{
        //    TODO: Map this against the IUserService/UserService
        //    UpdateUserResponse response = new ();
        //    return Task.FromResult(response);
        //}

        //TODO Define Delete method in api
        // DELETE api/<ValuesController>/5
        //[HttpDelete]
        //public Task<DeleteUserResponse> DeleteAsync([FromBody] DeleteUserRequest request)
        //{
        //    TODO: Map this against the IUserService/UserService
        //    DeleteUserResponse response = new ();
        //    return Task.FromResult(response);
        //}
    }
}
