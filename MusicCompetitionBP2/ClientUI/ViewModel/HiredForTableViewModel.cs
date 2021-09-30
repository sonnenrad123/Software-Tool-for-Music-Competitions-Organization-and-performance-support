using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
namespace ClientUI.ViewModel
{
    public class HiredForTableViewModel:BindableBase
    {
        public ObservableCollection<Common.Models.HiredFor> HiredForSet { get; set; } = new ObservableCollection<Common.Models.HiredFor>();
        private Common.Models.HiredFor selectedHiredFor;
        private string selectedJuryMember;
        private string selectedCompetition;

        public List<string> CompetitionStrings { get; set; } = new List<string>();
        public List<string> JuryMemberStrings { get; set; } = new List<string>();



        public List<Common.Models.Competition> Competitions;
        public List<Common.Models.JuryMember> JuryMembers;

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }

        public HiredForTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            HiredForSet = new ObservableCollection<Common.Models.HiredFor>(repo.RepositoryProxy.ReadEngagemenets());

            Competitions = repo.RepositoryProxy.ReadCompetitions().ToList();
            JuryMembers = repo.RepositoryProxy.ReadJuryMembers().ToList();
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);

            foreach (Common.Models.Competition cmp in Competitions)
            {
               CompetitionStrings.Add(cmp.NAME_COMP);
            }

            foreach (Common.Models.JuryMember jr in JuryMembers)
            {
                JuryMemberStrings.Add(jr.JMBG_SIN.ToString());
            }

            OnPropertyChanged("CompetitionStrings");
            OnPropertyChanged("JuryMemberStrings");
        }


        public Common.Models.HiredFor SelectedHiredFor
        {
            get
            {
                return selectedHiredFor;
            }
            set
            {
                selectedHiredFor = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public string SelectedJuryMember { get => selectedJuryMember; set { selectedJuryMember = value; OnPropertyChanged("SelectedJuryMember"); AddCommand.RaiseCanExecuteChanged(); } }
        public string SelectedCompetition { get => selectedCompetition; set { selectedCompetition = value; OnPropertyChanged("SelectedCompetition"); AddCommand.RaiseCanExecuteChanged(); } }


        private bool CanAdd()
        {
            Common.Models.Competition gt = null;
            foreach (Common.Models.Competition gtemp in Competitions)
            {
                if (gtemp.NAME_COMP == selectedCompetition)
                {
                    gt = gtemp;
                }
            }

            if (gt == null)
            {
                return false;
            }

            if (long.TryParse(SelectedJuryMember, out long jmbg))
            {
                if (JuryMemberStrings.Contains(SelectedJuryMember))
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

        private void OnAdd()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();

            long jmbg = -1;

            Common.Models.Competition gt = null;
            foreach (Common.Models.Competition gtemp in Competitions)
            {
                if (gtemp.NAME_COMP== selectedCompetition)
                {
                    gt = gtemp;
                }
            }

            if (gt == null)
            {
                System.Windows.MessageBox.Show("There was a problem! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (long.TryParse(SelectedJuryMember, out jmbg))
            {
                if (repo.RepositoryProxy.HireSingerForCompetition(jmbg,gt.ID_COMP))
                {
                    RefreshTable();
                }
                else
                {
                    System.Windows.MessageBox.Show("There was a problem choose another JMBG and ID! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            return  SelectedHiredFor != null;
        }

        private void OnDelete()
        {
            if (SelectedHiredFor != null)
            {
                RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
                repo.RepositoryProxy.DeleteHiredFor(selectedHiredFor.JuryMemberJMBG_SIN, selectedHiredFor.CompetitionID_COMP);
                RefreshTable();
            }
        }
        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            HiredForSet = new ObservableCollection<Common.Models.HiredFor>(repo.RepositoryProxy.ReadEngagemenets());
            OnPropertyChanged("HiredForSet");
           
        }

    }
}
