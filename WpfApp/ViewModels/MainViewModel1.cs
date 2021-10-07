namespace WpfApp.ViewModels
{
    //public class MainViewModel1 : ObservableObject
    //{
    //    private readonly ILogger<MainViewModel1> logger;
    //    private readonly IPersonService personService;

    //    public IEnumerable<PersonContract> People
    //    {
    //        get => people;
    //        set => SetProperty(ref people, value);
    //    }

    //    private IEnumerable<PersonContract> people;

    //    public PersonContract SelectedPerson
    //    {
    //        get => selectedPerson;
    //        set => SetProperty(ref selectedPerson, value);
    //    }

    //    private PersonContract selectedPerson;

    //    public Task MyTask
    //    {
    //        get => myTask;
    //        private set => SetPropertyAndNotifyOnCompletion(ref myTask, value);
    //    }

    //    private TaskNotifier myTask;

    //    public RelayCommand GetCommand { get; }

    //    public MainViewModel1(ILogger<MainViewModel1> logger, IPersonService personService)
    //    {
    //        this.logger = logger;
    //        this.personService = personService;
    //        GetCommand = new RelayCommand(async () => await GetAllAsync());

    //        //TODO Maybe use messaging instead to initiate the ViewModels?
    //        GetCommand.Execute(null);
    //    }

    //    public async Task GetAllAsync()
    //    {
    //        logger.LogInformation("Calling personService");
    //        People = await personService.GetAllPeopleAsync();

    //        //Person person = await personService.GetPersonAsync(1);
    //    }

    //    public void ReloadTask()
    //    {
    //        logger.LogDebug("Calling ReloadTask");
    //        MyTask = Task.Delay(3000);
    //    }
    //}
}