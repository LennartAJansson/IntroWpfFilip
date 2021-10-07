using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi.Configuration
{
    public static class ApplicationConfigurationExtensions
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, HostBuilderContext context)
        {
            //To get an instance of a specific setting using ConfigurationBinder:
            //T setting = hostBuilder.Configuration.GetSection(SectionName).Get<T>();

            return services;
        }
    }
}