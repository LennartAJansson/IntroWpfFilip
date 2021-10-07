using Contracts;

using Microsoft.Extensions.Logging;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

using WpfApp.Extensions;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class UsersService : IUsersService
    {
        private readonly ILogger<UsersService> logger;
        private readonly IHttpClientFactory httpClientFactory;

        public UsersService(ILogger<UsersService> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public Task<User> CreateAsync(User user)
        {
            return Task.FromResult(user);
        }

        public async Task<User> LoginAsync(User user)
        {
            string path = "users/login";

            HttpClient unsecureClient = httpClientFactory.CreateClient("UnsecureHttpClient");

            string json = JsonSerializer.Serialize(new LoginUserRequest(Email: user.Email, Password: user.Password));

            HttpContent content = CreateContent($"{unsecureClient.BaseAddress}{path}", json);

            HttpResponseMessage response = await unsecureClient.PostAsync(path, content).ConfigureAwait(false);

            json = await GetResponseStringAsync(response);

            ValidUserResponse userResponse = JsonSerializer.Deserialize<ValidUserResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            User newUser = userResponse.ToUser();

            if (newUser.JwtToken.IsValid)
            {
                user = newUser;
                return user;
            }

            return null;
        }

        public Task<User> ChangeAsync(User user, string newPassword)
        {
            //We should have a Token so use secureClient
            //Get a new token by sending the password change
            return Task.FromResult(user);
        }

        public async Task<User> LogoutAsync(User user)
        {
            //We should have a Token so use secureClient
            HttpClient secureClient = httpClientFactory.CreateClient("SecureHttpClient");

            //Invalidate the token in user and set IsValid to false
            //return Task.FromResult(user);

            string path = "users/logout";

            LogoutUserRequest request = new(Email: user.Email);
            string jsonUser = JsonSerializer.Serialize(request);
            HttpContent content = new StringContent(jsonUser);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            logger.LogHttpRequest(LogLevel.Information, new(HttpMethod.Post, $"{secureClient.BaseAddress}{path}")
            {
                Content = content
            }, "Request to web api");

            HttpResponseMessage response = await secureClient.PostAsync(path, content).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            logger.LogHttpResponse(LogLevel.Information, response, "Response from web api");

            string json = await response.Content.ReadAsStringAsync();
            logger.LogInformation("Response json: {json}", json);

            ValidUserResponse peopleResponse = JsonSerializer.Deserialize<ValidUserResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //TODO: Convert to User
            return user;
        }

        public Task<User> ResetAsync(User user)
        {
            //We should have a Token so use secureClient
            //Update the token in user
            return Task.FromResult(user);
        }

        public async Task<User> RefreshAsync(User user)
        {
            string path = "users/refresh";

            HttpClient secureClient = httpClientFactory.CreateClient("SecureHttpClient");

            logger.LogDebug("Refreshing JwtToken");

            HttpResponseMessage response = await secureClient.GetAsync(path).ConfigureAwait(false);

            string json = await GetResponseStringAsync(response);

            ValidUserResponse userResponse = JsonSerializer.Deserialize<ValidUserResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            User newUser = userResponse.ToUser();

            if (newUser.JwtToken.IsValid)
            {
                user = newUser;
                return user;
            }

            return null;
        }

        private HttpContent CreateContent(string url, string json)
        {
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            logger.LogHttpRequest(LogLevel.Debug, new(HttpMethod.Post, url)
            {
                Content = content
            }, "Request to web api");
            return content;
        }

        private async Task<string> GetResponseStringAsync(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            logger.LogHttpResponse(LogLevel.Debug, response, "Response from web api");
            string json = await response.Content.ReadAsStringAsync();
            logger.LogDebug("Response json: {json}", json);
            return json;
        }
    }
}
