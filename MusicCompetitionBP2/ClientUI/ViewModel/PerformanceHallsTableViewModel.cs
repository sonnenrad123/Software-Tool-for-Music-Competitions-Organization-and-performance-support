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
    public class PerformanceHallsTableViewModel:BindableBase
    {
        public ObservableCollection<Common.Models.PerformanceHall> PerformanceHalls { get; set; } = new ObservableCollection<Common.Models.PerformanceHall>();
   

        private Common.Models.PerformanceHall selectedPerformanceHall;
        private string nameTB = "";
        private string capacityTB = "";



        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand ModifyCommand { get; set; }
        public PerformanceHallsTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            PerformanceHalls = new ObservableCollection<PerformanceHall>(repo.RepositoryProxy.ReadPerformanceHalls());
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            ModifyCommand = new MyICommand(OnModify, CanModify);
        }

        public Common.Models.PerformanceHall SelectedPerformanceHall
        {
            get
            {
                return selectedPerformanceHall;
            }
            set
            {
                selectedPerformanceHall = value;
                if (SelectedPerformanceHall != null)
                {
                    NameTB = selectedPerformanceHall.NAME_HALL;
                    CapacityTB = selectedPerformanceHall.CAPACITY.ToString();
                    OnPropertyChanged("NameTB");
                    OnPropertyChanged("CapacityTB");

                }
                DeleteCommand.RaiseCanExecuteChanged();
                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanModify()
        {
            return  selectedPerformanceHall!=null && nameTB != "" && (int.TryParse(CapacityTB, out int capacity));
        }

        private void OnModify()
        {
            if(selectedPerformanceHall == null)
            {
                return;
            }
            int capacity = -1;
            if (!(int.TryParse(CapacityTB, out capacity)))
            {
                System.Windows.MessageBox.Show("Capacity must be a number! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.EditPerformanceHall(new PerformanceHall(selectedPerformanceHall.ID_HALL, NameTB, capacity));
            RefreshTable();
        }

        private bool CanAdd()
        {
            return (NameTB != "" && capacityTB != "");

        }

        private void OnAdd()
        {
            int capacity = -1;
            if(!(int.TryParse(CapacityTB,out capacity)))
            {
                System.Windows.MessageBox.Show("Capacity must be a number! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.AddPerformanceHall(new PerformanceHall(-1, NameTB, capacity));
            RefreshTable();
        }

        private bool CanDelete()
        {
            return SelectedPerformanceHall != null;
        }

        private void OnDelete()
        {
           if(selectedPerformanceHall != null)
            {
                RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
                repo.RepositoryProxy.DeletePerformanceHall(SelectedPerformanceHall.ID_HALL);
                RefreshTable();
            }
        }

        

        public string NameTB { get => nameTB; set { nameTB = value; AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string CapacityTB { get => capacityTB; set { capacityTB = value; AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            PerformanceHalls = new ObservableCollection<Common.Models.PerformanceHall>(repo.RepositoryProxy.ReadPerformanceHalls());
            OnPropertyChanged("PerformanceHalls");
        }
    }
}
