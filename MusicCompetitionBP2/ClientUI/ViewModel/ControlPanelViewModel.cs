using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClientUI.ViewModel
{
    public class ControlPanelViewModel:BindableBase
    {
        private string firstNameTB = "";
        private string lastNameTB = "";
        private string jmbgTB = "";
        private DateTime birthDP = DateTime.Now;
        private string emailTB = "";

        private string phoneNoTB = "";

        private string streetTB = "";
        private string numberTB = "";

        private string myPHTB = "-1";
        private bool isEventOrganizer = false;
        public bool IsEventOrganizer { get => isEventOrganizer; set { isEventOrganizer = value; OnPropertyChanged("IsEventOrganizer"); } }

        public List<string> CityStrings { get; set; } = new List<string>();
        private string selectedCity;
        private List<Common.Models.City> cities;

        public MyICommand<PasswordBox> ModifyCommand { get; set; }

        public ControlPanelViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();

            cities = repo.RepositoryProxy.ReadCities().ToList();
            foreach (Common.Models.City c in cities)
            {
                CityStrings.Add(c.Postcode + "-" + c.CityName);
            }

            ModifyCommand = new MyICommand<PasswordBox>(OnModify, CanModify);
            IsEventOrganizer = LoggedInUserSingleton.Instance.loggedInUser.Type == "EventOrganizer";

            if(LoggedInUserSingleton.Instance.loggedInUser.Type == "EventOrganizer")
            {
                Common.Models.EventOrganizer eotemp = repo.RepositoryProxy.ReadEventOrganizer(LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN);
                MyPHTB = eotemp.PublishingHouseID_PH.ToString();
            }

            LoadLoggedInUser();
        }




        public string FirstNameTB { get => firstNameTB; set { firstNameTB = value; OnPropertyChanged("FirstNameTB"); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string LastNameTB { get => lastNameTB; set { lastNameTB = value; OnPropertyChanged("LastNameTB"); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string JmbgTB { get => jmbgTB; set { jmbgTB = value; OnPropertyChanged("JmbgTB"); ModifyCommand.RaiseCanExecuteChanged(); } }
        public DateTime BirthDP { get => birthDP; set { birthDP = value; OnPropertyChanged("BirthDP"); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string EmailTB { get => emailTB; set { emailTB = value; OnPropertyChanged("EmailTB"); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string PhoneNoTB { get => phoneNoTB; set { phoneNoTB = value; OnPropertyChanged("PhoneNoTB"); ModifyCommand.RaiseCanExecuteChanged(); } }
       
        public string StreetTB { get => streetTB; set { streetTB = value; OnPropertyChanged("StreetTB"); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string NumberTB { get => numberTB; set { numberTB = value; OnPropertyChanged("NumberTB"); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string MyPHTB { get => myPHTB; set { myPHTB = value; OnPropertyChanged("MyPHTB");} }

        public string SelectedCity { get => selectedCity; set { selectedCity = value; OnPropertyChanged("SelectedCity");  ModifyCommand.RaiseCanExecuteChanged(); } }


        private bool CanModify(PasswordBox pb)
        {
            bool allRight = true;
            if(pb.Password == null)
            {
                return false;
            }

            if (!CityStrings.Contains(selectedCity))
            {
                allRight = false;
            }

            if (FirstNameTB == "" || lastNameTB == "" || birthDP == null || emailTB == "" || phoneNoTB == "" || streetTB == "" || !(int.TryParse(numberTB, out int n)) || BirthDP > DateTime.Now.AddYears(-10) || pb.Password.ToString()=="")
            {
                allRight = false;
            }

            return allRight;
        }



        private void OnModify(PasswordBox pb)
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();

            if (CanModify(pb))
            {
                string city = selectedCity.Split('-')[1];
                if (LoggedInUserSingleton.Instance.loggedInUser.Type == "Competitor")
                {
                    repo.RepositoryProxy.EditCompetitor(new Common.Models.Competitor(long.Parse(jmbgTB), firstNameTB, lastNameTB, birthDP, emailTB, phoneNoTB, new Common.Models.ADDRESS(numberTB, city, streetTB)));
                    Common.Models.User editSession;

                    editSession = repo.RepositoryProxy.ReadLoggedInUser(emailTB, LoggedInUserSingleton.Instance.loggedInUser.Password);
                    LoggedInUserSingleton.Instance.loggedInUser = editSession;
                }
                if (LoggedInUserSingleton.Instance.loggedInUser.Type == "JuryMember")
                {
                    repo.RepositoryProxy.EditJuryMember(new Common.Models.JuryMember(long.Parse(jmbgTB), firstNameTB, lastNameTB, birthDP, emailTB, phoneNoTB, new Common.Models.ADDRESS(numberTB, city, streetTB)));
                    Common.Models.User editSession;

                    editSession = repo.RepositoryProxy.ReadLoggedInUser(emailTB, LoggedInUserSingleton.Instance.loggedInUser.Password);
                    LoggedInUserSingleton.Instance.loggedInUser = editSession;
                }

                if(LoggedInUserSingleton.Instance.loggedInUser.Type == "EventOrganizer")
                {
                    Common.Models.EventOrganizer evtemp = repo.RepositoryProxy.ReadEventOrganizers().FirstOrDefault(t => t.JMBG_SIN == LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN);

                    repo.RepositoryProxy.EditEventOrganizer(new Common.Models.EventOrganizer(long.Parse(jmbgTB), firstNameTB, lastNameTB, birthDP, emailTB, phoneNoTB, new Common.Models.ADDRESS(numberTB, city, streetTB)) { PublishingHouseID_PH = evtemp.PublishingHouseID_PH });
                    Common.Models.User editSession;

                    editSession = repo.RepositoryProxy.ReadLoggedInUser(emailTB, LoggedInUserSingleton.Instance.loggedInUser.Password);
                    LoggedInUserSingleton.Instance.loggedInUser = editSession;
                }

                if(LoggedInUserSingleton.Instance.loggedInUser.Type == "Administrator")
                {
                    repo.RepositoryProxy.EditAdministrator(new Common.Models.Administrator(long.Parse(jmbgTB), firstNameTB, lastNameTB, birthDP, emailTB, phoneNoTB, new Common.Models.ADDRESS(numberTB, city, streetTB)));
                    Common.Models.User editSession;

                    editSession = repo.RepositoryProxy.ReadLoggedInUser(emailTB, LoggedInUserSingleton.Instance.loggedInUser.Password);
                    LoggedInUserSingleton.Instance.loggedInUser = editSession;
                }

               
                


                //ako treba promeni i password
                if (pb.Password.ToString() != "NoChange")
                {
                    if (pb.Password.ToString().Contains("-FL-"))
                    {
                        MessageBox.Show("Invalid password. Your password can't contain '-FL-'. Please try again.", "Invalid password", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    



                    Common.Models.User tempuser = new Common.Models.User(long.Parse(jmbgTB), firstNameTB, lastNameTB, birthDP, emailTB, phoneNoTB, new Common.Models.ADDRESS(numberTB, city, streetTB))
                    {
                        Password = Common.PasswordHasher.Hash(pb.Password.ToString(), 10),
                        Type = LoggedInUserSingleton.Instance.loggedInUser.Type
                    };
                    repo.RepositoryProxy.EditPassword(tempuser);

                    Common.Models.User loggedInUser;

                    loggedInUser = repo.RepositoryProxy.ReadLoggedInUser(tempuser.EMAIL_SIN, pb.Password.ToString());
                    LoggedInUserSingleton.Instance.loggedInUser = loggedInUser;
                    LoadLoggedInUser();


                }
                
            }
            else
            {
                System.Windows.MessageBox.Show("Wrong input!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            System.Windows.MessageBox.Show("Profile edited!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }





        public void LoadLoggedInUser()
        {
            LoggedInUserSingleton ls = LoggedInUserSingleton.Instance;
            Common.Models.User tempUser = ls.loggedInUser;
            if (tempUser != null)
            {
                FirstNameTB = tempUser.FIRSTNAME_SIN;
                LastNameTB = tempUser.LASTNAME_SIN;
                JmbgTB = tempUser.JMBG_SIN.ToString();
                BirthDP = tempUser.BIRTHDATE_SIN;
                EmailTB = tempUser.EMAIL_SIN;
                //AddressTB = selectedCompetitor.ADDRESS_SIN;
                PhoneNoTB = tempUser.PHONE_NO_SIN;
                StreetTB = tempUser.ADDRESS_SIN.STREET;
             
                NumberTB = tempUser.ADDRESS_SIN.HOME_NUMBER;

                foreach (Common.Models.City c in cities)
                {
                    if (tempUser.ADDRESS_SIN.CITY == c.CityName)
                    {
                        SelectedCity = c.Postcode + "-" + c.CityName;
                    }
                }


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
            //DeleteCommand.RaiseCanExecuteChanged();
            //AddCommand.RaiseCanExecuteChanged();
            //ModifyCommand.RaiseCanExecuteChanged();

        }



    }
}
