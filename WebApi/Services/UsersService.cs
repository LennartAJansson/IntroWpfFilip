using Contracts;

using Microsoft.Extensions.Logging;

using Security;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public class UsersService : IUsersService
    {
        //TODO: Create this!
        private readonly ILogger<UsersService> logger;
        private readonly GetUsersResponse usersResponse;

        public UsersService(ILogger<UsersService> logger)
        {
            this.logger = logger;
            usersResponse = GetTestUsersAsync().Result;
        }

        public Task<ValidUserResponse> LoginAsync(LoginUserRequest user)
        {
            //TODO: Handle illegal requests
            GetUserResponse userResponse = usersResponse.Users.Where(u => u.Email == user.Email).FirstOrDefault();

            ValidUserResponse response = new(AccessToken: UserJwtToken.GenerateToken(userResponse.Id, userResponse.Email, userResponse.Username, userResponse.Role));

            return Task.FromResult(response);
        }

        //public Task<LogoutUserResponse> LogoutAsync(LogoutUserRequest user)
        //{
        //    //Invalidate the token in user and set IsValid to false
        //    GetUserResponse userResponse = usersResponse.Users.Where(u => u.Email == user.Email).FirstOrDefault();

        //    LogoutUserResponse response = new(Username: userResponse.Username, Email: userResponse.Email);
        //    return Task.FromResult(response);
        //}

        //public Task<User> ChangePasswordAsync(User user, string newPassword)
        //{
        //    HttpClient client = httpClientFactory.CreateClient("SecureHttpClient");
        //    //Get a new token by sending the password change
        //    return Task.FromResult(user);
        //}

        public Task<ValidUserResponse> RefreshAsync(string token)
        {
            //TODO: Handle illegal requests
            GetUserResponse userResponse = usersResponse.Users.Where(u => u.Email == new UserJwtToken(token).Email).FirstOrDefault();

            ValidUserResponse response = new(AccessToken: UserJwtToken.GenerateToken(userResponse.Id, userResponse.Email, userResponse.Username, userResponse.Role));

            return Task.FromResult(response);
        }

        public Task<GetUsersResponse> GetTestUsersAsync()
        {
            IEnumerable<GetUserResponse> p = new List<GetUserResponse>
                    {
                        new GetUserResponse(Id:1,
                            Username:"Lennart",
                            Email:"lennart.jansson@nexergroup.com",
                            Role: "Administrator"),
                        new GetUserResponse(
                            Id:2,
                            Username:"Adam",
                            Email:"adh01@hotmail.se",
                            Role: "Administrator"),
                    };

            return Task.FromResult(new GetUsersResponse(p));
        }

    }
}
