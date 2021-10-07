using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.ComponentModel;

using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public class PersonViewModel : ObservableObject
    {
        public Person Person { get => person; set => SetProperty(ref person, value); }

        private Person person;

        private readonly ILogger<MainViewModel> logger;
        private readonly ICurrentPerson currentPerson;

        public PersonViewModel(ILogger<MainViewModel> logger, ICurrentPerson currentPerson)
        {
            this.logger = logger;
            this.currentPerson = currentPerson;
            Person = currentPerson.SelectedPerson;
            this.currentPerson.PersonChanged += CurrentPerson_PersonChanged;
        }

        private void CurrentPerson_PersonChanged(Person person)
        {
            logger.LogDebug("Person has changed");
            Person = person;
        }
    }
}