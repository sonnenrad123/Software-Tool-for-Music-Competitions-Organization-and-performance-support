using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
namespace ClientUI.ViewModel
{
    public class ReservationsTableViewModel:BindableBase
    {
        public ObservableCollection<Common.Models.Reserve> Reservations { get; set; } = new ObservableCollection<Common.Models.Reserve>();
        private Common.Models.Reserve selectedReservation;
        private string selectedCompetition = "";
        private string selectedPublishingHouse = "";
        private string selectedPerformanceHall = "";
        private string startTimeHoursTB = "";
        private string startTimeMinutesTB = "";
        private string endTimeHoursTB = "";
        private string endTimeMinutesTB = "";
        DateTime reservationDP = DateTime.Now;

        public List<string> PublishingHouseStrings { get; set; } = new List<string>();
        public List<string> CompetitionStrings { get; set; } = new List<string>();
        public List<string> PerformanceHallStrings { get; set; } = new List<string>();

        public List<Common.Models.PublishingHouse> PublishingHouses;
        public List<Common.Models.Competition> Competitions;
        public List<Common.Models.PerformanceHall> PerformanceHalls;
        public List<Common.Models.Organize> Organizations;

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand ModifyCommand { get; set; }

        public ReservationsTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Reservations = new ObservableCollection<Common.Models.Reserve>(repo.RepositoryProxy.ReadHallReservations());
            PerformanceHalls = repo.RepositoryProxy.ReadPerformanceHalls().ToList();
            Competitions = repo.RepositoryProxy.ReadCompetitions().ToList();
            PublishingHouses = repo.RepositoryProxy.ReadPublishingHouses().ToList();
            Organizations = repo.RepositoryProxy.ReadOrganizations().ToList();

            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            ModifyCommand = new MyICommand(OnModify, CanModify);

            foreach (Common.Models.PublishingHouse ph in PublishingHouses)
            {
                PublishingHouseStrings.Add(ph.ID_PH.ToString());
            }

            foreach(Common.Models.PerformanceHall ph in PerformanceHalls)
            {
                PerformanceHallStrings.Add(ph.ID_HALL.ToString());
            }

            OnPropertyChanged("PublishingHouseStrings");
            OnPropertyChanged("PerformanceHallStrings");
        }

