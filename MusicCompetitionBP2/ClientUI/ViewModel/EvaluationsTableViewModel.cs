using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
namespace ClientUI.ViewModel
{
    public class EvaluationsTableViewModel:BindableBase
    {
        public ObservableCollection<Common.Models.Evaluate> Evaluations { get; set; } = new ObservableCollection<Common.Models.Evaluate>();
        private Common.Models.Evaluate selectedEvaluation;
        private string selectedJuryMember = "";
        private string selectedMusicPerformance = "";

        private string mfGenre = "";
        private string markTB = "";
        private string commentTB = "";

        public List<string> JuryMemberStrings { get; set; } = new List<string>();
        public List<string> MusicPerformanceStrings { get; set; } = new List<string>();

        public List<Common.Models.MusicPerformance> MusicPerformances;
        public List<Common.Models.IsExpert> IsExpertSet;
        public List<Common.Models.JuryMember> JuryMembers;

        private bool isJuryMember = false;
        public bool IsJuryMember { get => isJuryMember; set { isJuryMember = value; OnPropertyChanged("IsJuryMember"); } }

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand ModifyCommand { get; set; }

        public EvaluationsTableViewModel()
        {
            IsJuryMember = LoggedInUserSingleton.Instance.CheckRole("JuryMember");
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Evaluations = new ObservableCollection<Common.Models.Evaluate>(repo.RepositoryProxy.ReadEvaluations());
            MusicPerformances = repo.RepositoryProxy.ReadMusicPerformances().ToList();
            IsExpertSet = repo.RepositoryProxy.ReadExpertises().ToList();
            JuryMembers = repo.RepositoryProxy.ReadJuryMembers().ToList();

            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            ModifyCommand = new MyICommand(OnModify, CanModify);

            foreach (Common.Models.MusicPerformance mf in MusicPerformances)
            {
                MusicPerformanceStrings.Add(mf.ID_PERF.ToString());
            }

            if (LoggedInUserSingleton.Instance.loggedInUser.Type == "JuryMember")//ako je u pitanju ziri on vidi ocene za takmicenja na kojima je zaduzen i ocenjuje ucesnike istih
            {
                JuryMemberStrings.Clear();
                MusicPerformanceStrings.Clear();


                JuryMemberStrings.Add(LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN.ToString());
                SelectedJuryMember = JuryMemberStrings[0];
                foreach (Common.Models.MusicPerformance mf in MusicPerformances)//dajemo mu u opticaj samo takmicenja za koja je ekspert i na kojima je zaduzen
                {
                    // da li je ekspert
                    Common.Models.IsExpert isexp = IsExpertSet.FirstOrDefault(i => i.GenreID_GENRE == mf.GenreID_GENRE && i.JuryMemberJMBG_SIN == LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN);
                    if (isexp != null)
                    {
                        //da li je angazovan za odredjeno takmicenje
                        Common.Models.HiredFor hftemp = repo.RepositoryProxy.ReadEngagemenets().FirstOrDefault(e => e.CompetitionID_COMP == mf.CompetitingOrganizeCompetitionID_COMP && e.JuryMemberJMBG_SIN == LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN);
                        if (hftemp != null)
                        {
                            MusicPerformanceStrings.Add(mf.ID_PERF.ToString());
                        }
                    }


                }

            }


            //filtriranje tabele

            //ako je ziri member vidi sve ocene koje je on dodelio
            if (LoggedInUserSingleton.Instance.loggedInUser.Type == "JuryMember")
            {
                List<Common.Models.Evaluate> evalstemp = repo.RepositoryProxy.ReadEvaluations().ToList();
                Evaluations.Clear();

                foreach(Common.Models.Evaluate et in evalstemp)
                {
                    if(et.IsExpertJuryMemberJMBG_SIN == LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN)
                    {
                        Evaluations.Add(et);
                    }
                }
                OnPropertyChanged("Evaluations");
            }


            //ako je eventorganizer vidi samo ocene za takmicare u okviru njegove izdavacke kuce
            if (LoggedInUserSingleton.Instance.loggedInUser.Type == "EventOrganizer")
            {
                Common.Models.EventOrganizer eotemp = repo.RepositoryProxy.ReadEventOrganizer(LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN);
                List<Common.Models.Evaluate> evalstemp = repo.RepositoryProxy.ReadEvaluations().ToList();
                Evaluations.Clear();

                foreach (Common.Models.Evaluate et in evalstemp)
                {
                    if (et.MusicPerformance.CompetitingOrganizePublishingHouseID_PH == eotemp.PublishingHouseID_PH)
                    {
                        Evaluations.Add(et);
                    }
                }
                OnPropertyChanged("Evaluations");
            }

            //ako je competitor vidi svoje ocene
            if (LoggedInUserSingleton.Instance.loggedInUser.Type == "Competitor")
            {

                List<Common.Models.Evaluate> evalstemp = repo.RepositoryProxy.ReadEvaluations().ToList();
                Evaluations.Clear();

                foreach (Common.Models.Evaluate et in evalstemp)
                {
                    if (et.MusicPerformance.CompetitingCompetitorJMBG_SIN == LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN)
                    {
                        Evaluations.Add(et);
                    }
                }
                OnPropertyChanged("Evaluations");
            }



            OnPropertyChanged("JuryMemberStrings");
            OnPropertyChanged("MusicPerformanceStrings");
            OnPropertyChanged("SelectedJuryMember");
        }


