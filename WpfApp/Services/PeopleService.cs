using Contracts;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using WpfApp.Extensions;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly ILogger<PeopleService> logger;
        private readonly HttpClient client; //This is a Secure Http Client

        public PeopleService(ILogger<PeopleService> logger, HttpClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
            string path = "people";
            HttpRequestMessage request = new(HttpMethod.Get, $"{client.BaseAddress}{path}");
            logger.LogHttpRequest(LogLevel.Information, request, "Request to web api");

            HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            logger.LogHttpResponse(LogLevel.Information, response, "Response from web api");

            string json = await response.Content.ReadAsStringAsync();
            logger.LogInformation("Response json: {json}", json);

            GetPeopleResponse peopleResponse = JsonSerializer.Deserialize<GetPeopleResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return peopleResponse.ToPeople();
        }

        public async Task<Person> GetPersonAsync(object id)
        {
            string path = $"people/{id}";
            HttpRequestMessage request = new(HttpMethod.Get, $"{client.BaseAddress}{path}");
            logger.LogHttpRequest(LogLevel.Information, request, "Request to web api");

            HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            logger.LogHttpResponse(LogLevel.Information, response, "Response from web api");

            string json = await response.Content.ReadAsStringAsync();
            logger.LogInformation("Response json: {json}", json);

            GetPersonResponse personResponse = JsonSerializer.Deserialize<GetPersonResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return personResponse.ToPerson();
        }
    }
}