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
    public class PublishingHousesTableViewModel:BindableBase
    {
        public  ObservableCollection<Common.Models.PublishingHouse> PublishingHouses { get; set; } = new ObservableCollection<Common.Models.PublishingHouse>();
       

        private Common.Models.PublishingHouse selectedPublishingHouse;
        private string nameTB = "";
       
        private string streetTB = "";
        private string numberTB = "";

        public List<string> CityStrings { get; set; } = new List<string>();
        private string selectedCity;
        private List<Common.Models.City> cities;


        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand ModifyCommand { get; set; }
        public string NameTB { get => nameTB; set { nameTB = value; OnPropertyChanged("NameTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        
        public string StreetTB { get => streetTB; set { streetTB = value; OnPropertyChanged("StreetTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string NumberTB { get => numberTB; set { numberTB = value; OnPropertyChanged("NumberTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string SelectedCity { get => selectedCity; set { selectedCity = value; OnPropertyChanged("SelectedCity"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }


        public PublishingHousesTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();

            cities = repo.RepositoryProxy.ReadCities().ToList();
            foreach (Common.Models.City c in cities)
            {
                CityStrings.Add(c.Postcode + "-" + c.CityName);
            }

            PublishingHouses = new ObservableCollection<PublishingHouse>(repo.RepositoryProxy.ReadPublishingHouses());
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            ModifyCommand = new MyICommand(OnModify, CanModify);
        }


        public Common.Models.PublishingHouse SelectedPublishingHouse
        {
            get
            {
                return selectedPublishingHouse;
            }
            set
            {
                selectedPublishingHouse = value;
                if (SelectedPublishingHouse != null)
                {
                    NameTB = selectedPublishingHouse.NAME_PH;
                    OnPropertyChanged("NameTB");
                    foreach (Common.Models.City c in cities)
                    {
                        if (selectedPublishingHouse.ADR_PH.CITY == c.CityName)
                        {
                            SelectedCity = c.Postcode + "-" + c.CityName;
                        }
                    }
                    streetTB = selectedPublishingHouse.ADR_PH.STREET;
                    numberTB = selectedPublishingHouse.ADR_PH.HOME_NUMBER;
                    OnPropertyChanged("StreetTB");
                    OnPropertyChanged("SelectedCity");
                    OnPropertyChanged("NumberTB");

                }
                DeleteCommand.RaiseCanExecuteChanged();
                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
            }
        }
        private bool CanModify()
        {
            if (!CityStrings.Contains(selectedCity))
            {
                return false;
            }

            return selectedPublishingHouse!=null && nameTB != "" && streetTB!=""&&numberTB!="" && int.TryParse(numberTB, out int n);
        }

        private void OnModify()
        {
            int adrnum = -1;
            if (!(int.TryParse(NumberTB, out adrnum)))
            {
                System.Windows.MessageBox.Show("Address number must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string city = selectedCity.Split('-')[1];

            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.EditPublishingHouse(new PublishingHouse(selectedPublishingHouse.ID_PH, NameTB, new Common.Models.ADDRESS(adrnum.ToString(), city, streetTB)));
            RefreshTable();
        }

        private bool CanAdd()
        {

            if (!CityStrings.Contains(selectedCity))
            {
                return false;
            }

            return (NameTB != "" &&  streetTB != "" && int.TryParse(numberTB, out int n));

        }

        private void OnAdd()
        {
            int adrnum = -1;
            if (!(int.TryParse(NumberTB, out adrnum)))
            {
                System.Windows.MessageBox.Show("Address number must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string city = selectedCity.Split('-')[1];
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.AddPublishingHouse(new PublishingHouse(-1, NameTB, new Common.Models.ADDRESS(adrnum.ToString(), city, streetTB)));
            RefreshTable();
        }

        private bool CanDelete()
        {
            return SelectedPublishingHouse != null;
        }

        private void OnDelete()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.DeletePublishingHouse(selectedPublishingHouse.ID_PH);
            RefreshTable();
        }

        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            PublishingHouses = new ObservableCollection<Common.Models.PublishingHouse>(repo.RepositoryProxy.ReadPublishingHouses());
            OnPropertyChanged("PublishingHouses");
        }
    }
}
