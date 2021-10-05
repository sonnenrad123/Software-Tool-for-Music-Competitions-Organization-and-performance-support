using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI
{
    public class NavigationEventArgs:EventArgs
    {
        private readonly string location;

        public NavigationEventArgs(string llocation)
        {
            location = llocation;
        }

        public string Location
        {
            get { return location; }
        }
    }
}
