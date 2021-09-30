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
        private string cityTB = "";
        private string streetTB = "";
        private string numberTB = "";

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand ModifyCommand { get; set; }
        public string NameTB { get => nameTB; set { nameTB = value; OnPropertyChanged("NameTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string CityTB { get => cityTB; set { cityTB = value; OnPropertyChanged("CityTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string StreetTB { get => streetTB; set { streetTB = value; OnPropertyChanged("StreetTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string NumberTB { get => numberTB; set { numberTB = value; OnPropertyChanged("NumberTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }

        public PublishingHousesTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
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
                    CityTB = selectedPublishingHouse.ADR_PH.CITY;
                    streetTB = selectedPublishingHouse.ADR_PH.STREET;
                    numberTB = selectedPublishingHouse.ADR_PH.HOME_NUMBER;
                    OnPropertyChanged("StreetTB");
                    OnPropertyChanged("CityTB");
                    OnPropertyChanged("NumberTB");

                }
                DeleteCommand.RaiseCanExecuteChanged();
                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
            }
        }
        private bool CanModify()
        {
            return selectedPublishingHouse!=null && nameTB != "" && cityTB!=""&&streetTB!=""&&numberTB!="" && int.TryParse(numberTB, out int n);
        }

        private void OnModify()
        {
            int adrnum = -1;
            if (!(int.TryParse(NumberTB, out adrnum)))
            {
                System.Windows.MessageBox.Show("Address number must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.EditPublishingHouse(new PublishingHouse(selectedPublishingHouse.ID_PH, NameTB, new Common.Models.ADDRESS(adrnum.ToString(), cityTB, streetTB)));
            RefreshTable();
        }

        private bool CanAdd()
        {
            return (NameTB != "" && cityTB != "" && streetTB != "" && int.TryParse(numberTB, out int n));

        }

        private void OnAdd()
        {
            int adrnum = -1;
            if (!(int.TryParse(NumberTB, out adrnum)))
            {
                System.Windows.MessageBox.Show("Address number must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.AddPublishingHouse(new PublishingHouse(-1, NameTB, new Common.Models.ADDRESS(adrnum.ToString(), cityTB, streetTB)));
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
