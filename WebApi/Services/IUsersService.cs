using Contracts;

using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IUsersService
    {
        //TODO: Create this!
        Task<ValidUserResponse> LoginAsync(LoginUserRequest user);
        //Task<LogoutUserResponse> LogoutAsync(LogoutUserRequest user);
        //Task<User> ChangePasswordAsync(User user, string newPassword);
        Task<ValidUserResponse> RefreshAsync(string token);
    }
}
