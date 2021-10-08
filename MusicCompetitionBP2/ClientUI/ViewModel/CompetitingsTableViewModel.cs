using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace ClientUI.ViewModel
{
    public class CompetitingsTableViewModel:BindableBase
    {
        public ObservableCollection<Common.Models.Competiting> Competitings { get; set; } = new ObservableCollection<Common.Models.Competiting>();
        private Common.Models.Competiting selectedCompetiting;
        private string selectedCompetitor = "";
        private string selectedCompetition = "";
        public List<string> CompetitorStrings { get; set; } = new List<string>();
        public List<string> CompetitionStrings { get; set; } = new List<string>();

        private bool isEventOrganizer = false;
        public bool IsEventOrganizer { get => isEventOrganizer; set { isEventOrganizer = value; OnPropertyChanged("IsEventOrganizer"); } }


        public List<Common.Models.Competitor> Competitors;
        public List<Common.Models.Competition> Competitions;
        public List<Common.Models.Organize> Organizations;


        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }


        public CompetitingsTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Competitings = new ObservableCollection<Common.Models.Competiting>(repo.RepositoryProxy.ReadCompetitings());
            IsEventOrganizer = LoggedInUserSingleton.Instance.CheckRole("EventOrganizer");
            Competitions = repo.RepositoryProxy.ReadCompetitions().ToList();
            Competitors = repo.RepositoryProxy.ReadCompetitors().ToList();
            Organizations = repo.RepositoryProxy.ReadOrganizations().ToList();
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            foreach (Common.Models.Competitor cmp in Competitors)
            {
                
                CompetitorStrings.Add(cmp.JMBG_SIN.ToString());
            }

            foreach (Common.Models.Competition cmp in Competitions)
            {
                Common.Models.Organize org = Organizations.Find(x => x.CompetitionID_COMP == cmp.ID_COMP);
                if (org != null && cmp.DATE_START > DateTime.Now)
                {
                   if(LoggedInUserSingleton.Instance.loggedInUser.Type == "EventOrganizer")
                    {
                        Common.Models.EventOrganizer eotemp = repo.RepositoryProxy.ReadEventOrganizer(LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN);
                        if(eotemp.PublishingHouseID_PH == org.PublishingHouseID_PH)//EO moze da prijavi takmicare samo na takmicenja od njegove izdavacke kuce
                        {
                            CompetitionStrings.Add(cmp.ID_COMP.ToString());
                        }
                    }
                    else if(LoggedInUserSingleton.Instance.loggedInUser.Type == "Administrator")
                    {
                        CompetitionStrings.Add(cmp.ID_COMP.ToString());
                    }
                    
                    
                }
                
               
            }

            //ako je ulogovan takmicar on moze da vidi samo svoja takmicenja
            if(LoggedInUserSingleton.Instance.loggedInUser.Type == "Competitor")
            {
                List<Common.Models.Competiting> competitings = repo.RepositoryProxy.ReadCompetitings().ToList();
                Competitings = new ObservableCollection<Common.Models.Competiting>();
                foreach (Common.Models.Competiting cmptemp in competitings)
                {
                    if(cmptemp.CompetitorJMBG_SIN == LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN)
                    {
                        Competitings.Add(cmptemp);
                    }
                }

            }



            OnPropertyChanged("Competitings");
            OnPropertyChanged("CompetitorStrings");
            OnPropertyChanged("CompetitionStrings");
            
        }

        public Common.Models.Competiting SelectedCompetiting
        {
            get
            {
                return selectedCompetiting;
            }
            set
            {
                selectedCompetiting = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }
            
        }

        public string SelectedCompetitor { get => selectedCompetitor; set { selectedCompetitor = value; OnPropertyChanged("SelectedCompetitorTB"); AddCommand.RaiseCanExecuteChanged(); } }
        public string SelectedCompetition { get => selectedCompetition; set { selectedCompetition = value; OnPropertyChanged("SelectedCompetitionTB"); AddCommand.RaiseCanExecuteChanged(); } }

        private bool CanAdd()
        {
            if (CompetitorStrings.Contains(SelectedCompetitor) && CompetitionStrings.Contains(SelectedCompetition))
            {
                return true;
            }
            return false;



          

        }

        private void OnAdd()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            long competitorId = -1;
            int competitionId = -1;

            if(long.TryParse(SelectedCompetitor, out competitorId) && int.TryParse(SelectedCompetition, out competitionId))
            {
                Common.Models.Organize org = Organizations.Find(x => x.CompetitionID_COMP == competitionId);
                if (repo.RepositoryProxy.AddCompetitorToCompetition(competitorId, competitionId,org.PublishingHouseID_PH))
                {

                }
                else
                {
                    System.Windows.MessageBox.Show("There was a problem, choose another IDs! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                RefreshTable();
            }
            else
            {
                System.Windows.MessageBox.Show("There was a problem! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }


        private bool CanDelete()
        {
            return SelectedCompetiting != null;
        }

        private void OnDelete()
        {
            if ( SelectedCompetiting != null){
                RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
                
                repo.RepositoryProxy.DeleteCompetiting(selectedCompetiting.CompetitorJMBG_SIN, selectedCompetiting.OrganizeCompetitionID_COMP,selectedCompetiting.OrganizePublishingHouseID_PH);
                RefreshTable();
            }
        }


        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Competitings = new ObservableCollection<Common.Models.Competiting>(repo.RepositoryProxy.ReadCompetitings());

            if (LoggedInUserSingleton.Instance.loggedInUser.Type == "Competitor")
            {
                List<Common.Models.Competiting> competitings = repo.RepositoryProxy.ReadCompetitings().ToList();
                Competitings = new ObservableCollection<Common.Models.Competiting>();
                foreach (Common.Models.Competiting cmptemp in competitings)
                {
                    if (cmptemp.CompetitorJMBG_SIN == LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN)
                    {
                        Competitings.Add(cmptemp);
                    }
                }

            }


            OnPropertyChanged("Competitings");
        }


    }
}
