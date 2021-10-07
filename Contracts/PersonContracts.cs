using System.Collections.Generic;

namespace Contracts
{
    //Requests
    public record AddPersonRequest(string Ssn, string Firstname, string Lastname, string Address, string Postalcode, string City, string Telephone, string Email, string IsYes, string IsTrue, string IsAdult);
    //public record GetPersonContractRequest(); ById
    //public record GetPeopleRequest(); //Filterfunctionality? Not possible with Get since it doesn't allow Body
    public record UpdatePersonRequest(int Id, string Ssn, string Firstname, string Lastname, string Address, string Postalcode, string City, string Telephone, string Email, string IsYes, string IsTrue, string IsAdult);
    public record DeletePersonRequest(int Id, string Ssn, string Firstname, string Lastname, string Address, string Postalcode, string City, string Telephone, string Email, string IsYes, string IsTrue, string IsAdult);

    //Responses
    public record AddPersonResponse(int Id, string Ssn, string Firstname, string Lastname, string Address, string Postalcode, string City, string Telephone, string Email, string IsYes, string IsTrue, string IsAdult);
    public record GetPersonResponse(int Id, string Ssn, string Firstname, string Lastname, string Address, string Postalcode, string City, string Telephone, string Email, string IsYes, string IsTrue, string IsAdult);
    public record GetPeopleResponse(IEnumerable<GetPersonResponse> People);
    public record UpdatePersonResponse(int Id, string Ssn, string Firstname, string Lastname, string Address, string Postalcode, string City, string Telephone, string Email, string IsYes, string IsTrue, string IsAdult);
    public record DeletePersonResponse(int Id, string Ssn, string Firstname, string Lastname, string Address, string Postalcode, string City, string Telephone, string Email, string IsYes, string IsTrue, string IsAdult);

    //public record Person(int Id, string Ssn, string Firstname, string Lastname, string Address, string Postalcode, string City, string Telephone, string Email, string IsYes, string IsTrue, string IsAdult);
}
