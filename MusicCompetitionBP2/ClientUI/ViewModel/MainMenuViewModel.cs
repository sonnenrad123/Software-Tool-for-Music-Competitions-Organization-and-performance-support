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


        public bool IsAdmin { get => isAdmin; set { isAdmin = value; OnPropertyChanged("IsAdmin");} }
        public bool IsCompetitor { get => isCompetitor; set { isCompetitor = value; OnPropertyChanged("IsCompetitor");} }
        public bool IsJuryMember { get => isJuryMember; set { isJuryMember = value; OnPropertyChanged("IsJuryMember");} }
        public bool IsEventOrganizer { get => isEventOrganizer; set { isEventOrganizer = value; OnPropertyChanged("IsEventOrganizer");} }

        public MainMenuViewModel()
        {
            IsAdmin = CheckIsAdmin();
            IsCompetitor = CheckIsCompetitor();
            IsJuryMember = CheckIsJuryMember();
            IsEventOrganizer = CheckIsEventOrganizer();
            NavigationCommand = new MyICommand<string>(Navigate);
            OnPropertyChanged("IsAdmin");
            OnPropertyChanged("IsCompetitor");
            OnPropertyChanged("IsJuryMember");
            OnPropertyChanged("IsEventOrganizer");
        }


        public void Navigate(string view) => NavigationEvent?.Invoke(this, new NavigationEventArgs(view));


        #region Rolecheckers
        public bool CheckIsAdmin()
        {
            LoggedInUserSingleton ls = LoggedInUserSingleton.Instance;
            if(ls.loggedInUser == null || ls.loggedInUser.Type != "Administrator")
            {
                return false;
            }
            return true;
        }


        public bool CheckIsCompetitor()
        {
            LoggedInUserSingleton ls = LoggedInUserSingleton.Instance;
            if (ls.loggedInUser == null || ls.loggedInUser.Type != "Competitor")
            {
                return false;
            }
            return true;
        }

        public bool CheckIsJuryMember()
        {
            LoggedInUserSingleton ls = LoggedInUserSingleton.Instance;
            if (ls.loggedInUser == null || ls.loggedInUser.Type != "JuryMember")
            {
                return false;
            }
            return true;
        }

        public bool CheckIsEventOrganizer()
        {
            LoggedInUserSingleton ls = LoggedInUserSingleton.Instance;
            if (ls.loggedInUser == null || ls.loggedInUser.Type != "EventOrganizer")
            {
                return false;
            }
            return true;
        }





        #endregion

    }
}
