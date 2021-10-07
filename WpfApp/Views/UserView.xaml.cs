using System.Windows.Controls;

using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        public UserView(UserViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
