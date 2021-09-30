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
        private MainWindowViewModel mainWindow;

        public MainMenuViewModel(MainWindowViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            NavigationCommand = new MyICommand<string>(Navigate);
        }


        private void Navigate(string view)
        {
            mainWindow.Navigate(view);
            
        }


    }
}
