   M S B A M L   `   `   `  ����C  ?ClientUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ClientUI.ViewsClientUI.Views  5      .    (ClientUI.Views.PerformanceHallsTableView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:ClientUI.Views     5      $��Segoe UI Emoji)�5   7   .��5	      � 
������   � RowDefinitions  5
   
   �� 5      $
��100* �6   6,   �� 5      $
��650* �6   6,   �� 5      $
��350* �6   6,   .+�5   
   � 
���   � J� Opacity$	 0.8Q�6"   $��0��60   .+�5      �� $��Performance Halls��6   $	��Top=�61   $��Center=�6I   
�������$��Italic$�6z   $��Aharoni)�6�   $��22(�6�   5      � 5   
   $��1��6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ tabela��5         AutoGenerateColumns .     
IsReadOnly .    RowBackground
 �����   CanUserAddRows .  ���#87CEFA$	 0.8Q�6"  $	��500��60  ��6p   ��  �� Path$ PerformanceHalls� �� UpdateSourceTrigger$ PropertyChanged=�	 �� SelectedItem	 6�   �� *SelectedPerformanceHall+
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6Q   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *	ID_HALL+6Q     5      $
 Name��6)   $ * 6U    67   �� *	NAME_HALL+6U     5      $ Capacity��6)   $ * 6X    6;   �� *
CAPACITY+6X   5      5       �� 5!   
    �� Orientation$ 
Horizontal=�6   $��2��6.   ���	#993E3EE8.+�5"      �� $��2��6   .+�5#      �� 
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   
Hall name:6m   �� 5$      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   Hall capacity:6q   5&      �� 5'      $��2��6   .+�5(      �� ���#87CEFA$	��100��6/   $��7q�6c   $	��200��6n   ��6>   �� *NameTB+ �� Mode$ TwoWay=�6   �� 5)      -   5        ���#87CEFA$	��100��5)   /   $��7q�6g   $	��200��6r   ��6>   �� *
CapacityTB+$ TwoWay=�6�   5,      5.      �� 5/   
   -   5         ��Name$ AddBtn��5/      $��Add new��6!   $
��Left=�63   $��	372,9,0,0q�6N   $��2��6a   $	��Top=�6n   $	��133��6�   ���	#993E3EE8���	#FFFAFB79��6�   �� *
AddCommand+�� 50   
   -   5        $ 	ModifyBtn��50      $��Modify selected��6$   $
��Left=�6>   $��
372,44,0,0q�6Y   $��2��6m   $	��Top=�6z   $	��133��6�   ���	#993E3EE8���	#FFFAFB79��6�   �� *ModifyCommand+�� 51   
   -   5        $ 	DeleteBtn��51      $��Delete selected��6$   $
��Left=�6>   $��
372,79,0,0q�6Y   $��2��6m   $	��Top=�6z   $	��133��6�   ���	#993E3EE8���	#FFFAFB79��6�   �� *DeleteCommand+52      53      