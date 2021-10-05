using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI.ViewModel
{
    public class AdminPanelViewModel: BindableBase
    {
        public MyICommand<string> NavigationCommand { get; private set; }
        public event EventHandler<NavigationEventArgs> NavigationEvent;



        public AdminPanelViewModel()
        {
            NavigationCommand = new MyICommand<string>(Navigate);
        }



        public void Navigate(string view) => NavigationEvent?.Invoke(this, new NavigationEventArgs(view));
    }
}
