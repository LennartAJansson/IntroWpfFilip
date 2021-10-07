using WpfApp.Events;
using WpfApp.Models;

namespace WpfApp.Services
{

    public class CurrentPerson : ICurrentPerson
    {
        public event PersonChangedDelegate PersonChanged;

        public Person SelectedPerson
        {
            get => person;
            set
            {
                person = value;
                PersonChanged?.Invoke(value);
            }
        }

        private Person person;

        public CurrentPerson()
        {
        }
    }
}
