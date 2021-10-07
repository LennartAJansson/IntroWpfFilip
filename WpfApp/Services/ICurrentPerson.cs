
using WpfApp.Events;
using WpfApp.Models;

namespace WpfApp.Services
{
    public interface ICurrentPerson
    {
        event PersonChangedDelegate PersonChanged;

        Person SelectedPerson { get; set; }
    }
}
