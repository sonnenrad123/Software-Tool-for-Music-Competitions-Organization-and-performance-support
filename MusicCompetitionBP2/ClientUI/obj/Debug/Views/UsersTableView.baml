   M S B A M L   `   `   `  ����C  ?ClientUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ClientUI.ViewsClientUI.Views  5      #    ClientUI.Views.UsersTableView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:ClientUI.Views     5      $��Segoe UI Emoji)�5   7   .��5	      �   � RowDefinitions  5
   
   �� 5      $
��100* �6   6,   �� 5      $
��650* �6   6,   �� 5      $
��350* �6   6,   .+�5   
   � 
���   � J� Opacity$	 0.8Q�6"   $��0��60   .+�5      �� $��	All users��6   $	��Top=�6)   $��Center=�6A   
�������$��Italic$�6r   $��Aharoni)�6�   $��22(�6�   5      � 5   
   $��1��6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ tabela��5         AutoGenerateColumns .     
IsReadOnly .    RowBackground
 �����   CanUserAddRows .  ���#87CEFA$	 0.8Q�6  $	��500��6"  ��6p   ��  �� Path$ Singers� �� UpdateSourceTrigger$ PropertyChanged=�	 �� SelectedItem	 6�   �� *SelectedCompetitor+
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 JMBG��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6x   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *
JMBG_SIN+$ PropertyChanged=�6x     5      $ 
First Name��6)   $ * 6_    6=   �� *FIRSTNAME_SIN+6_     5      $ 	Last Name��6)   $ * 6]    6<   �� *LASTNAME_SIN+6]     5      $ Date of birth��6)   $ * 6|    6@   �� *BIRTHDATE_SIN+ �� StringFormat$ 
dd.MM.yyyy��6|     5      $ Email��6)   $ * 6V    68   �� *	EMAIL_SIN+6V     5      $ 	Phone No.��6)   $ * 6]    6<   �� *PHONE_NO_SIN+6]     5      $ Address��6)   $ * 6Z    6:   �� *ADDRESS_SIN+6Z     5      $ Competitor/Jury Member��6)   $
 auto 6b    6I   �� *Type+6b   5"      5#      5(      5)      