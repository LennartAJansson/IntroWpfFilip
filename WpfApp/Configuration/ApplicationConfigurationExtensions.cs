using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WpfApp.Configuration
{
    public static class ApplicationConfigurationExtensions
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, HostBuilderContext context)
        {
            //To get an instance of a specific setting using ConfigurationBinder:
            //var setting = hostBuilder.Configuration.GetSection(CmdGroup.Section).Get<CmdGroup>();

            services.Configure<WindowsInfo>(settings =>
                context.Configuration.GetSection(WindowsInfo.SectionName).Bind(settings));

            services.Configure<SampleConfigData>(settings =>
                context.Configuration.GetSection(SampleConfigData.SectionName).Bind(settings));

            services.Configure<HttpClientSettings>(settings =>
                context.Configuration.GetSection(HttpClientSettings.SectionName).Bind(settings));

            return services;
        }
    }
}