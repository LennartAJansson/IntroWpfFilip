using System.Threading.Tasks;

using WpfApp.Models;

namespace WpfApp.Services
{
    public interface IUsersService
    {
        Task<User> CreateAsync(User user);
        Task<User> LoginAsync(User user);
        Task<User> ChangeAsync(User user, string newPassword);
        Task<User> LogoutAsync(User user);
        Task<User> ResetAsync(User user);
        Task<User> RefreshAsync(User user);
    }
}
