using System.Windows.Controls;

using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for GraphView.xaml
    /// </summary>
    public partial class GraphView : UserControl
    {
        public GraphView(GraphViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            this.SizeChanged += GraphView_SizeChanged;
        }

        private void GraphView_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            //TrueHeight = e.NewSize.Height;
            //TrueWidth = e.NewSize.Width;
        }

        //public double TrueHeight
        //{
        //    get { return (double)GetValue(TrueHeightProperty); }
        //    set { SetValue(TrueHeightProperty, value); }
        //}

        //public double TrueWidth
        //{
        //    get { return (double)GetValue(TrueWidthProperty); }
        //    set { SetValue(TrueWidthProperty, value); }
        //}

        //public static readonly DependencyProperty TrueHeightProperty =
        //DependencyProperty.Register("TrueHeight", typeof(double), typeof(GraphView), new PropertyMetadata(0));

        //public static readonly DependencyProperty TrueWidthProperty =
        //DependencyProperty.Register("TrueWidth", typeof(double), typeof(GraphView), new PropertyMetadata(0));
    }
}
