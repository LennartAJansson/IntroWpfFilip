using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Net.Http;
using System.Net.Http.Headers;

using WpfApp.Configuration;

namespace WpfApp.Services
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpClient("SecureHttpClient", ConfigureSecureHttpClientOptions);
            services.AddHttpClient("UnsecureHttpClient", ConfigureUnsecureHttpClientOptions);

            services.AddHttpClient<IPeopleService, PeopleService>(ConfigureSecureHttpClientOptions);
            services.AddSingleton<ICurrentPerson, CurrentPerson>();

            services.AddTransient<IUsersService, UsersService>();
            services.AddSingleton<ICurrentUser, CurrentUser>();

            return services;
        }

        private static void ConfigureSecureHttpClientOptions(IServiceProvider serviceProvider, HttpClient client)
        {
            HttpClientSettings options = serviceProvider.GetService<IConfiguration>()
                                .GetSection(HttpClientSettings.SectionName)
                                .Get<HttpClientSettings>();

            ICurrentUser currentUser = serviceProvider.GetService<ICurrentUser>();

            client.BaseAddress = new Uri(options.Url);

            //https://stackoverflow.com/questions/30858890/how-to-use-httpclient-to-post-with-authentication

            //If using a ClientId + ClientSecret
            //var byteArray = new UTF8Encoding().GetBytes($"{options.ClientId}:{options.ClientSecret}");
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            //If using a JwtToken:
            if (currentUser.JwtToken != null && !string.IsNullOrWhiteSpace(currentUser.JwtToken.Token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentUser.JwtToken.Token);
        }

        private static void ConfigureUnsecureHttpClientOptions(IServiceProvider serviceProvider, HttpClient client)
        {
            //This should be used by the service that do the actual login, we still don't have any Token
            HttpClientSettings options = serviceProvider.GetService<IConfiguration>()
                .GetSection(HttpClientSettings.SectionName)
                .Get<HttpClientSettings>();

            client.BaseAddress = new Uri(options.Url);
        }
    }
}