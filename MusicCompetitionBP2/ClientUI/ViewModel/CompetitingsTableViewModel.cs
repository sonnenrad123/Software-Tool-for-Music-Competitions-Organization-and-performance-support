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


        public List<Common.Models.Competitor> Competitors;
        public List<Common.Models.Competition> Competitions;

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }


        public CompetitingsTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Competitings = new ObservableCollection<Common.Models.Competiting>(repo.RepositoryProxy.ReadCompetitings());
            Competitions = repo.RepositoryProxy.ReadCompetitions().ToList();
            Competitors = repo.RepositoryProxy.ReadCompetitors().ToList();
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            foreach (Common.Models.Competitor cmp in Competitors)
            {
                
                CompetitorStrings.Add(cmp.JMBG_SIN.ToString());
            }

            foreach (Common.Models.Competition cmp in Competitions)
            {
                CompetitionStrings.Add(cmp.ID_COMP.ToString());
            }

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

                if (repo.RepositoryProxy.AddCompetitorToCompetition(competitorId, competitionId))
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
                repo.RepositoryProxy.DeleteCompetiting(selectedCompetiting.CompetitorJMBG_SIN, selectedCompetiting.CompetitionID_COMP);
                RefreshTable();
            }
        }


        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Competitings = new ObservableCollection<Common.Models.Competiting>(repo.RepositoryProxy.ReadCompetitings());
            OnPropertyChanged("Competitings");
        }


    }
}
