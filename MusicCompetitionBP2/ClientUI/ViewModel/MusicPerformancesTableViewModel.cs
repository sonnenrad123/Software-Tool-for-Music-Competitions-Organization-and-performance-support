using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Common.Models;
using System.Windows;

namespace ClientUI.ViewModel
{
    public class MusicPerformancesTableViewModel:BindableBase
    {
        public  ObservableCollection<Common.Models.MusicPerformance> MusicPerformances { get; set; } = new ObservableCollection<Common.Models.MusicPerformance>();
        private MusicPerformance selectedMusicPerformance;
        private string origPerfTB = "";
        private string songAuthorTB = "";
        private string songNameTB = "";
        private string selectedCompetitor = "";
        private string selectedCompetition = "";
        private string selectedGenre;
        private DateTime dateDP = DateTime.Now;



        public List<Common.Models.Competitor> Competitors;
        public List<Common.Models.Competition> Competitions;
        public List<Common.Models.Genre> Genres;
        public List<string> CompetitorStrings { get; set; } = new List<string>();
        public List<string> CompetitionStrings { get; set; } = new List<string>();
        public List<string> GenreStrings { get; set; } = new List<string>();

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand ModifyCommand { get; set; }



        public MusicPerformancesTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            MusicPerformances = new ObservableCollection<MusicPerformance>(repo.RepositoryProxy.ReadMusicPerformances());
            Competitions = repo.RepositoryProxy.ReadCompetitions().ToList();
            Competitors = repo.RepositoryProxy.ReadCompetitors().ToList();
            Genres = repo.RepositoryProxy.ReadGenres().ToList();
            
            foreach(Competitor cmp in Competitors)
            {
                string ime_prezime = cmp.FIRSTNAME_SIN + " " + cmp.LASTNAME_SIN;
                CompetitorStrings.Add(cmp.JMBG_SIN.ToString());
            }

            foreach(Competition cmp in Competitions)
            {
                CompetitionStrings.Add(cmp.NAME_COMP);
            }

            foreach(Genre g in Genres)
            {
                GenreStrings.Add(g.GENRE_NAME);
            }
            OnPropertyChanged("CompetitorStrings");
            OnPropertyChanged("CompetitionStrings");
            OnPropertyChanged("GenreStrings");

            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            ModifyCommand = new MyICommand(OnModify, CanModify);
        }


        public MusicPerformance SelectedMusicPerformance
        {
            get
            {
                return selectedMusicPerformance;
            }
            set {
                selectedMusicPerformance = value;
                if(SelectedMusicPerformance != null)
                {
                    OrigPerfTB = selectedMusicPerformance.ORIG_PERFORMER;
                    SongAuthorTB = selectedMusicPerformance.SONG_AUTHOR;
                    SongNameTB = selectedMusicPerformance.SONG_NAME;

                    RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();

                    Competitor competitorTemp = repo.RepositoryProxy.ReadCompetitor(selectedMusicPerformance.CompetitingCompetitorJMBG_SIN ?? default(long));
                    SelectedCompetitor = competitorTemp.JMBG_SIN.ToString();

                    Competition cmpTemp = repo.RepositoryProxy.ReadCompetition(SelectedMusicPerformance.CompetitingOrganizeCompetitionID_COMP ?? default(int));
                    SelectedCompetition = cmpTemp.NAME_COMP;

                    Genre genretmp = repo.RepositoryProxy.ReadGenre(SelectedMusicPerformance.GenreID_GENRE);
                    SelectedGenre = genretmp.GENRE_NAME;

                    DateDP = SelectedMusicPerformance.DATE_PERF;

                    OnPropertyChanged("OrigPerfTB");
                    OnPropertyChanged("SongAuthorTB");
                    OnPropertyChanged("SongNameTB");
                    OnPropertyChanged("SelectedCompetitor");
                    OnPropertyChanged("SelectedCompetition");
                    OnPropertyChanged("SelectedGenre");
                    OnPropertyChanged("DateDP");
                    
                }
                DeleteCommand.RaiseCanExecuteChanged();
                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
            }

        }


        private bool CanModify()
        {
            bool AllOkey = true;

            if(selectedMusicPerformance == null)
            {
                return false;
            }

            if (OrigPerfTB == "" || SongAuthorTB == "" || songNameTB == "" || selectedCompetition != selectedMusicPerformance.Competiting.Organize.Competition.NAME_COMP || selectedGenre != selectedMusicPerformance.Genre.GENRE_NAME || selectedCompetitor != selectedMusicPerformance.CompetitingCompetitorJMBG_SIN.ToString())
            {
                AllOkey = false;
                
            }




            return AllOkey;
        }

