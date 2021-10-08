using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
namespace ClientUI.ViewModel
{
    public class OrganizationsTableViewModel:BindableBase
    {
        public ObservableCollection<Common.Models.Organize> Organizations { get; set; } = new ObservableCollection<Common.Models.Organize>();

        private Common.Models.Organize selectedOrganization;

        private string selectedCompetition = "";
        private string selectedPublishingHouse = "";

        public List<string> PublishingHouseStrings { get; set; } = new List<string>();
        public List<string> CompetitionStrings { get; set; } = new List<string>();

        public List<Common.Models.PublishingHouse> PublishingHouses;
        public List<Common.Models.Competition> Competitions;


        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }

        private bool isAdministrator = false;
        public bool IsAdministrator { get => isAdministrator; set { isAdministrator = value; OnPropertyChanged("IsAdministrator"); } }


        public OrganizationsTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            IsAdministrator = LoggedInUserSingleton.Instance.CheckRole("Administrator");
            Organizations = new ObservableCollection<Common.Models.Organize>(repo.RepositoryProxy.ReadOrganizations());
            Competitions = repo.RepositoryProxy.ReadCompetitions().ToList();
            PublishingHouses = repo.RepositoryProxy.ReadPublishingHouses().ToList();
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);

            foreach (Common.Models.Competition cmp in Competitions)
            {
                CompetitionStrings.Add(cmp.ID_COMP.ToString());
            }

            foreach(Common.Models.PublishingHouse ph in PublishingHouses)
            {
                PublishingHouseStrings.Add(ph.NAME_PH.ToString());
            }
            OnPropertyChanged("PublishingHouseStrings");
            OnPropertyChanged("CompetitionStrings");
        }


        public Common.Models.Organize SelectedOrganization
        {
            get
            {
                return selectedOrganization;
            }
            set
            {
                selectedOrganization = value;
                if (selectedOrganization != null)
                {
                    SelectedPublishingHouse = selectedOrganization.PublishingHouse.NAME_PH;
                    SelectedCompetition = selectedOrganization.CompetitionID_COMP.ToString();
                }
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public string SelectedPublishingHouse { get => selectedPublishingHouse; set { selectedPublishingHouse = value; OnPropertyChanged("SelectedPublishingHouse"); AddCommand.RaiseCanExecuteChanged(); } }
        public string SelectedCompetition { get => selectedCompetition; set { selectedCompetition = value; OnPropertyChanged("SelectedCompetition"); AddCommand.RaiseCanExecuteChanged(); } }


        private bool CanAdd()
        {
            if (CompetitionStrings.Contains(SelectedCompetition))
            {
               

                foreach(Common.Models.PublishingHouse ph in PublishingHouses)
                {
                    if(ph.NAME_PH == selectedPublishingHouse)
                    {

                        //ne sme se dva puta isto takmicenje preuzeti od razlicitih kuca
                        RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
                        List<Common.Models.Organize> orgztemp = repo.RepositoryProxy.ReadOrganizations().ToList();

                        Common.Models.Organize orttemp = orgztemp.Find(x => x.CompetitionID_COMP.ToString() == selectedCompetition);
                        if (orttemp != null)
                        {
                            return false;
                        }

                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }





        }

        private void OnAdd()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            long publishingHouseId = -1;
            int competitionId = -1;

            if (int.TryParse(SelectedCompetition, out competitionId))
            {
                foreach (Common.Models.PublishingHouse ph in PublishingHouses)
                {
                    if (ph.NAME_PH == selectedPublishingHouse)
                    {
                        if(repo.RepositoryProxy.AddPublishingHouseOrganization(competitionId, ph.ID_PH))
                        {
                            RefreshTable();
                            return;
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("There was a problem. Choose another IDs! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                }
                System.Windows.MessageBox.Show("There was a problem! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                System.Windows.MessageBox.Show("There was a problem! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        private bool CanDelete()
        {
            return SelectedOrganization != null;
        }
        private void OnDelete()
        {
            if (SelectedOrganization != null)
            {
                RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
                repo.RepositoryProxy.DeleteOrganization(selectedOrganization.PublishingHouseID_PH, selectedOrganization.CompetitionID_COMP);
                RefreshTable();
            }
        }

        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Organizations = new ObservableCollection<Common.Models.Organize>(repo.RepositoryProxy.ReadOrganizations());
            OnPropertyChanged("Organizations");
           
        }
    }
}
