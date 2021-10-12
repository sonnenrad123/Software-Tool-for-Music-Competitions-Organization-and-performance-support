using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Common.Models;

namespace ClientUI.ViewModel
{
    public class JuryMembersTableViewModel:BindableBase
    {
        public ObservableCollection<Common.Models.JuryMember> JuryMembers { get; set; } = new ObservableCollection<Common.Models.JuryMember>();
        

        private Common.Models.JuryMember selectedJuryMember;

        private string firstNameTB = "";
        private string lastNameTB = "";
        private string jmbgTB = "";
        private DateTime birthDP = DateTime.Now;
        private string emailTB = "";

        private string phoneNoTB = "";
        private string cityTB = "";
        private string streetTB = "";
        private string numberTB = "";

        public List<string> CityStrings { get; set; } = new List<string>();
        private string selectedCity;
        private List<City> cities;

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand ModifyCommand { get; set; }

        public JuryMembersTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            cities = repo.RepositoryProxy.ReadCities().ToList();
            foreach(City c in cities)
            {
                CityStrings.Add(c.Postcode + "-" + c.CityName);
            }


            JuryMembers = new ObservableCollection<Common.Models.JuryMember>(repo.RepositoryProxy.ReadJuryMembers());
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            ModifyCommand = new MyICommand(OnModify, CanModify);
        }

        private bool CanModify()
        {
            bool allRight = true;

            if (selectedJuryMember == null)
            {
                allRight = false;
                return false;
            }

            if (selectedJuryMember.JMBG_SIN.ToString() != jmbgTB)
            {
                allRight = false;

            }

            if (!CityStrings.Contains(selectedCity)){
                allRight = false;
            }

            if(BirthDP > DateTime.Now.AddYears(-10))
            {
                allRight = false;
            }

            if (FirstNameTB == "" || lastNameTB == "" || birthDP == null && emailTB == "" || phoneNoTB == "" || streetTB == "" || !int.TryParse(numberTB, out int n))
            {
                allRight = false;
            }

            return allRight;
        }

        private void OnModify()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();

            if (CanModify())
            {
                string city = selectedCity.Split('-')[1];

                repo.RepositoryProxy.EditJuryMember(new Common.Models.JuryMember(selectedJuryMember.JMBG_SIN, firstNameTB, lastNameTB, birthDP, emailTB, phoneNoTB, new Common.Models.ADDRESS(numberTB, city, streetTB)));
                RefreshTable();
            }
            else
            {
                System.Windows.MessageBox.Show("Wrong input!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private bool CanAdd()
        {

            if (!CityStrings.Contains(selectedCity))
            {
                return false;
            }

            return (long.TryParse(JmbgTB, out long x) && FirstNameTB != "" && lastNameTB != "" && birthDP != null && emailTB != "" && phoneNoTB != "" && streetTB != "" && int.TryParse(numberTB, out int n) && BirthDP < DateTime.Now.AddYears(-10));

        }

        private void OnAdd()
        {
            int adrnum = -1;
            if (!(int.TryParse(NumberTB, out adrnum)))
            {
                System.Windows.MessageBox.Show("Address number must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            long jmbg = -1;
            if (!(long.TryParse(JmbgTB, out jmbg)))
            {
                System.Windows.MessageBox.Show("JMBG must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                var temp = JuryMembers.FirstOrDefault((x) => x.JMBG_SIN == jmbg);
                if (temp != null)
                {
                    System.Windows.MessageBox.Show("JMBG exists in database!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
            string city = selectedCity.Split('-')[1];
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            if(repo.RepositoryProxy.AddJuryMember(new Common.Models.JuryMember(jmbg, FirstNameTB, lastNameTB, birthDP, emailTB, phoneNoTB, new Common.Models.ADDRESS(adrnum.ToString(), city, streetTB))))
            {
                RefreshTable();
            }
            else
            {
                System.Windows.MessageBox.Show("Provided email address already exists in database!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private bool CanDelete()
        {
            return SelectedJuryMember != null;
        }

        private void OnDelete()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.DeleteJuryMember(selectedJuryMember.JMBG_SIN);
            RefreshTable();
        }






        public Common.Models.JuryMember SelectedJuryMember
        {
            get
            {
                return selectedJuryMember;
            }
            set
            {
                selectedJuryMember = value;
                if (selectedJuryMember != null)
                {
                    FirstNameTB = selectedJuryMember.FIRSTNAME_SIN;
                    LastNameTB = selectedJuryMember.LASTNAME_SIN;
                    JmbgTB = selectedJuryMember.JMBG_SIN.ToString();
                    BirthDP = selectedJuryMember.BIRTHDATE_SIN;
                    EmailTB = selectedJuryMember.EMAIL_SIN;
                    //AddressTB = selectedJuryMember.ADDRESS_SIN;
                    PhoneNoTB = selectedJuryMember.PHONE_NO_SIN;
                    StreetTB = selectedJuryMember.ADDRESS_SIN.STREET;

                    foreach(City c in cities)
                    {
                        if(selectedJuryMember.ADDRESS_SIN.CITY == c.CityName)
                        {
                            SelectedCity = c.Postcode + "-" + c.CityName;
                        }
                    }

                    CityTB = selectedJuryMember.ADDRESS_SIN.CITY;
                    NumberTB = selectedJuryMember.ADDRESS_SIN.HOME_NUMBER;

                    OnPropertyChanged("FirstNameTB");
                    OnPropertyChanged("LastNameTB");
                    OnPropertyChanged("JmbgTB");
                    OnPropertyChanged("BirthDP");
                    OnPropertyChanged("EmailTB");
                    OnPropertyChanged("PhoneNoTB");
                    OnPropertyChanged("StreetTB");
                    OnPropertyChanged("SelectedCity");
                    OnPropertyChanged("NumberTB");
                }
                DeleteCommand.RaiseCanExecuteChanged();
                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
            }
        }
        public string FirstNameTB { get => firstNameTB; set { firstNameTB = value; OnPropertyChanged("FirstNameTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string LastNameTB { get => lastNameTB; set { lastNameTB = value; OnPropertyChanged("LastNameTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string JmbgTB { get => jmbgTB; set { jmbgTB = value; OnPropertyChanged("JmbgTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public DateTime BirthDP { get => birthDP; set { birthDP = value; OnPropertyChanged("BirthDP"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string EmailTB { get => emailTB; set { emailTB = value; OnPropertyChanged("EmailTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string PhoneNoTB { get => phoneNoTB; set { phoneNoTB = value; OnPropertyChanged("PhoneNoTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string CityTB { get => cityTB; set { cityTB = value; OnPropertyChanged("CityTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string StreetTB { get => streetTB; set { streetTB = value; OnPropertyChanged("StreetTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string NumberTB { get => numberTB; set { numberTB = value; OnPropertyChanged("NumberTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }

        public string SelectedCity { get => selectedCity; set { selectedCity = value; OnPropertyChanged("SelectedCity"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }

        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            JuryMembers = new ObservableCollection<Common.Models.JuryMember>(repo.RepositoryProxy.ReadJuryMembers());
            OnPropertyChanged("JuryMembers");
        }
    }
}
