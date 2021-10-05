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


        public bool IsAdmin { get => isAdmin; set { isAdmin = value; OnPropertyChanged("IsAdmin");} }

        public MainMenuViewModel()
        {
            IsAdmin = CheckIsAdmin();
            NavigationCommand = new MyICommand<string>(Navigate);
            OnPropertyChanged("IsAdmin");
        }


        public void Navigate(string view) => NavigationEvent?.Invoke(this, new NavigationEventArgs(view));
        
        public bool CheckIsAdmin()
        {
            LoggedInUserSingleton ls = LoggedInUserSingleton.Instance;
            if(ls.loggedInUser == null || ls.loggedInUser.Type != "Administrator")
            {
                return false;
            }
            return true;
        }

    }
}
