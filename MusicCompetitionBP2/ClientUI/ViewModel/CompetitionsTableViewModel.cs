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
    public class CompetitionsTableViewModel : BindableBase
    {
        public ObservableCollection<Common.Models.Competition> Competitions { get; set; } = new ObservableCollection<Common.Models.Competition>();
        private MainWindowViewModel mainWindow;

        private Common.Models.Competition selectedCompetition;
        private string nameTB = "";
        private string maxCompetitorsTB="";
        private DateTime startDateDP = DateTime.Now;
        private DateTime endDateDP = DateTime.Now;



        private bool isEventOrganizer = false;
        public bool IsEventOrganizer { get => isEventOrganizer; set { isEventOrganizer = value; OnPropertyChanged("IsEventOrganizer"); } }
        private bool isCompetitor = false;
        public bool IsCompetitor { get => isCompetitor; set { isCompetitor = value; OnPropertyChanged("IsCompetitor"); } }


        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand ModifyCommand { get; set; }
        public MyICommand ApplyCommand { get; set; }
        public CompetitionsTableViewModel(MainWindowViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Competitions = new ObservableCollection<Competition>(repo.RepositoryProxy.ReadCompetitions());


            IsEventOrganizer = LoggedInUserSingleton.Instance.CheckRole("EventOrganizer");
            OnPropertyChanged("IsEventOrganizer");
            IsCompetitor = LoggedInUserSingleton.Instance.CheckRole("Competitor");
            OnPropertyChanged("IsCompetitor");

            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            ModifyCommand = new MyICommand(OnModify, CanModify);
            ApplyCommand = new MyICommand(OnApply, CanApply);
        }

        private bool CanApply()
        {
            return selectedCompetition != null;
        }

        private void OnApply()
        {
            if (selectedCompetition == null)
            {
                return;
            }

            long CompetitorJMBG = LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN;
            int idCompetition = selectedCompetition.ID_COMP;

            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Organize orgtemp = repo.RepositoryProxy.ReadOrganizations().FirstOrDefault(x => x.CompetitionID_COMP == idCompetition);

            if(orgtemp == null)
            {
                System.Windows.MessageBox.Show("Selected competition isn't still organized by publishing house!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            

            if (repo.RepositoryProxy.AddCompetitorToCompetition(CompetitorJMBG, idCompetition, orgtemp.PublishingHouseID_PH) == false)
            {
                System.Windows.MessageBox.Show("Something went wrong. Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                System.Windows.MessageBox.Show("Successfully applied for competition with Name: " + orgtemp.Competition.NAME_COMP +".", "Congrats.", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

        }


        private bool CanModify()
        {


            return selectedCompetition!=null && NameTB != "" && int.TryParse(MaxCompetitorsTB, out int mc) && StartDateDP != null && EndDateDP != null && StartDateDP<=EndDateDP;
        }

        private void OnModify()
        {
            int maxcomp = -1;
            if (!(int.TryParse(MaxCompetitorsTB, out maxcomp)))
            {
                System.Windows.MessageBox.Show("Max competitors must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.EditCompetition(new Competition(SelectedCompetition.ID_COMP, startDateDP, endDateDP, nameTB, maxcomp));
            RefreshTable();
        }

        private bool CanAdd()
        {

            return (NameTB != "" && int.TryParse(MaxCompetitorsTB, out int mc) && StartDateDP != null && EndDateDP != null && StartDateDP<=EndDateDP);
        }

        private void OnAdd()
        {
            int maxcomp = -1;
            if(!(int.TryParse(MaxCompetitorsTB,out maxcomp)))
            {
                System.Windows.MessageBox.Show("Max competitors must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();

            int idComp = -1;


            repo.RepositoryProxy.AddCompetition(new Competition(-1, startDateDP, endDateDP, NameTB, maxcomp),out idComp);

            if (LoggedInUserSingleton.Instance.loggedInUser.Type == "EventOrganizer" && idComp != -1)
            {
                EventOrganizer eotemp = repo.RepositoryProxy.ReadEventOrganizer(LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN);
                repo.RepositoryProxy.AddPublishingHouseOrganization(idComp, eotemp.PublishingHouseID_PH);
            }


            RefreshTable();
        }

        private bool CanDelete()
        {
            return SelectedCompetition != null;
        }

        private void OnDelete()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.DeleteCompetition(selectedCompetition.ID_COMP);
            RefreshTable();
        }

        public Common.Models.Competition SelectedCompetition
        {
            get => selectedCompetition;
            set
            {
                selectedCompetition = value;

                if(selectedCompetition != null)
                {
                    NameTB = selectedCompetition.NAME_COMP;
                    MaxCompetitorsTB = selectedCompetition.MAX_COMPETITORS.ToString();
                    StartDateDP = selectedCompetition.DATE_START;
                    EndDateDP = selectedCompetition.DATE_END;
                    OnPropertyChanged("NameTB");
                    OnPropertyChanged("MaxCompetitorsTB");
                    OnPropertyChanged("StartDateDP");
                    OnPropertyChanged("EndDateDP");
                }


                DeleteCommand.RaiseCanExecuteChanged();
                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
                ApplyCommand.RaiseCanExecuteChanged();
            }
        }

        public string NameTB
        {
            get
            {
                return nameTB;
            }
            set
            {
                nameTB = value;
                OnPropertyChanged("NameTB");
                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
            }
        }
        public string MaxCompetitorsTB { get => maxCompetitorsTB; set { maxCompetitorsTB = value; OnPropertyChanged("MaxCompetitorsTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public DateTime StartDateDP { get => startDateDP; set { startDateDP = value; OnPropertyChanged("StartDateDP"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public DateTime EndDateDP { get => endDateDP; set { endDateDP = value; OnPropertyChanged("EndDateDP"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }

        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            List<Competition> templist;
            templist = repo.RepositoryProxy.ReadCompetitions().ToList();
            Competitions = new ObservableCollection<Competition>(templist);
            OnPropertyChanged("Competitions");
        }
    }
}
