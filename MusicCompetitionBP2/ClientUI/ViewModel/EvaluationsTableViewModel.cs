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

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand ModifyCommand { get; set; }

        public EvaluationsTableViewModel()
        {
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
           

            OnPropertyChanged("JuryMemberStrings");
            OnPropertyChanged("MusicPerformanceStrings");
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
            
        }
    }
}
