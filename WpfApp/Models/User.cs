using Microsoft.Toolkit.Mvvm.ComponentModel;

using Security;

namespace WpfApp.Models
{
    public class User : ObservableObject
    {
        public virtual int Id { get => id; set => SetProperty(ref id, value); }
        protected int id;

        public virtual string Email { get => email; set => SetProperty(ref email, value); }
        protected string email;

        public virtual string Password { get => password; set => SetProperty(ref password, value); }
        protected string password;

        public virtual string Username { get => username; set => SetProperty(ref username, value); }
        protected string username;

        public virtual string Role { get => role; set => SetProperty(ref role, value); }
        protected string role;

        public virtual UserJwtToken JwtToken { get => jwtToken; set => SetProperty(ref jwtToken, value); }
        protected UserJwtToken jwtToken;
    }
}
