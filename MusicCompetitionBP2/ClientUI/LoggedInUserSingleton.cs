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

        
    }
}
