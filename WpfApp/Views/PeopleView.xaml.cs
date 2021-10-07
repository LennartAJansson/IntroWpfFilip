using System.Windows.Controls;

using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for PeopleView.xaml
    /// </summary>
    public partial class PeopleView : UserControl
    {
        public PeopleView(PeopleViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}