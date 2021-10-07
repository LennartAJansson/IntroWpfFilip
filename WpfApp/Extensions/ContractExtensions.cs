using Contracts;

using Security;

using System.Collections.Generic;
using System.Linq;

using WpfApp.Models;

namespace WpfApp.Extensions
{
    public static class ContractExtensions
    {
        public static IEnumerable<Person> ToPeople(this GetPeopleResponse getPeopleResponse) => getPeopleResponse.People.Select(getPersonResponse => new Person
        {
            Id = getPersonResponse.Id,
            Ssn = getPersonResponse.Ssn,
            Firstname = getPersonResponse.Firstname,
            Lastname = getPersonResponse.Lastname,
            Address = getPersonResponse.Address,
            Postalcode = getPersonResponse.Postalcode,
            City = getPersonResponse.City,
            Email = getPersonResponse.Email,
            Telephone = getPersonResponse.Telephone,
            IsAdult = getPersonResponse.IsAdult,
            IsTrue = getPersonResponse.IsTrue,
            IsYes = getPersonResponse.IsYes
        });

        public static Person ToPerson(this GetPersonResponse getPersonResponse) => new()
        {
            Id = getPersonResponse.Id,
            Ssn = getPersonResponse.Ssn,
            Firstname = getPersonResponse.Firstname,
            Lastname = getPersonResponse.Lastname,
            Address = getPersonResponse.Address,
            Postalcode = getPersonResponse.Postalcode,
            City = getPersonResponse.City,
            Email = getPersonResponse.Email,
            Telephone = getPersonResponse.Telephone,
            IsAdult = getPersonResponse.IsAdult,
            IsTrue = getPersonResponse.IsTrue,
            IsYes = getPersonResponse.IsYes
        };

        public static User ToUser(this ValidUserResponse validUserResponse)
        {
            UserJwtToken token = new(validUserResponse.AccessToken);

            return new()
            {
                Id = int.Parse(token.UserId),
                Username = token.Username,
                Email = token.Email,
                Role = token.Role,
                JwtToken = token
            };
        }
    }
}
