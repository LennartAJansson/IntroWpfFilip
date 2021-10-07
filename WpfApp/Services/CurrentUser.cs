
using Nito.AsyncEx.Synchronous;

using Security;

using System;
using System.Threading;
using System.Threading.Tasks;

using WpfApp.Events;
using WpfApp.Models;

namespace WpfApp.Services
{
    /// <summary>
    /// Class that lives as a singleton and use a timer to refresh its token for the SelectedUser
    /// </summary>
    public class CurrentUser : User, ICurrentUser, IDisposable
    {
        //This is inherited so it will trigger PropertyChangedNotification
        public override int Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
                UserChanged?.Invoke(this);
            }
        }

        //This is inherited so it will trigger PropertyChangedNotification
        public override string Email
        {
            get => email;
            set
            {
                SetProperty(ref email, value);
                UserChanged?.Invoke(this);
            }
        }

        //This is inherited so it will trigger PropertyChangedNotification
        public override string Password
        {
            get => password;
            set
            {
                SetProperty(ref password, value);
                UserChanged?.Invoke(this);
            }
        }

        //This is inherited so it will trigger PropertyChangedNotification
        public override string Username
        {
            get => username;
            set
            {
                SetProperty(ref username, value);
                UserChanged?.Invoke(this);
            }
        }

        //This is inherited so it will trigger PropertyChangedNotification
        public override string Role
        {
            get => role;
            set
            {
                SetProperty(ref role, value);
                UserChanged?.Invoke(this);
            }
        }

        //This is inherited so it will trigger PropertyChangedNotification
        public override UserJwtToken JwtToken
        {
            get => jwtToken;
            set
            {
                SetProperty(ref jwtToken, value);
                UserChanged?.Invoke(this);
            }
        }

        private bool isDisposed;

        private readonly IUsersService userService;
        private readonly Timer refreshTimer;

        public event UserChangedDelegate UserChanged;

        public CurrentUser(IUsersService userService)
        {
            this.userService = userService;
            refreshTimer = new Timer(DoRefresh, JwtToken, Timeout.Infinite, Timeout.Infinite);
        }

        public Task<User> GetUser()
        {
            return Task.FromResult(this as User);
        }

        public Task SetUser(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Password = user.Password;
            Username = user.Username;
            Role = user.Role;
            JwtToken = user.JwtToken;

            return Task.CompletedTask;
        }

        public async Task<bool> CreateAsync()
        {
            await userService.CreateAsync(this);

            return JwtToken.IsValid;
        }

        public async Task<bool> LoginAsync()
        {
            User user = await userService.LoginAsync(this);
            await SetUser(user);

            if (JwtToken != null && JwtToken.IsValid)
            {
                double refreshSeconds = (JwtToken.Expires - DateTime.Now).TotalSeconds / 2;
                refreshTimer?.Change(TimeSpan.FromSeconds(refreshSeconds), TimeSpan.FromSeconds(refreshSeconds));
                return true;
            }
            else
            {
                refreshTimer?.Change(Timeout.Infinite, Timeout.Infinite);
                return false;
            }
        }

        public async Task<bool> ChangeAsync(string newPassword)
        {
            await userService.ChangeAsync(this, newPassword);

            return JwtToken.IsValid;
        }

        public async Task<bool> LogoutAsync()
        {
            await userService.LogoutAsync(this);

            return JwtToken.IsValid;
        }

        public async Task<bool> ResetAsync()
        {
            await userService.ResetAsync(this);

            return JwtToken.IsValid;
        }

        private void DoRefresh(object state)
        {
            //This method does not execute on the thread that created the timer; it executes on a ThreadPool thread supplied by the system.
            var task = userService.RefreshAsync(this);
            User user = task.WaitAndUnwrapException();
            SetUser(user);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                refreshTimer?.Change(Timeout.Infinite, Timeout.Infinite);
                refreshTimer?.Dispose();
            }

            isDisposed = true;
        }
    }
}
