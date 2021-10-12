using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Common.Models;
using System.Windows;
using System.IO;
using CsvHelper;
using System.Globalization;
using Microsoft.Win32;
using ClientUI.Resources.ImportClasses;

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
        public MyICommand ImportCSVCommand { get; set; }
        public MyICommand ExportCSVCommand { get; set; }


        private bool isEventOrganizer = false;
        public bool IsEventOrganizer { get => isEventOrganizer; set { isEventOrganizer = value; OnPropertyChanged("IsEventOrganizer"); } }

        public MusicPerformancesTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            MusicPerformances = new ObservableCollection<MusicPerformance>(repo.RepositoryProxy.ReadMusicPerformances());
            Competitions = repo.RepositoryProxy.ReadCompetitions().ToList();
            Competitors = repo.RepositoryProxy.ReadCompetitors().ToList();
            Genres = repo.RepositoryProxy.ReadGenres().ToList();
            IsEventOrganizer = LoggedInUserSingleton.Instance.CheckRole("EventOrganizer");

            foreach (Competitor cmp in Competitors)
            {
                string ime_prezime = cmp.FIRSTNAME_SIN + " " + cmp.LASTNAME_SIN;
                CompetitorStrings.Add(cmp.JMBG_SIN.ToString());
            }




            foreach(Competition cmp in Competitions)
            {
                CompetitionStrings.Add(cmp.NAME_COMP);
            }

            //sada ako nije admin vec event organizer on moze dodati samo ucesnike na svoja takmicenja i videti samo takmicenja u okviru svoje organizacije
            if (LoggedInUserSingleton.Instance.loggedInUser.Type == "EventOrganizer")
            {
                CompetitionStrings.Clear();
                EventOrganizer eotemp = repo.RepositoryProxy.ReadEventOrganizer(LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN);
                List<Organize> organizations = repo.RepositoryProxy.ReadOrganizations().ToList();

                foreach (Competition cmp in Competitions)
                {
                    Organize orgtemp = organizations.FirstOrDefault(o => o.CompetitionID_COMP == cmp.ID_COMP);
                    if(orgtemp != null)
                    {
                        if(orgtemp.PublishingHouseID_PH == eotemp.PublishingHouseID_PH)
                        {
                            CompetitionStrings.Add(cmp.NAME_COMP);
                        }
                    }
                }

                MusicPerformances.Clear();
                List<MusicPerformance> musicPerformances = repo.RepositoryProxy.ReadMusicPerformances().ToList();

                foreach(MusicPerformance mp in musicPerformances)
                {
                    if(mp.CompetitingOrganizePublishingHouseID_PH == eotemp.PublishingHouseID_PH)//moze videti samo nastupe pod svojom nadleznosti
                    {
                        MusicPerformances.Add(mp);
                    }
                }


            }


            //ako je ziri moze da vidi samo izvodjenja koja su se desila na takmicenja gde je on angazovan
            if(LoggedInUserSingleton.Instance.loggedInUser.Type == "JuryMember")
            {
                MusicPerformances.Clear();
                List<MusicPerformance> musicPerformances = repo.RepositoryProxy.ReadMusicPerformances().ToList();

                List<HiredFor> hfs = repo.RepositoryProxy.ReadEngagemenets().ToList();


                foreach (MusicPerformance mp in musicPerformances)
                {
                    HiredFor hftemp = hfs.FirstOrDefault(h => h.CompetitionID_COMP == mp.Competiting.OrganizeCompetitionID_COMP && h.JuryMemberJMBG_SIN == LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN);

                    if (hftemp != null)//moze videti samo nastupe pod svojom nadleznosti
                    {
                        MusicPerformances.Add(mp);
                    }
                }
            }

            //ako je takmicar moze videti samo svoje nastupe
            if (LoggedInUserSingleton.Instance.loggedInUser.Type == "Competitor")
            {
                MusicPerformances.Clear();
                List<MusicPerformance> musicPerformances = repo.RepositoryProxy.ReadMusicPerformances().ToList();

                foreach (MusicPerformance mp in musicPerformances)
                {
                    if (mp.CompetitingCompetitorJMBG_SIN == LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN)
                    {
                        MusicPerformances.Add(mp);
                    }
                }
            }


            foreach(Genre g in Genres)
            {
                GenreStrings.Add(g.GENRE_NAME);
            }
            OnPropertyChanged("CompetitorStrings");
            OnPropertyChanged("CompetitionStrings");
            OnPropertyChanged("GenreStrings");
            OnPropertyChanged("MusicPerformances");
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            ModifyCommand = new MyICommand(OnModify, CanModify);

            ExportCSVCommand = new MyICommand(OnExportCSV);
            ImportCSVCommand = new MyICommand(OnImportCSV);
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



        private bool AddImportedObjects(MusicPerformanceCSVImportTemplate obj)
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            List<Common.Models.Organize> orgnz = repo.RepositoryProxy.ReadOrganizations().ToList();


            long competitorId = obj.Competitor_JMBG??-1;
            int competitionId = obj.Competition_ID??-1;
            int genreID = obj.Genre_ID;


            if (competitionId == -1 || competitorId == -1 || genreID == -1)
            {
                return false;
            }
            Common.Models.Organize orgtemp = orgnz.Find(x => x.CompetitionID_COMP == competitionId && x.PublishingHouseID_PH == obj.PH_ID);
            if (orgtemp == null) { 
                return false;
            }
            
            //ako nije admin u pitanju vec EO ne sme da doda ono sto nije za njegovu izdavacku kucu
            if(LoggedInUserSingleton.Instance.loggedInUser.Type != "Administrator")
            {
                EventOrganizer eo = repo.RepositoryProxy.ReadEventOrganizer(LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN);
                if(eo.PublishingHouseID_PH != obj.PH_ID)
                {
                    return false;
                }
            }

            if (repo.RepositoryProxy.AddMusicPerformance(new MusicPerformance(-1,obj.Original_performer, obj.Song_name, obj.Song_author, obj.Performance_date, obj.Competitor_JMBG, obj.Competition_ID, obj.PH_ID,obj.Genre_ID,null,null)))
            {
                return true;
            }
            else
            {
                return false;
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

        public void OnExportCSV()
        {
            using (var writer = new StreamWriter("MusicPerformancesExport-" + LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN.ToString() + ".csv"))
            {
                List<MusicPerformance> mftemp = MusicPerformances.ToList();
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<MusicPerformanceMap>();
                    csv.WriteHeader<MusicPerformance>();
                    csv.NextRecord();
                    foreach(var mf in mftemp)
                    {
                        
                        csv.WriteRecord(mf);
                        csv.NextRecord();
                    }
                }

            }
            System.Windows.MessageBox.Show(string.Format("Exporting done."), "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void OnImportCSV()
        {
            try
            {
                bool allwentokey = true;
                string csvlocation = string.Empty;
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == true)
                    csvlocation = ofd.FileName;

                using (var reader = new StreamReader(csvlocation))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        try 
                        { 
                            List<MusicPerformanceCSVImportTemplate> records = csv.GetRecords<MusicPerformanceCSVImportTemplate>().ToList();

                            foreach(MusicPerformanceCSVImportTemplate tmp in records)
                            {
                                if (!AddImportedObjects(tmp))
                                {
                                    allwentokey = false;
                                }
                            }
                            if(allwentokey == false)
                            {
                                System.Windows.MessageBox.Show(string.Format("Some of the records ignored because of validation errors."), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                        }
                        catch (Exception e)
                        {
                            System.Windows.MessageBox.Show(string.Format("Invalid CSV file! Please, try again."), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(string.Format("Invalid CSV file! Please, try again."), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            System.Windows.MessageBox.Show(string.Format("Importing done."), "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            RefreshTable();
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

            if (LoggedInUserSingleton.Instance.loggedInUser.Type == "EventOrganizer")
            {
                
                EventOrganizer eotemp = repo.RepositoryProxy.ReadEventOrganizer(LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN);
                List<Organize> organizations = repo.RepositoryProxy.ReadOrganizations().ToList();
                MusicPerformances.Clear();
                List<MusicPerformance> musicPerformances = repo.RepositoryProxy.ReadMusicPerformances().ToList();

                foreach (MusicPerformance mp in musicPerformances)
                {
                    if (mp.CompetitingOrganizePublishingHouseID_PH == eotemp.PublishingHouseID_PH)//moze videti samo nastupe pod svojom nadleznosti
                    {
                        MusicPerformances.Add(mp);
                    }
                }


            }
            //ako je ziri moze da vidi samo izvodjenja koja su se desila na takmicenja gde je on angazovan
            if (LoggedInUserSingleton.Instance.loggedInUser.Type == "JuryMember")
            {
                MusicPerformances.Clear();
                List<MusicPerformance> musicPerformances = repo.RepositoryProxy.ReadMusicPerformances().ToList();

                List<HiredFor> hfs = repo.RepositoryProxy.ReadEngagemenets().ToList();


                foreach (MusicPerformance mp in musicPerformances)
                {
                    HiredFor hftemp = hfs.FirstOrDefault(h => h.CompetitionID_COMP == mp.Competiting.OrganizeCompetitionID_COMP && h.JuryMemberJMBG_SIN == LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN);

                    if (hftemp != null)//moze videti samo nastupe pod svojom nadleznosti
                    {
                        MusicPerformances.Add(mp);
                    }
                }
            }


            OnPropertyChanged("MusicPerformances");



        }
    }
}
