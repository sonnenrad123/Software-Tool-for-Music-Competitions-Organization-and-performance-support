   M S B A M L   `   `   `  ����C  ?ClientUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ClientUIClientUI  5   	   /clr-namespace:ClientUI.ViewsClientUI.Views  5   	   7 clr-namespace:ClientUI.ViewModelClientUI.ViewModel  5   	       ClientUI.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:ClientUI     5   	   .viewsclr-namespace:ClientUI.Views     5   	   6	viewModel clr-namespace:ClientUI.ViewModel     5   	   Hi:http://schemas.microsoft.com/expression/2010/interactivity     5	   	   -   5          �Name$  GlavniProzor��5   	    � Title$ 
MainWindow��5   	   $	��600��6   $	��800��6)    � WindowState$ 	Maximized=�65   $��Segoe UI Emoji)�6M    � Icon$ mic2.png��6i   ��5      �� 5   
    �� ImageSource$" Resources/Images/vocal-2.jpg��6    � DataContext 5      "   ClientUI.MainWindowViewModel  5   
    � 	Resources 5       �� DataType*   $ClientUI.ViewModel.MainMenuViewModel!   ClientUI.Views.MainMenuView2   ,ClientUI.ViewModel.CompetitorsTableViewModel)   #ClientUI.Views.CompetitorsTableView2   ,ClientUI.ViewModel.JuryMembersTableViewModel$   ClientUI.Views.JuryMembersView3   -ClientUI.ViewModel.CompetitionsTableViewModel*	   $ClientUI.Views.CompetitionsTableView-
   'ClientUI.ViewModel.GenresTableViewModel$   ClientUI.Views.GenresTableView7   1ClientUI.ViewModel.PublishingHousesTableViewModel.   (ClientUI.Views.PublishingHousesTableView7   1ClientUI.ViewModel.PerformanceHallsTableViewModel.   (ClientUI.Views.PerformanceHallsTableView8   2ClientUI.ViewModel.MusicPerformancesTableViewModel/   )ClientUI.Views.MusicPerformancesTableView3   -ClientUI.ViewModel.CompetitingsTableViewModel*   $ClientUI.Views.CompetitingsTableView1   +ClientUI.ViewModel.PossessesATableViewModel(   "ClientUI.Views.PossessesATableView2   ,ClientUI.ViewModel.EvaluationsTableViewModel)   #ClientUI.Views.EvaluationsTableView/   )ClientUI.ViewModel.IsExpertTableViewModel&    ClientUI.Views.IsExpertTableView/   )ClientUI.ViewModel.HiredForTableViewModel&    ClientUI.Views.HiredForTableView4   .ClientUI.ViewModel.OrganizationsTableViewModel+   %ClientUI.Views.OrganizationsTableView3   -ClientUI.ViewModel.ReservationsTableViewModel*   $ClientUI.Views.ReservationsTableView,   &ClientUI.ViewModel.UsersTableViewModel#!   ClientUI.Views.UsersTableView,"   &ClientUI.ViewModel.AdminPanelViewModel##   ClientUI.Views.AdminPanelView.$   (ClientUI.ViewModel.ControlPanelViewModel%%   ClientUI.Views.ControlPanelView5&   /ClientUI.ViewModel.EventOrganizerTableViewModel,'   &ClientUI.Views.EventOrganizerTableView��%�  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5"      *, +)(�� �     5%      *,
 +)(�� 	    5(      *, +)(�� @    5+      *, +)(�� w    5.      *, +)(�� �    51      *, +)(�� �    54      *, +)(��     57      *, +)(�� S    5:      *, +)(�� �    5=      *, +)(�� �    5@      *, +)(�� �    5C      *, +)(�� /    5F      *,  +)(�� f    5I      *," +)(�� �    5L      *,$ +)(�� �    5O      *,& +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5         6&   5!      �� 5"   
   "  6   .R�5#      	  6,   5$      �� 5%   
   " 
 6   .R�5&        6&   5'      �� 5(   
   "  6   .R�5)        60   5*      �� 5+   
   "  6   .R�5,        60   5-      �� 5.   
   "  6   .R�5/        61   50      �� 51   
   "  6   .R�52        6,   53      �� 54   
   "  6   .R�55        6*   56      �� 57   
   "  6   .R�58        6+   59      �� 5:   
   "  6   .R�5;        6(   5<      �� 5=   
   "  6   .R�5>        6(   5?      �� 5@   
   "  6   .R�5A        6-   5B      �� 5C   
   "  6   .R�5D        6,   5E      �� 5F   
   "   6   .R�5G      !  6%   5H      �� 5I   
   " " 6   .R�5J      #  6%   5K      �� 5L   
   " $ 6   .R�5M      %  6'   5N      �� 5O   
   " & 6   .R�5P      '  6.   5Q      5R      .��5T      �  � RowDefinitions 5U   
   �� 5V      $��50 �6   �� 5W      $
��800* �6   �� 5X      $
��200* �6   .+�5Z   
   � $��0��6   $��Right=�6   $	��Top=�69   ���	#FF44A2EB.+�5[      �� $��20(�67   ��6   �� *WelcomeMessage+6G   5\      � 5]   
   $��1��6   $��Center=�6   $��Center=�69   .+�5^      �� 	 �� Orientation$	 
Horizontal=�6   65   �� 5_      $
��Left=�6B   $��	10,10,0,0q�6]   $��Stretch=�6q   $��Stretch=�6�   ��6   �� *CurrentViewModel+6�   5`      �� 5a   
   $��2��6   $��Bottom=�6#   .+�5b      �� -   5        
 ��Name$
 HomeBtn��5b      $��	Main menu��6&   $��29��6:    J� Opacity$	 0.8Q�6F   ��6T   �� *
BackToHome+�� 5c      -   5        $
 BackBtn��5c      $
��Back��6&   $��28��65   $	 0.8Q�6A   ��6O   �� *BackCommand+�� 5d      -   5        $
 	LogOutBtn��5d      $��Log out��6(   $��28��6:   $	 0.8Q�6F   ���	#FFF00A0A��6T   �� *LogOutCommand+��6�   ��  �� ElementName$ GlavniProzor��5g      5h      5i      