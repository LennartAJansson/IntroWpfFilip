using System.Windows.Controls;

using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for PersonView.xaml
    /// </summary>
    public partial class PersonView : UserControl
    {
        public PersonView(PersonViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}