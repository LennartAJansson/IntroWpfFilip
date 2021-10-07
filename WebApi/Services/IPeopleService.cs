
using Contracts;

using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IPeopleService
    {
        Task<GetPeopleResponse> GetPeopleAsync();

        Task<GetPersonResponse> GetPersonAsync(object id);
    }
}
