using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI.ViewModel
{
    public class MainMenuViewModel : BindableBase
    {
        
        public MyICommand<string> NavigationCommand { get; private set; }
        public event EventHandler<NavigationEventArgs> NavigationEvent;
        private bool isAdmin = false;
        private bool isCompetitor = false;
        private bool isJuryMember = false;
        private bool isEventOrganizer = false;
        private string competitingsButtonText = "";

        private bool competitingButtonIsVisible = false;

        public bool CompetitingButtonIsVisible { get => competitingButtonIsVisible; set { competitingButtonIsVisible = value; OnPropertyChanged("CompetitingButtonIsVisible"); } }
        public bool IsAdmin { get => isAdmin; set { isAdmin = value; OnPropertyChanged("IsAdmin");} }
        public bool IsCompetitor { get => isCompetitor; set { isCompetitor = value; OnPropertyChanged("IsCompetitor");} }
        public bool IsJuryMember { get => isJuryMember; set { isJuryMember = value; OnPropertyChanged("IsJuryMember");} }
        public bool IsEventOrganizer { get => isEventOrganizer; set { isEventOrganizer = value; OnPropertyChanged("IsEventOrganizer");} }
        public string CompetitingsButtonText { get => competitingsButtonText; set { competitingsButtonText = value; OnPropertyChanged("CompetitingsButtonText"); } }

        public MainMenuViewModel()
        {
            IsAdmin = LoggedInUserSingleton.Instance.CheckRole("Administrator");
            IsCompetitor = LoggedInUserSingleton.Instance.CheckRole("Competitor");
            IsJuryMember = LoggedInUserSingleton.Instance.CheckRole("JuryMember");
            IsEventOrganizer = LoggedInUserSingleton.Instance.CheckRole("EventOrganizer");
            NavigationCommand = new MyICommand<string>(Navigate);
            OnPropertyChanged("IsAdmin");
            OnPropertyChanged("IsCompetitor");
            OnPropertyChanged("IsJuryMember");
            OnPropertyChanged("IsEventOrganizer");

            if (LoggedInUserSingleton.Instance.CheckRole("Administrator"))
            {
                CompetitingsButtonText = "See all competitings";
                CompetitingButtonIsVisible = true;
            }

            else if (IsCompetitor)
            {
                CompetitingsButtonText = "See my competitings";
                CompetitingButtonIsVisible = true;
            }

            else if (IsEventOrganizer)
            {
                CompetitingsButtonText = "See all competitings";
                CompetitingButtonIsVisible = true;
            }
            else if (IsJuryMember)
            {
                CompetitingButtonIsVisible = false;
            }

        }


        public void Navigate(string view) => NavigationEvent?.Invoke(this, new NavigationEventArgs(view));


        

    }
}
