using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using System.Threading.Tasks;

using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public class UserViewModel : ObservableObject
    {
        public User User
        {
            get => user;
            set
            {
                SetProperty(ref user, value);
            }
        }
        private User user;

        public string CurrentPassword { get => currentPassword; set => SetProperty(ref currentPassword, value); }
        private string currentPassword;

        public string NewPassword1 { get => newPassword1; set => SetProperty(ref newPassword1, value); }
        private string newPassword1;

        public string NewPassword2 { get => newPassword2; set => SetProperty(ref newPassword2, value); }
        private string newPassword2;
        private readonly ILogger<UserViewModel> logger;
        private readonly ICurrentUser currentUser;

        public bool ShowSelection
        {
            get => showSelection;
            set
            {
                SetProperty(ref showSelection, value);
                if (value)
                    ShowCreate = ShowLogin = ShowChange = ShowLogout = ShowReset = false;
            }
        }
        private bool showSelection;

        public bool ShowCreate
        {
            get => showCreate;
            set
            {
                SetProperty(ref showCreate, value);
                if (value)
                    ShowSelection = ShowLogin = ShowChange = ShowLogout = ShowReset = false;
            }
        }
        private bool showCreate;

        public bool ShowLogin
        {
            get => showLogin;
            set
            {
                SetProperty(ref showLogin, value);
                if (value)
                    ShowSelection = ShowCreate = ShowChange = ShowLogout = ShowReset = false;
            }
        }
        private bool showLogin;

        public bool ShowChange
        {
            get => showChange;
            set
            {
                SetProperty(ref showChange, value);
                if (value)
                    ShowSelection = ShowCreate = ShowLogin = ShowLogout = ShowReset = false;
            }
        }
        private bool showChange;

        public bool ShowLogout
        {
            get => showLogout;
            set
            {
                SetProperty(ref showLogout, value);
                if (value)
                    ShowSelection = ShowCreate = ShowLogin = ShowChange = ShowReset = false;
            }
        }
        private bool showLogout;

        public bool ShowReset
        {
            get => showReset;
            set
            {
                SetProperty(ref showReset, value);
                if (value)
                    ShowSelection = ShowCreate = ShowLogin = ShowChange = ShowLogout = false;
            }
        }
        private bool showReset;

        public AsyncRelayCommand<string> SelectionCommand { get; }
        public AsyncRelayCommand CreateUserCommand { get; }
        public AsyncRelayCommand LoginUserCommand { get; }
        public AsyncRelayCommand ChangeUserCommand { get; }
        public AsyncRelayCommand LogoutUserCommand { get; }
        public AsyncRelayCommand ResetUserCommand { get; }

        public UserViewModel(ILogger<UserViewModel> logger, ICurrentUser currentUser)
        {
            this.logger = logger;
            this.currentUser = currentUser;

            User = (User)this.currentUser;

            this.currentUser.UserChanged += SetCurrentUserNotified;

            SelectionCommand = new AsyncRelayCommand<string>(Select);//, (i) => ShowSelection);
            CreateUserCommand = new AsyncRelayCommand(CreateUser);//, () => ShowCreate);
            LoginUserCommand = new AsyncRelayCommand(LoginUser);//, () => ShowLogin);
            ChangeUserCommand = new AsyncRelayCommand(ChangeUser);//, () => ShowChange);
            LogoutUserCommand = new AsyncRelayCommand(LogoutUser);//, () => ShowLogout);
            ResetUserCommand = new AsyncRelayCommand(ResetUser);//, () => ShowReset);
            ShowSelection = true;
        }

        private Task Select(string arg)
        {
            ShowSelection = false;
            switch (arg)
            {
                case "1":
                    ShowCreate = true;
                    break;
                case "2":
                    ShowLogin = true;
                    break;
                case "3":
                    ShowChange = true;
                    break;
                case "4":
                    ShowLogout = true;
                    break;
                case "5":
                    ShowReset = true;
                    break;
                default:
                    ShowSelection = true;
                    break;
            }
            return Task.CompletedTask;
        }

        private async Task CreateUser()
        {
            SetCurrentUserUnnotified();
            await currentUser.CreateAsync();
            logger.LogInformation("CurrentPassword: {cp}, NewPassword1: {np1}, NewPassword2: {np2}", CurrentPassword, NewPassword1, NewPassword2);
            ShowSelection = true;
        }

        private async Task LoginUser()
        {
            SetCurrentUserUnnotified();
            await currentUser.LoginAsync();
            logger.LogInformation("CurrentPassword: {cp}, NewPassword1: {np1}, NewPassword2: {np2}", CurrentPassword, NewPassword1, NewPassword2);
            ShowSelection = true;
        }

        private async Task ChangeUser()
        {
            SetCurrentUserUnnotified();
            await currentUser.ChangeAsync(NewPassword1);
            logger.LogInformation("CurrentPassword: {cp}, NewPassword1: {np1}, NewPassword2: {np2}", CurrentPassword, NewPassword1, NewPassword2);
            ShowSelection = true;
        }

        private async Task LogoutUser()
        {
            SetCurrentUserUnnotified();
            await currentUser.LogoutAsync();
            logger.LogInformation("CurrentPassword: {cp}, NewPassword1: {np1}, NewPassword2: {np2}", CurrentPassword, NewPassword1, NewPassword2);
            ShowSelection = true;
        }

        private async Task ResetUser()
        {
            SetCurrentUserUnnotified();
            await currentUser.ResetAsync();
            logger.LogInformation("CurrentPassword: {cp}, NewPassword1: {np1}, NewPassword2: {np2}", CurrentPassword, NewPassword1, NewPassword2);
            ShowSelection = true;
        }

        private void SetCurrentUserUnnotified()
        {
            currentUser.UserChanged -= SetCurrentUserNotified;
            currentUser.SetUser(User);
            currentUser.UserChanged += SetCurrentUserNotified;
        }

        private void SetCurrentUserNotified(User user) => User = user;
    }
}
