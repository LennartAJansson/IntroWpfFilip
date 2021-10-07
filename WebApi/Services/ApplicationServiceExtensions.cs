using Microsoft.Extensions.DependencyInjection;

using WebApi.Services;

namespace WpfApp.Services
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IPeopleService, PeopleService>();
            services.AddTransient<IUsersService, UsersService>();

            return services;
        }
    }
}