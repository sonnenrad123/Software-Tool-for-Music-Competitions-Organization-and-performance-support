   M S B A M L   `   `   `  ����C  ?ClientUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ClientUI.ViewsClientUI.Views  5      &     ClientUI.Views.IsExpertTableView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:ClientUI.Views     5      $��Segoe UI Emoji)�5   7   .��5	      � 
������   � RowDefinitions  5
   
   �� 5      $
��100* �6   6,   �� 5      $
��650* �6   6,   �� 5      $
��350* �6   6,   .+�5   
   � 
���   � J� Opacity$	 0.8Q�6"   $��0��60   .+�5      �� $��
Expertises��6   $	��Top=�6*   $��Center=�6B   
�������$��Italic$�6s   $��Aharoni)�6�   $��22(�6�   5      � 5   
   $��1��6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ tabela��5         AutoGenerateColumns .     
IsReadOnly .    RowBackground
 �����   CanUserAddRows .  ���#87CEFA$	 0.8Q�6  $	��500��6$  ��6p   ��  �� Path$ IsExpertSet� �� UpdateSourceTrigger$ PropertyChanged=�	 �� SelectedItem	 6�   �� *SelectedExpertise+
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Jury member JMBG��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� *JuryMemberJMBG_SIN+$ PropertyChanged=�6�     5      $ Jury FirstName��6)   $ * 6n    6A   �� *JuryMember.FIRSTNAME_SIN+6n     5      $ Jury LastName��6)   $ * 6l    6@   �� *JuryMember.LASTNAME_SIN+6l     5      $ Genre ID��6)   $ * 6�    6;   �� *GenreID_GENRE+$ PropertyChanged=�6�     5      $ 
Genre Name��6)   $ * 6b    6=   �� *Genre.GENRE_NAME+6b   5      5       �� 5!   
    �� Orientation$ 
Horizontal=�6   $��2��6.   ���	#993E3EE8.+�5"      �� $��2��6   .+�5$      �� 
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   Genre name:6n   �� 5%      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   Jury Member JMBG:6t   5&      �� 5'      $��2��6   .+�5(      �� -   5         ��Name$ GenreCB��5(   B   ���#87CEFA �� 
IsEditable . $��4q�6S   $	��150��6^   ��6j   �� *GenreStrings+ �� Mode$ TwoWay=�$ PropertyChanged=�	 6�   �� *SelectedGenre+6�   �� 5)      -   5        $ CompetitionCB��5)   B   ���#87CEFA . $��4q�6Y   ��6d   �� *JuryMemberStrings+$ TwoWay=�$ PropertyChanged=�	 6�   �� *SelectedJuryMember+6�   5*      5,      �� 5-   
   -   5         ��Name$ AddBtn��5-      $��Add new��6!   $
��Left=�63   $��	372,9,0,0q�6N   $��2��6a   $	��Top=�6n   $	��133��6�   ���	#993E3EE8���	#FFFAFB79��6�   �� *
AddCommand+�� 5.   
   -   5        $ 	DeleteBtn��5.      $��Delete selected��6$   $
��Left=�6>   $��
372,46,0,0q�6Y   $��2��6m   $	��Top=�6z   $	��133��6�   ���	#993E3EE8���	#FFFAFB79��6�   �� *DeleteCommand+5/      50      