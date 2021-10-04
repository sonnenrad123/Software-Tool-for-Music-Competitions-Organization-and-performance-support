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
            Common.Models.User loggedInUser = repo.RepositoryProxy.ReadLoggedInUser(EmailTB, wnd.PasswordBox.Password.ToString());

            if(loggedInUser == null)
            {
                MessageBox.Show("Wrong email or password. Please try again.", "Invalid login credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(loggedInUser.Password == null)//ako je detektovano prvo logovanje
            {
                MessageBox.Show("Your password has never been set. Your auto-generated password has been emailed to you.", "Password not set.", MessageBoxButton.OK, MessageBoxImage.Information);
                string addition = RandomString(20);
                loggedInUser.Password = "-FL-" + addition;
                repo.RepositoryProxy.EditPassword(loggedInUser);

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("mcompetition2021@gmail.com", "muzickotakmicenje"),
                    EnableSsl = true,
                };

                smtpClient.Send("mcompetition2021@gmail.com", loggedInUser.EMAIL_SIN, "Your login password for Global Music Competitions app.", string.Format("Email: {0}\nPassword:{1}", loggedInUser.EMAIL_SIN, loggedInUser.Password)); ;




                return;
            }


            if(loggedInUser.Password == "ChangePassword")
            {
                if (wnd.PasswordBox.Password.ToString().Contains("-FL-"))
                {
                    MessageBox.Show("Invalid password. Your password can't contain '-FL-'. Please try again.", "Invalid password", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                loggedInUser.Password = wnd.PasswordBox.Password.ToString();
                repo.RepositoryProxy.EditPassword(loggedInUser);
            }


            if(loggedInUser.Password != wnd.PasswordBox.Password.ToString())//ako nije dobar password nema dalje
            {
                MessageBox.Show("Wrong email or password. Please try again.", "Invalid login credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (loggedInUser.Password.Contains("-FL-"))//ako je koriscen auto generisan password
            {
                MessageBox.Show("We detected you logged in with auto-generated password. Please login again and next password you enter will be used for future login.", "Choose password.", MessageBoxButton.OK, MessageBoxImage.Information);
                loggedInUser.Password = "ChangePassword";
                repo.RepositoryProxy.EditPassword(loggedInUser);
                return;
            }
            

            MainWindow newWindow = new MainWindow();
            newWindow.Show();
            wnd.Close();
        }
        static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }
    }

}
