using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
namespace ClientUI.ViewModel
{
    public class IsExpertTableViewModel:BindableBase
    {
        public ObservableCollection<Common.Models.IsExpert> IsExpertSet { get; set; } = new ObservableCollection<Common.Models.IsExpert>();
        private Common.Models.IsExpert selectedExpertise;
        private string selectedJuryMember;
        private string selectedGenre;

        public List<string> GenreStrings { get; set; } = new List<string>();
        public List<string> JuryMemberStrings { get; set; } = new List<string>();

        public List<Common.Models.Genre> Genres;
        public List<Common.Models.JuryMember> JuryMembers;

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }

        public IsExpertTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            IsExpertSet = new ObservableCollection<Common.Models.IsExpert>(repo.RepositoryProxy.ReadExpertises());
            Genres = repo.RepositoryProxy.ReadGenres().ToList();
            JuryMembers = repo.RepositoryProxy.ReadJuryMembers().ToList();
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);

            foreach(Common.Models.Genre cmp in Genres)
            {
                GenreStrings.Add(cmp.GENRE_NAME);
            }

            foreach(Common.Models.JuryMember jr in JuryMembers)
            {
                JuryMemberStrings.Add(jr.JMBG_SIN.ToString());
            }

            OnPropertyChanged("GenreStrings");
            OnPropertyChanged("JuryMemberStrings");
        }



        public Common.Models.IsExpert SelectedExpertise
        {
            get
            {
                return selectedExpertise;
            }
            set
            {
                selectedExpertise = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public string SelectedJuryMember { get => selectedJuryMember; set { selectedJuryMember = value; OnPropertyChanged("SelectedJuryMember"); AddCommand.RaiseCanExecuteChanged(); } }
        public string SelectedGenre { get => selectedGenre; set { selectedGenre = value; OnPropertyChanged("SelectedGenre"); AddCommand.RaiseCanExecuteChanged(); } }


        private bool CanAdd()
        {
            Common.Models.Genre gt = null;
            foreach (Common.Models.Genre gtemp in Genres)
            {
                if(gtemp.GENRE_NAME == selectedGenre)
                {
                    gt = gtemp;
                }
            }

            if(gt == null)
            {
                return false;
            }

            if(long.TryParse(SelectedJuryMember,out long jmbg))
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

            Common.Models.Genre gt = null;
            foreach (Common.Models.Genre gtemp in Genres)
            {
                if (gtemp.GENRE_NAME == selectedGenre)
                {
                    gt = gtemp;
                }
            }

            if(gt == null)
            {
                System.Windows.MessageBox.Show("There was a problem! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if(long.TryParse(SelectedJuryMember,out jmbg)){
                if (repo.RepositoryProxy.AddGenreExpertise(gt.ID_GENRE, jmbg))
                {
                    RefreshTable();
                }
                else
                {
                    System.Windows.MessageBox.Show("There was a problem. Choose another JMBG and ID! Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            return SelectedExpertise != null;
        }

        private void OnDelete()
        {
            if (SelectedExpertise != null)
            {
                RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
                repo.RepositoryProxy.DeleteExpertise(selectedExpertise.JuryMemberJMBG_SIN, selectedExpertise.GenreID_GENRE);
                RefreshTable();
            }
        }
        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            IsExpertSet = new ObservableCollection<Common.Models.IsExpert>(repo.RepositoryProxy.ReadExpertises());
            OnPropertyChanged("IsExpertSet");
        }

    }


}
