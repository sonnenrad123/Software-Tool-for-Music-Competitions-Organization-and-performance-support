using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Common.Models;

namespace ClientUI.ViewModel
{
    public class GenresTableViewModel:BindableBase
    {
        public ObservableCollection<Common.Models.Genre> Genres { get; set; } = new ObservableCollection<Common.Models.Genre>();
        

        private Common.Models.Genre selectedGenre;
        private string nameTB = "";



        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand ModifyCommand { get; set; }
        public GenresTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Genres = new ObservableCollection<Genre>(repo.RepositoryProxy.ReadGenres());
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            ModifyCommand = new MyICommand(OnModify, CanModify);
        }

        public Common.Models.Genre SelectedGenre
        {
            get
            {
                return selectedGenre;
            }
            set
            {
                selectedGenre = value;
                if(SelectedGenre != null)
                {
                    NameTB = selectedGenre.GENRE_NAME;
                    OnPropertyChanged("NameTB");
                    
                }
                DeleteCommand.RaiseCanExecuteChanged();
                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
            }
        }


        private bool CanModify()
        {
            return SelectedGenre != null && nameTB != "";
        }

        private void OnModify()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.EditGenre(new Genre(selectedGenre.ID_GENRE, NameTB));
            RefreshTable();
        }

        private bool CanAdd()
        {
            return (NameTB != "");

        }

        private void OnAdd()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.AddGenre(new Genre(-1, NameTB));
            RefreshTable();
        }

        private bool CanDelete()
        {
            return SelectedGenre != null;
        }

        private void OnDelete()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.DeleteGenre(selectedGenre.ID_GENRE);
            RefreshTable();
        }


        public string NameTB { get => nameTB; set { nameTB = value; OnPropertyChanged("NameTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Genres = new ObservableCollection<Common.Models.Genre>(repo.RepositoryProxy.ReadGenres());
            OnPropertyChanged("Genres");
        }
    }
}
