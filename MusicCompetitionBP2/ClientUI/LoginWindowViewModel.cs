using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClientUI
{
    public class LoginWindowViewModel:BindableBase
    {
        public MyICommand<LoginWindow> LoginCommand { get; private set; }
        private BindableBase currentViewModel;
        private string emailTB = "";
        

        public LoginWindowViewModel()
        {
            currentViewModel = this;
            LoginCommand = new MyICommand<LoginWindow>(LoginWindowCommand);
            
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        public string EmailTB { get => emailTB; set { emailTB = value; OnPropertyChanged("EmailTB");  } }

        public void LoginWindowCommand(LoginWindow wnd)
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            Common.Models.User loggedInUser;

            loggedInUser = repo.RepositoryProxy.ReadLoggedInUser(EmailTB, wnd.PasswordBox.Password.ToString());
            

            if(loggedInUser == null)
            {
                MessageBox.Show("Wrong email or password. Please try again.", "Invalid login credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if(loggedInUser.Password == "ChangePassword")
            {
                if (wnd.PasswordBox.Password.ToString().Contains("-FL-"))
                {
                    MessageBox.Show("Invalid password. Your password can't contain '-FL-'. Please try again.", "Invalid password", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                loggedInUser.Password = Common.PasswordHasher.Hash(wnd.PasswordBox.Password.ToString(), 10);
                repo.RepositoryProxy.EditPassword(loggedInUser);
                
            }


            if (loggedInUser.Password.Contains("-FL-"))//ako je koriscen auto generisan password
            {
                MessageBox.Show("We detected you logged in with auto-generated password. Please login again and next password you enter will be used for future login.", "Choose password.", MessageBoxButton.OK, MessageBoxImage.Information);
                loggedInUser.Password = "ChangePassword";
                repo.RepositoryProxy.EditPassword(loggedInUser);
                return;
            }

            LoggedInUserSingleton ls = LoggedInUserSingleton.Instance;
            ls.loggedInUser = loggedInUser;
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
            wnd.Close();
        }
        
    }

}
