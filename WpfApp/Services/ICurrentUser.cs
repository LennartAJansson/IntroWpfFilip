using Security;

using System.Threading.Tasks;

using WpfApp.Events;
using WpfApp.Models;

namespace WpfApp.Services
{
    public interface ICurrentUser
    {
        event UserChangedDelegate UserChanged;

        int Id { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string Username { get; set; }
        string Role { get; set; }
        UserJwtToken JwtToken { get; set; }

        Task<User> GetUser();
        Task SetUser(User user);
        Task<bool> CreateAsync();
        Task<bool> LoginAsync();
        Task<bool> ChangeAsync(string newPassword);
        Task<bool> LogoutAsync();
        Task<bool> ResetAsync();
    }
}
