using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
namespace ClientUI.ViewModel
{
    public class PossessesATableViewModel:BindableBase
    {
        public ObservableCollection<Common.Models.PossessesA> CompetitionGenres { get; set; } = new ObservableCollection<Common.Models.PossessesA>();
        private Common.Models.PossessesA selectedCompetitionGenre;
        private string selectedGenre = "";
        private string selectedCompetition = "";

        public List<string> GenreStrings { get; set; } = new List<string>();
        public List<string> CompetitionStrings { get; set; } = new List<string>();

        public List<Common.Models.Genre> Genres;
        public List<Common.Models.Competition> Competitions;

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }

        public PossessesATableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            CompetitionGenres = new ObservableCollection<Common.Models.PossessesA>(repo.RepositoryProxy.ReadPossessATable());
            Competitions = repo.RepositoryProxy.ReadCompetitions().ToList();
            Genres = repo.RepositoryProxy.ReadGenres().ToList();
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            foreach (Common.Models.Genre cmp in Genres)
            {

                GenreStrings.Add(cmp.ID_GENRE.ToString());
            }

            foreach (Common.Models.Competition cmp in Competitions)
            {
                CompetitionStrings.Add(cmp.ID_COMP.ToString());
            }
            OnPropertyChanged("GenreStrings");
            OnPropertyChanged("CompetitionStrings");
        }

        public Common.Models.PossessesA SelectedCompetitionGenre
        {
            get
            {
                return selectedCompetitionGenre;
            }
            set
            {
                selectedCompetitionGenre = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }

        }

        public string SelectedGenre { get => selectedGenre; set { selectedGenre = value; OnPropertyChanged("SelectedGenre"); AddCommand.RaiseCanExecuteChanged(); } }
        public string SelectedCompetition { get => selectedCompetition; set { selectedCompetition = value; OnPropertyChanged("SelectedCompetition"); AddCommand.RaiseCanExecuteChanged(); } }

        private bool CanAdd()
        {
            if (GenreStrings.Contains(SelectedGenre) && CompetitionStrings.Contains(SelectedCompetition))
            {
                return true;
            }
            return false;





        }

        private void OnAdd()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            int genreId = -1;
            int competitionId = -1;

            if (int.TryParse(SelectedGenre, out genreId) && int.TryParse(SelectedCompetition, out competitionId))
            {
                
                if (repo.RepositoryProxy.AddGenreToCompetition(genreId, competitionId))
                {

                }
                else
                {
                    System.Windows.MessageBox.Show("There was a problem! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            return SelectedGenre != null;
        }

        private void OnDelete()
        {
            if (SelectedCompetitionGenre != null)
            {
                RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
                repo.RepositoryProxy.DeletePossessA(selectedCompetitionGenre.GenreID_GENRE, selectedCompetitionGenre.CompetitionID_COMP);
                RefreshTable();
            }
        }

        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            CompetitionGenres = new ObservableCollection<Common.Models.PossessesA>(repo.RepositoryProxy.ReadPossessATable());
            OnPropertyChanged("CompetitionGenres");
        }

    }
}
