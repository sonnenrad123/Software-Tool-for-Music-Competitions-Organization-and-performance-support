   M S B A M L   `   `   `  ����C  ?ClientUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ClientUI.ViewsClientUI.Views  5      ,    &ClientUI.Views.EventOrganizerTableView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:ClientUI.Views     5      $��Segoe UI Emoji)�5   9   .��5	      � 
������   � RowDefinitions  5
   
   �� 5      $
��100* �6   6,   �� 5      $
��650* �6   6,   �� 5      $
��450* �6   6,   .+�5   
   � 
���   � J� Opacity$	 0.8Q�6"   $��0��60   .+�5      �� $��Event organizers��6   $	��Top=�60   $��Center=�6H   
�������$��Italic$�6y   $��Aharoni)�6�   $��22(�6�   5      � 5   
   $��1��6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ tabela��5         AutoGenerateColumns .     
IsReadOnly .    RowBackground
 �����   CanUserAddRows .  ���#87CEFA$	 0.8Q�6   $	��500��6.  ��6p   ��  �� Path$ EventOrganizers� �� UpdateSourceTrigger$ PropertyChanged=�	 �� SelectedItem	 6�   �� *SelectedEventOrganizer+
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 JMBG��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *
JMBG_SIN+6T     5      $ 
First Name��6)   $ * 6_    6=   �� *FIRSTNAME_SIN+6_     5      $ 	Last Name��6)   $ * 6]    6<   �� *LASTNAME_SIN+6]     5      $ Date of birth��6)   $ * 6|    6@   �� *BIRTHDATE_SIN+ �� StringFormat$ 
dd.MM.yyyy��6|     5      $ Email��6)   $ * 6V    68   �� *	EMAIL_SIN+6V     5      $ 	Phone No.��6)   $ * 6]    6<   �� *PHONE_NO_SIN+6]     5      $ Address��6)   $ * 6Z    6:   �� *ADDRESS_SIN+6Z     5      $ 	P.H. Name��6)   $ * 6h    6<   �� *PublishingHouse.NAME_PH+6h   5"      5#      �� 5$   
    �� Orientation$ 
Horizontal=�6   $��2��6.   ���	#993E3EE8$	��400��6R   .+�5%      �� $��2��6   .+�5&      �� 
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   $��2q�6a   .��6l   JMBG:6s   �� 5'      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   $��2q�6a   .��6l   First name:6y   �� 5(      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   
Last name:6m   �� 5)      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   Date of birth:6q   �� 5*      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   Email:6i   �� 5+      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   
Phone No.:6m   �� 5,      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   City:6h   �� 5-      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   Street name:6o   �� 5.      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   	Number:6j   �� 5/      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   
P.H. Name:6m   50      �� 51      $��2��6   .+�52      �� -   5        $	��100��52      ���#87CEFA$��4q�6�   $	��200��6�   ��6?   �� *JmbgTB+ �� Mode$ TwoWay=�6�   �� 53      $	��100��6   ���#87CEFA$��4q�6j   $	��200��6u   ��6@   �� *FirstNameTB+$ TwoWay=�6u   �� 54      $	��100��6   ���#87CEFA$��4q�6h   $	��200��6s   ��6?   �� *
LastNameTB+$ TwoWay=�6s   (  "System.Windows.Controls.DatePicker  55      $��1q�6   ���#87CEFA   SelectedDate 6@   �� *	BirthDP+$ TwoWay=� � 	Resources 56      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value��%�   '        �� 57      "  6    .�58      ��  �@��
IsReadOnly . 6;   �� 59       � �� �#87CEFA6;   5:      5;      5<      �� 5=      ���#87CEFA$	��100��6/   $��3q�6d   $	��200��6o   ��6>   �� *	EmailTB+$ TwoWay=�6o   �� 5>      -   5        ���#87CEFA$	��100��5>   0   $��3q�6g   $	��200��6r   ��6?   �� *	PhoneNoTB+$ TwoWay=�6�   �� 5?      -   5         ��Name$ CityCB��5?   C   ���#87CEFA �� 
IsEditable . $��4q�6S   $	��200��6^   ��6j   �� *CityStrings+$ TwoWay=�$ PropertyChanged=�	 6�   �� *SelectedCity+6�   �� 5@      ���#87CEFA$	��100��6/   $��3q�6e   $	��200��6p   ��6>   �� *
StreetTB+$ TwoWay=�6p   �� 5A      -   5        ���#87CEFA$	��100��5A   /   $��3q�6e   $	��200��6p   ��6>   �� *
NumberTB+$ TwoWay=�6   �� 5B      -   5        $
 PhCB��5B   ^    . ���#87CEFA$��3q�6B   $	��200��6N   ��6m   �� *PublishingHouseStrings+$ TwoWay=�$ PropertyChanged=�	 6�   �� *SelectedPublishingHouse+6�   5C      �� 5D      $��2��6   $��Center=�6&   .+�5E      �� -   5         ��Name$ AddBtn��5E      $��Add new��6)   $
��Left=�6;   $��0q�6V   $��2��6a   $	��Top=�6n   $	��133��6�   ���	#993E3EE8���	#FFFAFB79��6�   �� *
AddCommand+�� 5F      -   5        $ 	ModifyBtn��5F      $��Modify selected��6,   $
��Left=�6F   $��0q�6a   $��2��6l   $	��Top=�6y   $	��133��6�   ���	#993E3EE8���	#FFFAFB79��6�   �� *ModifyCommand+�� 5G      -	   5        $ 	DeleteBtn��5G      $��Delete selected��6,   $
��Left=�6F   $��0q�6a   $��2��6l   $	��Top=�6y   $	��133��6�   ���	#993E3EE8���	#FFFAFB79��6�   �� *DeleteCommand+5H      5I      5K      5L      