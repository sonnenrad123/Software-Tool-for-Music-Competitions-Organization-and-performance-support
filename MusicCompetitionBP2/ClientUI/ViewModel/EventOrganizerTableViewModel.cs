using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientUI.ViewModel
{
    public class EventOrganizerTableViewModel:BindableBase
    {
        public ObservableCollection<Common.Models.EventOrganizer> EventOrganizers { get; set; } = new ObservableCollection<Common.Models.EventOrganizer>();


        private Common.Models.EventOrganizer selectedEventOrganizer;

        private string firstNameTB = "";
        private string lastNameTB = "";
        private string jmbgTB = "";
        private DateTime birthDP = DateTime.Now;
        private string emailTB = "";

        private string phoneNoTB = "";
        private string cityTB = "";
        private string streetTB = "";
        private string numberTB = "";

        private string selectedPublishingHouse = "";
        public List<string> PublishingHouseStrings { get; set; } = new List<string>();
        public List<Common.Models.PublishingHouse> PublishingHouses;

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand ModifyCommand { get; set; }

        public EventOrganizerTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            EventOrganizers = new ObservableCollection<Common.Models.EventOrganizer>(repo.RepositoryProxy.ReadEventOrganizers());
            PublishingHouses = repo.RepositoryProxy.ReadPublishingHouses().ToList();
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            ModifyCommand = new MyICommand(OnModify, CanModify);


            foreach(Common.Models.PublishingHouse phtemp in PublishingHouses)
            {
                PublishingHouseStrings.Add(phtemp.NAME_PH);
            }

            OnPropertyChanged("PublishingHouseStrings");

        }

        public string SelectedPublishingHouse { get => selectedPublishingHouse; set { selectedPublishingHouse = value; OnPropertyChanged("SelectedPublishingHouse"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }



        private bool CanModify()
        {
            bool allRight = true;

            if (selectedEventOrganizer == null)
            {
                allRight = false;
                return false;
            }

            if (selectedEventOrganizer.JMBG_SIN.ToString() != jmbgTB)
            {
                allRight = false;

            }

            if (BirthDP > DateTime.Now.AddYears(-10))
            {
                allRight = false;
            }

            if (FirstNameTB == "" || lastNameTB == "" || birthDP == null && emailTB == "" || phoneNoTB == "" || cityTB == "" || streetTB == "" || !int.TryParse(numberTB, out int n))
            {
                allRight = false;
            }

            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Common.Models.PublishingHouse phtemp = repo.RepositoryProxy.ReadPublishingHouses().FirstOrDefault(x => x.NAME_PH == selectedPublishingHouse);
            if (phtemp == null)
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
                Common.Models.PublishingHouse phtemp = PublishingHouses.FirstOrDefault(t => t.NAME_PH == selectedPublishingHouse);
                if (phtemp == null)
                {
                    System.Windows.MessageBox.Show("Non-existent publishing house!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                repo.RepositoryProxy.EditEventOrganizer(new Common.Models.EventOrganizer(selectedEventOrganizer.JMBG_SIN, firstNameTB, lastNameTB, birthDP, emailTB, phoneNoTB, new Common.Models.ADDRESS(numberTB, cityTB, streetTB)) { PublishingHouseID_PH = phtemp.ID_PH});
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


            Common.Models.PublishingHouse phtemp = PublishingHouses.FirstOrDefault(t => t.NAME_PH == selectedPublishingHouse);
            if (phtemp == null)
            {
                return false;
            }


            return (long.TryParse(JmbgTB, out long x) && FirstNameTB != "" && lastNameTB != "" && birthDP != null && emailTB != "" && phoneNoTB != "" && cityTB != "" && streetTB != "" && int.TryParse(numberTB, out int n) && BirthDP < DateTime.Now.AddYears(-10));

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
                var temp = EventOrganizers.FirstOrDefault((x) => x.JMBG_SIN == jmbg);
                if (temp != null)
                {
                    System.Windows.MessageBox.Show("JMBG exists in database!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }

            Common.Models.PublishingHouse phtemp = PublishingHouses.FirstOrDefault(t => t.NAME_PH == selectedPublishingHouse);
            if (phtemp == null)
            {
                System.Windows.MessageBox.Show("Non-existent publishing house!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.AddEventOrganizer(new Common.Models.EventOrganizer(jmbg, FirstNameTB, lastNameTB, birthDP, emailTB, phoneNoTB, new Common.Models.ADDRESS(adrnum.ToString(), cityTB, streetTB)) { PublishingHouseID_PH = phtemp.ID_PH });
            RefreshTable();
        }

        private bool CanDelete()
        {
            return SelectedEventOrganizer != null;
        }

        private void OnDelete()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.DeleteEventOrganizer(selectedEventOrganizer.JMBG_SIN);
            RefreshTable();
        }






        public Common.Models.EventOrganizer SelectedEventOrganizer
        {
            get
            {
                return selectedEventOrganizer;
            }
            set
            {
                selectedEventOrganizer = value;
                if (selectedEventOrganizer != null)
                {
                    FirstNameTB = selectedEventOrganizer.FIRSTNAME_SIN;
                    LastNameTB = selectedEventOrganizer.LASTNAME_SIN;
                    JmbgTB = selectedEventOrganizer.JMBG_SIN.ToString();
                    BirthDP = selectedEventOrganizer.BIRTHDATE_SIN;
                    EmailTB = selectedEventOrganizer.EMAIL_SIN;
                    //AddressTB = selectedJuryMember.ADDRESS_SIN;
                    PhoneNoTB = selectedEventOrganizer.PHONE_NO_SIN;
                    StreetTB = selectedEventOrganizer.ADDRESS_SIN.STREET;
                    CityTB = selectedEventOrganizer.ADDRESS_SIN.CITY;
                    NumberTB = selectedEventOrganizer.ADDRESS_SIN.HOME_NUMBER;
                    SelectedPublishingHouse = selectedEventOrganizer.PublishingHouse.NAME_PH;

                    OnPropertyChanged("FirstNameTB");
                    OnPropertyChanged("LastNameTB");
                    OnPropertyChanged("JmbgTB");
                    OnPropertyChanged("BirthDP");
                    OnPropertyChanged("EmailTB");
                    OnPropertyChanged("PhoneNoTB");
                    OnPropertyChanged("StreetTB");
                    OnPropertyChanged("CityTB");
                    OnPropertyChanged("NumberTB");
                    OnPropertyChanged("SelectedPublishingHouse");
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
        
        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            EventOrganizers = new ObservableCollection<Common.Models.EventOrganizer>(repo.RepositoryProxy.ReadEventOrganizers());
            OnPropertyChanged("EventOrganizers");
        }
    }
}
