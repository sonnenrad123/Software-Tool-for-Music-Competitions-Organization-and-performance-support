using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientUI.ViewModel
{
    public class CompetitorsTableViewModel:BindableBase
    {
        public ObservableCollection<Common.Models.Competitor> Competitors { get; set; } = new ObservableCollection<Common.Models.Competitor>();

        private Common.Models.Competitor selectedCompetitor;
        private string firstNameTB = "";
        private string lastNameTB = "";
        private string jmbgTB = "";
        private DateTime birthDP = DateTime.Now;
        private string emailTB = "";
        
        private string phoneNoTB = "";
        private string cityTB = "";
        private string streetTB = "";
        private string numberTB = "";

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand ModifyCommand { get; set; }

        public CompetitorsTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Competitors = new ObservableCollection<Common.Models.Competitor>(repo.RepositoryProxy.ReadCompetitors());
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            ModifyCommand = new MyICommand(OnModify, CanModify);

        }

        private bool CanModify()
        {
            bool allRight = true;

            if (selectedCompetitor == null)
            {
                allRight = false;
                return false;
            }

            if (selectedCompetitor.JMBG_SIN.ToString() != jmbgTB)
            {
                allRight = false;
               
            }

            if (FirstNameTB == "" || lastNameTB == "" || birthDP == null || emailTB == "" || phoneNoTB == "" || cityTB == "" || streetTB == "" || !(int.TryParse(numberTB, out int n)) || BirthDP > DateTime.Now.AddYears(-10))
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
                repo.RepositoryProxy.EditCompetitor(new Common.Models.Competitor(selectedCompetitor.JMBG_SIN, firstNameTB, lastNameTB, birthDP, emailTB, phoneNoTB, new Common.Models.ADDRESS(numberTB, cityTB, streetTB)));
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
            return (long.TryParse(JmbgTB,out long x) && FirstNameTB != "" && lastNameTB != "" && birthDP != null && emailTB != "" && phoneNoTB != "" && cityTB != "" && streetTB != "" && int.TryParse(numberTB,out int n) && BirthDP < DateTime.Now.AddYears(-10));
            
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
            if(!(long.TryParse(JmbgTB,out jmbg))){
                System.Windows.MessageBox.Show("JMBG must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                var temp = Competitors.FirstOrDefault((x) => x.JMBG_SIN == jmbg);
                if (temp != null)
                {
                    System.Windows.MessageBox.Show("JMBG exists in database!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }

            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.AddCompetitor(new Common.Models.Competitor(jmbg, FirstNameTB, lastNameTB, birthDP, emailTB, phoneNoTB, new Common.Models.ADDRESS(adrnum.ToString(), cityTB, streetTB)));
            RefreshTable();
        }

        private bool CanDelete()
        {
            return SelectedCompetitor != null;
        }

        private void OnDelete()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.DeleteCompetitor(selectedCompetitor.JMBG_SIN);
            RefreshTable();
        }

        public Common.Models.Competitor SelectedCompetitor
        {
            get
            {
                return selectedCompetitor;
            }
            set
            {
                selectedCompetitor = value;
                if(SelectedCompetitor != null)
                {
                    FirstNameTB = selectedCompetitor.FIRSTNAME_SIN;
                    LastNameTB = selectedCompetitor.LASTNAME_SIN;
                    JmbgTB = selectedCompetitor.JMBG_SIN.ToString();
                    BirthDP = selectedCompetitor.BIRTHDATE_SIN;
                    EmailTB = selectedCompetitor.EMAIL_SIN;
                    //AddressTB = selectedCompetitor.ADDRESS_SIN;
                    PhoneNoTB = selectedCompetitor.PHONE_NO_SIN;
                    StreetTB = selectedCompetitor.ADDRESS_SIN.STREET;
                    CityTB = selectedCompetitor.ADDRESS_SIN.CITY;
                    NumberTB = selectedCompetitor.ADDRESS_SIN.HOME_NUMBER;
                  
                    OnPropertyChanged("FirstNameTB");
                    OnPropertyChanged("LastNameTB");
                    OnPropertyChanged("JmbgTB");
                    OnPropertyChanged("BirthDP");
                    OnPropertyChanged("EmailTB");
                    OnPropertyChanged("PhoneNoTB");
                    OnPropertyChanged("StreetTB");
                    OnPropertyChanged("CityTB");
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


        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Competitors = new ObservableCollection<Common.Models.Competitor>(repo.RepositoryProxy.ReadCompetitors());
            OnPropertyChanged("Competitors");
        }
    }
}
