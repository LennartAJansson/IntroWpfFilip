using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.ComponentModel;

using Nito.AsyncEx.Synchronous;

using System;
using System.Collections.Generic;

using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public class PeopleViewModel : ObservableObject
    {
        public IEnumerable<Person> People
        {
            get { return people; }
            set { SetProperty(ref people, value); }
        }

        private IEnumerable<Person> people;

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set { SetProperty(ref selectedPerson, value); currentPerson.SelectedPerson = value; }
        }

        private Person selectedPerson;

        private readonly ILogger<MainViewModel> logger;
        private readonly IPeopleService service;
        private readonly ICurrentPerson currentPerson;

        public PeopleViewModel(ILogger<MainViewModel> logger, IPeopleService service, ICurrentPerson currentPerson)
        {
            this.logger = logger;
            this.service = service;
            this.currentPerson = currentPerson;
            GetPeople();
        }

        private void GetPeople()
        {
            logger.LogDebug("Getting people");
            try
            {
                var task = service.GetPeopleAsync();
                var people = task.WaitAndUnwrapException();
                People = people;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting people ");
            }
        }
    }
}