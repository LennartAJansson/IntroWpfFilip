using Microsoft.Extensions.DependencyInjection;

namespace WpfApp.Views
{
    public static class ApplicationViewExtensions
    {
        public static IServiceCollection AddApplicationViews(this IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<PeopleView>();
            services.AddTransient<PersonView>();
            services.AddTransient<GraphView>();
            services.AddTransient<UserView>();
            //Do not add those who should use an inherited viewmodel from a parent,
            //MainMenu, ToolBars and StatusBar are using MainWindow's MainViewModel
            return services;
        }
    }
}