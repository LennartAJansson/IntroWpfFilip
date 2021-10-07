
using System.Collections.Generic;
using System.Threading.Tasks;

using WpfApp.Models;

namespace WpfApp.Services
{
    public interface IPeopleService
    {
        Task<IEnumerable<Person>> GetPeopleAsync();

        Task<Person> GetPersonAsync(object id);
    }
}