        private bool CanModify()
        {
            if(selectedReservation != null && selectedReservation.Organize.CompetitionID_COMP.ToString() == selectedCompetition && selectedReservation.OrganizePublishingHouseID_PH.ToString() == selectedPublishingHouse && selectedReservation.PerformanceHallID_HALL.ToString()==selectedPerformanceHall)
            {
                int stm = -1;
                int sth = -1;
                int eth = -1;
                int etm = -1;
                if (int.TryParse(StartTimeHoursTB, out sth) && int.TryParse(StartTimeMinutesTB, out stm) && int.TryParse(EndTimeHoursTB, out eth) && int.TryParse(EndTimeMinutesTB, out etm))
                {
                    if (PublishingHouseStrings.Contains(selectedPublishingHouse) && ReservationDP != null && CompetitionStrings.Contains(selectedCompetition) && PerformanceHallStrings.Contains(selectedPerformanceHall) && stm >= 0 && stm <= 60 && etm >= 0 && etm <= 60 && sth >= 0 && sth < 24 && eth >= 0 && eth < 24)
                    {

                        if(sth < eth)
                        {
                            return true;
                        }
                        else if(sth == eth)
                        {
                            if(stm < etm)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public Common.Models.Reserve SelectedReservation
        {
            get
            {
                return selectedReservation;
            }
            set
            {
                

                selectedReservation = value;
                if(selectedReservation != null)
                {
                    SelectedPublishingHouse = selectedReservation.OrganizePublishingHouseID_PH.ToString();
                    SelectedCompetition = selectedReservation.OrganizeCompetitionID_COMP.ToString();
                    SelectedPerformanceHall = selectedReservation.PerformanceHallID_HALL.ToString();
                    ReservationDP = selectedReservation.DATE_RES;
                    EndTimeMinutesTB = selectedReservation.END_TIME.Minutes.ToString();
                    EndTimeHoursTB = selectedReservation.END_TIME.Hours.ToString();
                    StartTimeHoursTB = selectedReservation.START_TIME.Hours.ToString();
                    StartTimeMinutesTB = selectedReservation.START_TIME.Minutes.ToString();

                }
                DeleteCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
            }
        }


        public string SelectedPublishingHouse
        {
            get
            {
                return selectedPublishingHouse;
            }
            set {
                selectedPublishingHouse = value;
                CompetitionStrings = new List<string>();
                RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
                if(int.TryParse(SelectedPublishingHouse,out int phid))
                {
                    Common.Models.PublishingHouse ph = repo.RepositoryProxy.ReadPublishingHouse(phid);

                    foreach(Common.Models.Organize org in Organizations)
                    {
                        if(org.PublishingHouseID_PH == phid)
                        {
                            CompetitionStrings.Add(org.CompetitionID_COMP.ToString());
                        }
                    }

                }
                OnPropertyChanged("CompetitionStrings");
                OnPropertyChanged("SelectedPublishingHouse");

                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();

            }
        }

        public string SelectedCompetition
        {
            get
            {
                return selectedCompetition;
            }
            set
            {
                selectedCompetition = value;
                OnPropertyChanged("SelectedCompetition");
                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
            }
        }

        public string SelectedPerformanceHall
        {
            get
            {
                return selectedPerformanceHall;
            }
            set
            {
                selectedPerformanceHall = value;
                OnPropertyChanged("SelectedPerformanceHall");
                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
            }
        }

        public string StartTimeHoursTB { get => startTimeHoursTB; set { startTimeHoursTB = value; OnPropertyChanged("StartTimeHoursTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string StartTimeMinutesTB { get => startTimeMinutesTB; set { startTimeMinutesTB = value; OnPropertyChanged("StartTimeMinutesTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string EndTimeHoursTB { get => endTimeHoursTB; set { endTimeHoursTB = value; OnPropertyChanged("EndTimeHoursTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string EndTimeMinutesTB { get => endTimeMinutesTB; set { endTimeMinutesTB = value; OnPropertyChanged("EndTimeMinutesTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }

        public DateTime ReservationDP { get => reservationDP; set { reservationDP = value; OnPropertyChanged("ReservationDP"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }

        private bool CanAdd()
        {
            int stm = -1;
            int sth = -1;
            int eth = -1;
            int etm = -1;
            if (int.TryParse(StartTimeHoursTB, out sth) && int.TryParse(StartTimeMinutesTB, out stm) && int.TryParse(EndTimeHoursTB, out eth) && int.TryParse(EndTimeMinutesTB, out etm))
            {
                if (PublishingHouseStrings.Contains(selectedPublishingHouse) && ReservationDP != null && CompetitionStrings.Contains(selectedCompetition) && PerformanceHallStrings.Contains(selectedPerformanceHall) && stm>=0 && stm<=60 && etm >= 0 && etm <= 60 && sth >= 0 && sth < 24 && eth >= 0 && eth < 24)
                {
                    if (sth < eth)
                    {
                        return true;
                    }
                    else if (sth == eth)
                    {
                        if (stm < etm)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }



        }


        private void OnModify()
        {
            int stm = -1;
            int sth = -1;
            int eth = -1;
            int etm = -1;


            if (int.TryParse(StartTimeHoursTB, out sth) && int.TryParse(StartTimeMinutesTB, out stm) && int.TryParse(EndTimeHoursTB, out eth) && int.TryParse(EndTimeMinutesTB, out etm))
            {
                RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
                repo.RepositoryProxy.EditReservation(new Common.Models.Reserve(ReservationDP, new TimeSpan(sth, stm, 0), new TimeSpan(eth, etm, 0), selectedReservation.OrganizePublishingHouseID_PH, selectedReservation.OrganizeCompetitionID_COMP, selectedReservation.PerformanceHallID_HALL, selectedReservation.Organize, selectedReservation.PerformanceHall));
                RefreshTable();
            }
            else
            {
                System.Windows.MessageBox.Show("There was a problem! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


        }

        private void OnAdd()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();

            int stm = -1;
            int sth = -1;
            int eth = -1;
            int etm = -1;

            if (int.TryParse(StartTimeHoursTB, out sth) && int.TryParse(StartTimeMinutesTB, out stm) && int.TryParse(EndTimeHoursTB, out eth) && int.TryParse(EndTimeMinutesTB, out etm))
            {

                if (PublishingHouseStrings.Contains(selectedPublishingHouse) && CompetitionStrings.Contains(selectedCompetition) && PerformanceHallStrings.Contains(selectedPerformanceHall) && stm >= 0 && stm <= 60 && etm >= 0 && etm <= 60 && sth >= 0 && sth < 24 && eth >= 0 && eth < 24)
                {
                    if(repo.RepositoryProxy.AddHallReservation(int.Parse(selectedCompetition),int.Parse(selectedPublishingHouse),int.Parse(selectedPerformanceHall),reservationDP,new TimeSpan(sth,stm,0),new TimeSpan(eth, etm, 0))){
                        RefreshTable();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("There was a problem. Choose another IDs! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {

                    System.Windows.MessageBox.Show("There was a problem! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("There was a problem! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private bool CanDelete()
        {
            return SelectedReservation != null;
        }
        private void OnDelete()
        {
            if (SelectedReservation != null)
            {
                RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
                repo.RepositoryProxy.DeleteHallReservation(selectedReservation.OrganizePublishingHouseID_PH, selectedReservation.OrganizeCompetitionID_COMP, selectedReservation.PerformanceHallID_HALL);
                RefreshTable();
            }
        }
        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Reservations = new ObservableCollection<Common.Models.Reserve>(repo.RepositoryProxy.ReadHallReservations());
            OnPropertyChanged("Reservations");

        }


    }
}
