   M S B A M L   `   `   `  ����C  ?ClientUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ClientUI.ViewsClientUI.Views  5      )    #ClientUI.Views.EvaluationsTableView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:ClientUI.Views     5      $��Segoe UI Emoji)�5   7   .��5	      � 
������   � RowDefinitions  5
   
   �� 5      $
��100* �6   6,   �� 5      $
��650* �6   6,   �� 5      $
��350* �6   6,   .+�5   
   � 
���   � J� Opacity$	 0.8Q�6"   $��0��60   .+�5      �� $��Evaluations��6   $	��Top=�6+   $��Center=�6C   
�������$��Italic$�6t   $��Aharoni)�6�   $��22(�6�   5      � 5   
   $��1��6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ tabela��5         AutoGenerateColumns .     
IsReadOnly .    RowBackground
 �����   CanUserAddRows .  ���#87CEFA$	 0.8Q�6  $	��500��6%  ��6p   ��  �� Path$ Evaluations� �� UpdateSourceTrigger$ PropertyChanged=�	 �� SelectedItem	 6�   �� *SelectedEvaluation+
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Music Performance ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   �� *MusicPerformanceID_PERF+$ PropertyChanged=�6�     5      $ Competitor Firstname��6)   $
 auto 6�    6G   �� *75MusicPerformance.Competiting.Competitor.FIRSTNAME_SIN+6�     5      $ Competitor Lastname��6)   $
 auto 6�    6F   �� *64MusicPerformance.Competiting.Competitor.LASTNAME_SIN+$ PropertyChanged=�6�     5      $ 	Song name��6)   $ * 6k    6<   �� *MusicPerformance.SONG_NAME+6k     5      $ 
Genre name��6)   $ * 6s    6=   �� *#!MusicPerformance.Genre.GENRE_NAME+6s     5      $ Jury Member JMBG��6)   $
 auto 6s    6C   �� *IsExpert.JuryMemberJMBG_SIN+6s     5      $
 Mark��6)   $ * 6P    67   �� *MARK+6P     5      $ Comment��6)   $ * 6V    6:   �� *	COMMENT+6V   5$      5%      �� 5&   
    �� Orientation$ 
Horizontal=�6   $��2��6.   ���	#993E3EE8.+�5'      �� $��2��6   .+�5(      �� 
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   Music Performance ID:6x   �� 5)      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   Jury Member JMBG:6t   �� 5*      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   Mark:6h   �� 5+      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   
Comment:6k   5,      �� 5-      $��2��6   .+�5.      �� -   5         ��Name$ MusicPerformanceCB��5.   B   ���#87CEFA �� 
IsEditable . $��4q�6^   $	��150��6i   ��6u   �� *MusicPerformanceStrings+ �� Mode$ TwoWay=�$ PropertyChanged=�	 6�   �� *SelectedMusicPerformance+6	  �� 5/      -   5        $ JuryMemberCB��5/   B   ���#87CEFA . $��4q�6X   ��6c   �� *JuryMemberStrings+$ TwoWay=�$ PropertyChanged=�	 6�   �� *SelectedJuryMember+6�   �� 50      -   5        ���#87CEFA$	��100��50   0   $��4q�6d   $	��200��6o   ��6?   �� *MarkTB+$ TwoWay=�6�   �� 51      ���#87CEFA$	��100��6/   $��3q�6f   $	��200��6q   ��6>   �� *	CommentTB+$ TwoWay=�6�   52      54      �� 55   
   -   5         ��Name$ AddBtn��55      $��Add new��6!   $
��Left=�63   $��	372,9,0,0q�6N   $��2��6a   $	��Top=�6n   $	��133��6�   ���	#993E3EE8���	#FFFAFB79��6�   �� *
AddCommand+y�6�   �� *IsJuryMember+    BooleanToVisibilityConverter �� 	Converter# [  �� 56   
   -   5        $ 	ModifyBtn��56      $��Modify selected��6$   $
��Left=�6>   $��
372,44,0,0q�6Y   $��2��6m   $	��Top=�6z   $	��133��6�   ���	#993E3EE8���	#FFFAFB79��6�   �� *ModifyCommand+y�6�   �� *IsJuryMember+# [  �� 57   
   -   5        $ 	DeleteBtn��57      $��Delete selected��6$   $
��Left=�6>   $��
372,79,0,0q�6Y   $��2��6m   $	��Top=�6z   $	��133��6�   ���	#993E3EE8���	#FFFAFB79��6�   �� *DeleteCommand+y�6�   �� *IsJuryMember+# [  58      59      