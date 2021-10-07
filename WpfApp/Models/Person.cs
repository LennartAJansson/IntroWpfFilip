using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace WpfApp.Models
{
    public class Person : ObservableObject
    {
        public int Id { get => id; set => SetProperty(ref id, value); }
        private int id;

        public string Ssn { get => ssn; set => SetProperty(ref ssn, value); }
        private string ssn;

        public string Firstname { get => firstname; set => SetProperty(ref firstname, value); }
        private string firstname;

        public string Lastname { get => lastname; set => SetProperty(ref lastname, value); }
        private string lastname;

        public string Address { get => address; set => SetProperty(ref address, value); }
        private string address;

        public string Postalcode { get => postalcode; set => SetProperty(ref postalcode, value); }
        private string postalcode;

        public string City { get => city; set => SetProperty(ref city, value); }
        private string city;

        public string Telephone { get => telephone; set => SetProperty(ref telephone, value); }
        private string telephone;

        public string Email { get => email; set => SetProperty(ref email, value); }
        private string email;

        public string IsYes { get => isYes; set => SetProperty(ref isYes, value); }
        private string isYes = "No";

        public string IsTrue { get => isTrue; set => SetProperty(ref isTrue, value); }
        private string isTrue = "False";

        public string IsAdult { get => isAdult; set => SetProperty(ref isAdult, value); }
        private string isAdult = "Child";
    }
}