        public Common.Models.Evaluate SelectedEvaluation
        {
            get
            {
                return selectedEvaluation;
            }
            set
            {
                selectedEvaluation = value;
                if(selectedEvaluation != null)
                {
                    SelectedJuryMember = selectedEvaluation.IsExpertJuryMemberJMBG_SIN.ToString();
                    SelectedMusicPerformance = selectedEvaluation.MusicPerformanceID_PERF.ToString();
                    MarkTB = selectedEvaluation.MARK.ToString();
                    CommentTB = selectedEvaluation.COMMENT.ToString();
                }
                DeleteCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
            }
        }


        private bool CanModify()
        {
            if(selectedEvaluation != null && selectedJuryMember == selectedEvaluation.IsExpertJuryMemberJMBG_SIN.ToString() && selectedMusicPerformance == selectedEvaluation.MusicPerformanceID_PERF.ToString())
            {
                int mark = -1;
                if (int.TryParse(MarkTB, out mark))
                {
                    if (JuryMemberStrings.Contains(SelectedJuryMember) && MusicPerformanceStrings.Contains(SelectedMusicPerformance) && CommentTB != "" && mark > 0 && mark < 11)
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
     



        public string SelectedMusicPerformance
        {
            get
            {
                return selectedMusicPerformance;
            }
            set
            {
                selectedMusicPerformance = value;
                JuryMemberStrings = new List<string>();
                if (int.TryParse(SelectedMusicPerformance,out int MFID))
                {
                    RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
                    Common.Models.MusicPerformance tempmf = repo.RepositoryProxy.ReadMusicPerformance(MFID);

                    foreach (Common.Models.IsExpert jr in IsExpertSet)
                    {
                        if(jr.GenreID_GENRE == tempmf.GenreID_GENRE)
                        {
                            JuryMemberStrings.Add(jr.JuryMemberJMBG_SIN.ToString());
                        }
                    }

                    if (LoggedInUserSingleton.Instance.loggedInUser.Type == "JuryMember")//ako je u pitanju ziri on vidi ocene za takmicenja na kojima je zaduzen i ocenjuje ucesnike istih
                    {
                        JuryMemberStrings.Add(LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN.ToString());
                    }


                    OnPropertyChanged("JuryMemberStrings");
                    OnPropertyChanged("SelectedMusicPerformance");
                    
                    OnPropertyChanged("SelectedJuryMember");
                }
                else
                {
                    JuryMemberStrings.Clear();
                    OnPropertyChanged("JuryMemberStrings");
                    OnPropertyChanged("SelectedMusicPerformance");
                }
                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();

            }
        }

        public string SelectedJuryMember
        {
            get
            {
                return selectedJuryMember;
            }
            set
            {
                selectedJuryMember = value;
                OnPropertyChanged("SelectedJuryMember");
                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
            }
        }

        public string MarkTB { get => markTB; set { markTB = value; OnPropertyChanged("MarkTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public string CommentTB { get => commentTB; set { commentTB = value; OnPropertyChanged("CommentTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }

        private bool CanAdd()
        {
            int mark = -1;
            if (int.TryParse(MarkTB, out mark))
            {
                if (JuryMemberStrings.Contains(SelectedJuryMember) && MusicPerformanceStrings.Contains(SelectedMusicPerformance) && CommentTB != "" && mark > 0 && mark < 11)
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

        private void OnModify()
        {
            if (selectedEvaluation != null && selectedJuryMember == selectedEvaluation.IsExpertJuryMemberJMBG_SIN.ToString() && selectedMusicPerformance == selectedEvaluation.MusicPerformanceID_PERF.ToString())
            {
                int mark = -1;
                if (int.TryParse(MarkTB, out mark))
                {
                    if (JuryMemberStrings.Contains(SelectedJuryMember) && MusicPerformanceStrings.Contains(SelectedMusicPerformance) && CommentTB != "" && mark > 0 && mark < 11)
                    {
                        RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
                        repo.RepositoryProxy.EditEvaluation((short)mark, CommentTB, long.Parse(selectedJuryMember), int.Parse(selectedMusicPerformance));
                        RefreshTable();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Mark must be in range [1,10]! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {

                    System.Windows.MessageBox.Show("Mark must be a number! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {

                System.Windows.MessageBox.Show("There was a problem! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void OnAdd()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();

            short mark;
            long jurymember;
            int mfid;

            if(int.TryParse(SelectedMusicPerformance,out mfid) && long.TryParse(selectedJuryMember,out jurymember) && short.TryParse(MarkTB,out mark))
            {
                foreach(Common.Models.MusicPerformance mftemp in MusicPerformances)
                {
                    if(mftemp.ID_PERF == mfid)
                    {
                        if (repo.RepositoryProxy.AddEvaluationForMusicPerformance(jurymember, mftemp.GenreID_GENRE, mfid, mark, CommentTB))
                        {
                            RefreshTable();
                        }
                        else
                        {

                            System.Windows.MessageBox.Show("Evaluation for that performance already exists! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;

                        }
                    }
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
            return SelectedEvaluation != null;
        }

        private void OnDelete()
        {
            if (SelectedEvaluation != null)
            {
                RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
                repo.RepositoryProxy.DeleteEvaluation(selectedEvaluation.IsExpertJuryMemberJMBG_SIN, selectedEvaluation.MusicPerformanceID_PERF);
                RefreshTable();
            }
        }
        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Evaluations = new ObservableCollection<Common.Models.Evaluate>(repo.RepositoryProxy.ReadEvaluations());
            OnPropertyChanged("Evaluations");
            //ako je ziri member vidi sve ocene koje je on dodelio
            if (LoggedInUserSingleton.Instance.loggedInUser.Type == "JuryMember")
            {
                List<Common.Models.Evaluate> evalstemp = repo.RepositoryProxy.ReadEvaluations().ToList();
                Evaluations.Clear();

                foreach (Common.Models.Evaluate et in evalstemp)
                {
                    if (et.IsExpertJuryMemberJMBG_SIN == LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN)
                    {
                        Evaluations.Add(et);
                    }
                }
                OnPropertyChanged("Evaluations");
            }
            //ako je eventorganizer vidi samo ocene za takmicare u okviru njegove izdavacke kuce
            if (LoggedInUserSingleton.Instance.loggedInUser.Type == "EventOrganizer")
            {
                Common.Models.EventOrganizer eotemp = repo.RepositoryProxy.ReadEventOrganizer(LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN);
                List<Common.Models.Evaluate> evalstemp = repo.RepositoryProxy.ReadEvaluations().ToList();
                Evaluations.Clear();

                foreach (Common.Models.Evaluate et in evalstemp)
                {
                    if (et.MusicPerformance.CompetitingOrganizePublishingHouseID_PH == eotemp.PublishingHouseID_PH)
                    {
                        Evaluations.Add(et);
                    }
                }
                OnPropertyChanged("Evaluations");
            }
            //ako je competitor vidi svoje ocene
            if (LoggedInUserSingleton.Instance.loggedInUser.Type == "Competitor")
            {
                
                List<Common.Models.Evaluate> evalstemp = repo.RepositoryProxy.ReadEvaluations().ToList();
                Evaluations.Clear();

                foreach (Common.Models.Evaluate et in evalstemp)
                {
                    if (et.MusicPerformance.CompetitingCompetitorJMBG_SIN == LoggedInUserSingleton.Instance.loggedInUser.JMBG_SIN)
                    {
                        Evaluations.Add(et);
                    }
                }
                OnPropertyChanged("Evaluations");
            }

        }
    }
}
