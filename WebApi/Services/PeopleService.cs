using Contracts;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly ILogger<PeopleService> logger;

        public PeopleService(ILogger<PeopleService> logger)
        {
            this.logger = logger;
        }

        public Task<GetPeopleResponse> GetPeopleAsync()
        {
            logger.LogDebug("Getting people");

            IEnumerable<GetPersonResponse> p = new List<GetPersonResponse>
                    {
                        new GetPersonResponse(Id:1,
                            Ssn:"196101231451",
                            Firstname:"Lennart",
                            Lastname:"Jansson",
                            Address:"Klockaregatan 2a",
                            Postalcode:"25200",
                            City:"Helsingborg",
                            Email:"lennart.jansson@nexergroup.com",
                            Telephone:"+46734400114",
                            IsYes: "No",
                            IsTrue: "False",
                            IsAdult: "Child"),
                        new GetPersonResponse(
                            Id:2,
                            Ssn:"200111234076",
                            Firstname:"Adam",
                            Lastname:"Häggström",
                            Address:"Nordanväg 6c",
                            Postalcode:"24438",
                            City:"Kävlinge",
                            Email:"adh01@hotmail.se",
                            Telephone:"+46723080868",
                            IsYes: "No",
                            IsTrue: "False",
                            IsAdult: "Child"),
                    };

            return Task.FromResult(new GetPeopleResponse(p));
        }

        public Task<GetPersonResponse> GetPersonAsync(object id)
        {
            logger.LogDebug("Getting person");

            return Task.FromResult(new GetPersonResponse(Id: 1,
                            Ssn: "196101231451",
                            Firstname: "Lennart",
                            Lastname: "Jansson",
                            Address: "Klockaregatan 2a",
                            Postalcode: "25200",
                            City: "Helsingborg",
                            Email: "lennart.jansson@nexergroup.com",
                            Telephone: "+46734400114",
                            IsYes: "No",
                            IsTrue: "False",
                            IsAdult: "Child"));
        }
    }
}
