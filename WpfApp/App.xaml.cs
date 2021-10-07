using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Windows;

using WpfApp.Configuration;
using WpfApp.Services;
using WpfApp.ViewModels;
using WpfApp.Views;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => ConfigureAppServices(context, services))
                .Build();
        }

        private static void ConfigureAppServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddApplicationConfiguration(context);
            services.AddApplicationViewModels();
            services.AddApplicationViews();
            services.AddApplicationServices();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            host.Services.GetRequiredService<MainWindow>().Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync();
            }

            base.OnExit(e);
        }
    }
}