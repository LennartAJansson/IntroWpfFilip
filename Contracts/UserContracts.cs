using System.Collections.Generic;

namespace Contracts
{
    //Requests
    public record LoginUserRequest(string Email, string Password);
    public record LogoutUserRequest(string Email);
    public record RefreshUserRequest(string AccessToken);
    //public record AddUserRequest(string Username, string Email, string Role, string AccessToken);
    public record GetUserContractRequest();
    public record GetUsersRequest(); //Filterfunctionality? Not possible with Get since it doesn't allow Body
    //public record UpdateUserRequest(int Id, string Username, string Email, string Role, string AccessToken);
    //public record DeleteUserRequest(int Id, string Username, string Email, string Role, string AccessToken);

    //Responses
    public record ValidUserResponse(string AccessToken);
    public record LogoutUserResponse(string Username, string Email);
    //public record AddUserResponse(int Id, string Username, string Email, string Role, string AccessToken);
    public record GetUserResponse(int Id, string Username, string Email, string Role);
    public record GetUsersResponse(IEnumerable<GetUserResponse> Users);
    //public record UpdateUserResponse(int Id, string Username, string Email, string Role, string AccessToken);
    //public record DeleteUserResponse(int Id, string Username, string Email, string Role, string AccessToken);

    //public record User(int Id, string Username, string Email, string Role, string AccessToken);
}
