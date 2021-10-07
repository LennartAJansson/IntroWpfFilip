using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Windows;

using WpfApp.Configuration;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
#pragma warning disable IDE0052 // Remove unread private members
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger<MainWindow> logger;
        private readonly MainViewModel viewModel;
        private readonly WindowsInfo windowsInfo;

        public MainWindow(ILogger<MainWindow> logger, MainViewModel viewModel, IOptions<WindowsInfo> options)
        {
            InitializeComponent();
            this.logger = logger;
            this.viewModel = viewModel;
            DataContext = viewModel;
            windowsInfo = options.Value;
            if (windowsInfo.Width != 0 && windowsInfo.Height != 0)
            {
                Height = windowsInfo.Height;
                Width = windowsInfo.Width;
                Left = windowsInfo.Left;
                Top = windowsInfo.Top;
                WindowState = windowsInfo.State;
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            windowsInfo.Height = Height;
            windowsInfo.Width = Width;
            windowsInfo.Left = Left;
            windowsInfo.Top = Top;
            windowsInfo.State = WindowState;

            WindowsInfoRoot root = new()
            {
                WindowsInfo = windowsInfo
            };

            string json = System.Text.Json.JsonSerializer.Serialize(root);
            System.IO.File.WriteAllText(@".\WindowsInfo.json", json);
        }
    }
#pragma warning restore IDE0052 // Remove unread private members
}