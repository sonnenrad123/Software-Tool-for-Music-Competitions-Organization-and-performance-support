using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI
{
    public class LoggedInUserSingleton
    {
        private static LoggedInUserSingleton _Instance;

        public static LoggedInUserSingleton Instance
        {
            get
            {
                if(_Instance == null)
                {
                    _Instance = new LoggedInUserSingleton();
                }
                return _Instance;
            }
        }
        private LoggedInUserSingleton()
        {

        }

        public Common.Models.User loggedInUser { get; set; }

        public bool CheckRole(string role)
        {
            if (Instance.loggedInUser.Type == role || Instance.loggedInUser.Type == "Administrator")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