        private void OnModify()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            long competitorId = -1;
            int competitionId = -1;
            int genreID = -1;
            if(selectedMusicPerformance == null)
            {
                ModifyCommand.RaiseCanExecuteChanged();
                return;
            }
            foreach (Competitor cmp in Competitors)
            {
                if (selectedCompetitor == (cmp.JMBG_SIN.ToString()))
                {
                    competitorId = cmp.JMBG_SIN;
                }
            }

            foreach (Competition cmp in Competitions)
            {
                if (selectedCompetition == cmp.NAME_COMP)
                {
                    competitionId = cmp.ID_COMP;
                }
            }

            foreach (Genre g in Genres)
            {
                if (g.GENRE_NAME == selectedGenre)
                {
                    genreID = g.ID_GENRE;
                }
            }

            if (competitionId == -1 || competitorId == -1 || genreID == -1)
            {
                System.Windows.MessageBox.Show("There was a problem! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<Common.Models.Organize> orgnz = repo.RepositoryProxy.ReadOrganizations().ToList();
            Common.Models.Organize orgtemp = orgnz.Find(x => x.CompetitionID_COMP == competitionId);

            repo.RepositoryProxy.EditMusicPerformance(new MusicPerformance(selectedMusicPerformance.ID_PERF, OrigPerfTB, SongNameTB, SongAuthorTB, DateDP, competitorId, competitionId,orgtemp.PublishingHouseID_PH, genreID, null, null));
            RefreshTable();
        }

        private bool CanAdd()
        {
            bool AllOkey = true;

            if(OrigPerfTB == "" || SongAuthorTB == "" || songNameTB == "" || selectedCompetition == "" || selectedGenre == "" || selectedCompetitor == "")
            {
                AllOkey = false;
            }




            return AllOkey;

        }

        private void OnAdd()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            List<Common.Models.Organize> orgnz = repo.RepositoryProxy.ReadOrganizations().ToList();
            

            long competitorId = -1;
            int competitionId = -1;
            int genreID = -1;
            
            foreach (Competitor cmp in Competitors)
            {
                if(selectedCompetitor == cmp.JMBG_SIN.ToString())
                {
                    competitorId = cmp.JMBG_SIN;
                }
            }

            foreach (Competition cmp in Competitions)
            {
                if(selectedCompetition == cmp.NAME_COMP)
                {
                    competitionId = cmp.ID_COMP;
                }
            }

            foreach (Genre g in Genres)
            {
                if(g.GENRE_NAME == selectedGenre)
                {
                    genreID = g.ID_GENRE;
                }
            }

            if(competitionId == -1 || competitorId == -1 || genreID == -1) {
                System.Windows.MessageBox.Show("There was a problem! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Common.Models.Organize orgtemp = orgnz.Find(x => x.CompetitionID_COMP == competitionId);
            if (orgtemp == null)
            {
                System.Windows.MessageBox.Show("There was a problem! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int pubhouseid = orgtemp.PublishingHouseID_PH;
          

            if (repo.RepositoryProxy.AddMusicPerformance(new MusicPerformance(-1, OrigPerfTB, SongNameTB, SongAuthorTB, DateDP, competitorId, competitionId,pubhouseid, genreID, null, null)))
            {
                RefreshTable();
            }
            else
            {
                System.Windows.MessageBox.Show(string.Format("There is no competitor {0} that competes at {1}! Please, try again.", selectedCompetitor,selectedCompetition), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private bool CanDelete()
        {
            return SelectedGenre != null;
        }

        private void OnDelete()
        {
            if(selectedMusicPerformance != null)
            {
                RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
                repo.RepositoryProxy.DeleteMusicPerformance(selectedMusicPerformance.ID_PERF);
                RefreshTable();
            }
        }




        public string OrigPerfTB { get => origPerfTB; set { origPerfTB = value; OnPropertyChanged("OrigPerfTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string SongAuthorTB { get => songAuthorTB; set { songAuthorTB = value; OnPropertyChanged("SongAuthorTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string SongNameTB { get => songNameTB; set { songNameTB = value; OnPropertyChanged("SongNameTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string SelectedCompetitor { get => selectedCompetitor; set { selectedCompetitor = value; OnPropertyChanged("SelectedCompetitorTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string SelectedCompetition { get => selectedCompetition; set { selectedCompetition = value; OnPropertyChanged("SelectedCompetitionTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string SelectedGenre { get => selectedGenre; set { selectedGenre = value; OnPropertyChanged("SelectedGenreTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); }  }
        public DateTime DateDP { get => dateDP; set { dateDP = value; OnPropertyChanged("DateDP"); AddCommand.RaiseCanExecuteChanged(); } }

        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            MusicPerformances = new ObservableCollection<Common.Models.MusicPerformance>(repo.RepositoryProxy.ReadMusicPerformances());
            OnPropertyChanged("MusicPerformances");
        }
    }
}
