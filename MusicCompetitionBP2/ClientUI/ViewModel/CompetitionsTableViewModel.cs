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
    public class CompetitionsTableViewModel : BindableBase
    {
        public ObservableCollection<Common.Models.Competition> Competitions { get; set; } = new ObservableCollection<Common.Models.Competition>();
        private MainWindowViewModel mainWindow;

        private Common.Models.Competition selectedCompetition;
        private string nameTB = "";
        private string maxCompetitorsTB="";
        private DateTime startDateDP = DateTime.Now;
        private DateTime endDateDP = DateTime.Now;
       

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand ModifyCommand { get; set; }
        public CompetitionsTableViewModel(MainWindowViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Competitions = new ObservableCollection<Competition>(repo.RepositoryProxy.ReadCompetitions());
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            ModifyCommand = new MyICommand(OnModify, CanModify);
        }


        private bool CanModify()
        {
            return selectedCompetition!=null && NameTB != "" && int.TryParse(MaxCompetitorsTB, out int mc) && StartDateDP != null && EndDateDP != null && StartDateDP<=EndDateDP;
        }

        private void OnModify()
        {
            int maxcomp = -1;
            if (!(int.TryParse(MaxCompetitorsTB, out maxcomp)))
            {
                System.Windows.MessageBox.Show("Max competitors must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.EditCompetition(new Competition(SelectedCompetition.ID_COMP, startDateDP, endDateDP, nameTB, maxcomp));
            RefreshTable();
        }

        private bool CanAdd()
        {
            return (NameTB != "" && int.TryParse(MaxCompetitorsTB, out int mc) && StartDateDP != null && EndDateDP != null && StartDateDP<=EndDateDP);
        }

        private void OnAdd()
        {
            int maxcomp = -1;
            if(!(int.TryParse(MaxCompetitorsTB,out maxcomp)))
            {
                System.Windows.MessageBox.Show("Max competitors must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.AddCompetition(new Competition(-1, startDateDP, endDateDP, NameTB, maxcomp));
            RefreshTable();
        }

        private bool CanDelete()
        {
            return SelectedCompetition != null;
        }

        private void OnDelete()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            repo.RepositoryProxy.DeleteCompetition(selectedCompetition.ID_COMP);
            RefreshTable();
        }

        public Common.Models.Competition SelectedCompetition
        {
            get => selectedCompetition;
            set
            {
                selectedCompetition = value;

                if(selectedCompetition != null)
                {
                    NameTB = selectedCompetition.NAME_COMP;
                    MaxCompetitorsTB = selectedCompetition.MAX_COMPETITORS.ToString();
                    StartDateDP = selectedCompetition.DATE_START;
                    EndDateDP = selectedCompetition.DATE_END;
                    OnPropertyChanged("NameTB");
                    OnPropertyChanged("MaxCompetitorsTB");
                    OnPropertyChanged("StartDateDP");
                    OnPropertyChanged("EndDateDP");
                }


                DeleteCommand.RaiseCanExecuteChanged();
                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
            }
        }

        public string NameTB
        {
            get
            {
                return nameTB;
            }
            set
            {
                nameTB = value;
                OnPropertyChanged("NameTB");
                AddCommand.RaiseCanExecuteChanged();
                ModifyCommand.RaiseCanExecuteChanged();
            }
        }
        public string MaxCompetitorsTB { get => maxCompetitorsTB; set { maxCompetitorsTB = value; OnPropertyChanged("MaxCompetitorsTB"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public DateTime StartDateDP { get => startDateDP; set { startDateDP = value; OnPropertyChanged("StartDateDP"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }
        public DateTime EndDateDP { get => endDateDP; set { endDateDP = value; OnPropertyChanged("EndDateDP"); AddCommand.RaiseCanExecuteChanged(); ModifyCommand.RaiseCanExecuteChanged(); } }

        private void RefreshTable()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            List<Competition> templist;
            templist = repo.RepositoryProxy.ReadCompetitions().ToList();
            Competitions = new ObservableCollection<Competition>(templist);
            OnPropertyChanged("Competitions");
        }
    }
}
