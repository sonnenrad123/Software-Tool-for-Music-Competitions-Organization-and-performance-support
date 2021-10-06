﻿using ClientUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI
{
    public class MainWindowViewModel: BindableBase
    {
        


        public MyICommand<string> MenuCommand { get; private set; }
        public MyICommand<MainWindow> LogOutCommand { get; private set; }
       
        public MyICommand BackCommand { get; set; }
        public MyICommand BackToHome { get; set; }

        private BindableBase currentViewModel;
        private MainMenuViewModel MainMenu;
        



        //Lista u kojoj se cuvaju izvrsene akcije
        public static List<Akcija> akcije = new List<Akcija>();


       


        public MainWindowViewModel()
        {
            MainMenu = new MainMenuViewModel();
            MainMenu.NavigationEvent += (object sender, NavigationEventArgs e) => Navigate(e.Location);
            CurrentViewModel = MainMenu;
            MenuCommand = new MyICommand<string>(Navigate);
            BackToHome = new MyICommand(Home);
            BackCommand = new MyICommand(Back);

            LogOutCommand = new MyICommand<MainWindow>(LogOut);
        }


        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }


        public void Home()
        {
            CurrentViewModel = MainMenu;
        }


        private void Back()
        {
            
        }


        public void Navigate(string view)
        {
            akcije.Add(new Akcija("Navigacija", currentViewModel));//cuvamo prethodni view
            switch (view)
            {
                case "AdminPanel":
                    AdminPanelViewModel admtemp = new AdminPanelViewModel();
                    admtemp.NavigationEvent += (object sender, NavigationEventArgs e) => Navigate(e.Location);
                    CurrentViewModel = admtemp;
                    break;
                case "ControlPanel":
                    CurrentViewModel = new ControlPanelViewModel();
                    break;
                case "Users":
                    CurrentViewModel = new UsersTableViewModel();
                    break;
                case "Competitors":
                    CurrentViewModel = new CompetitorsTableViewModel();
                    break;
                case "JuryMembers":
                    CurrentViewModel = new JuryMembersTableViewModel();
                    break;
                case "EventOrganizers":
                    CurrentViewModel = new EventOrganizerTableViewModel();
                    break;
                case "Competitions":
                    CurrentViewModel = new CompetitionsTableViewModel(this);
                    break;
                case "Genres":
                    CurrentViewModel = new GenresTableViewModel();
                    break;
                case "PublishingHouses":
                    CurrentViewModel = new PublishingHousesTableViewModel();
                    break;
                case "PerformanceHalls":
                    CurrentViewModel = new PerformanceHallsTableViewModel();
                    break;
                case "MusicPerformances":
                    CurrentViewModel = new MusicPerformancesTableViewModel();
                    break;
                case "Competitings":
                    CurrentViewModel = new CompetitingsTableViewModel();
                    break;
                case "PossessesA":
                    CurrentViewModel = new PossessesATableViewModel();
                    break;
                case "Evaluations":
                    CurrentViewModel = new EvaluationsTableViewModel();
                    break;
                case "IsExpertSet":
                    CurrentViewModel = new IsExpertTableViewModel();
                    break;
                case "Engagements":
                    CurrentViewModel = new HiredForTableViewModel();
                    break;
                case "Organizations":
                    CurrentViewModel = new OrganizationsTableViewModel();
                    break;
                case "Reservations":
                    CurrentViewModel = new ReservationsTableViewModel();
                    break;

            }
        }

        public void LogOut(MainWindow wnd)
        {
            LoggedInUserSingleton.Instance.loggedInUser = null;
            LoginWindow lw = new LoginWindow();
            lw.Show();
            wnd.Close();
            
        }
    }
}
