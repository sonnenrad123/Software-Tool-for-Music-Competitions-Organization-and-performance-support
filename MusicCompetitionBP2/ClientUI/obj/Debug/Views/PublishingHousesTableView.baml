   M S B A M L   `   `   `  ����C  ?ClientUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ClientUI.ViewsClientUI.Views  5      .    (ClientUI.Views.PublishingHousesTableView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:ClientUI.Views     5      $��Segoe UI Emoji)�5   7   .��5	      � 
������   � RowDefinitions  5
   
   �� 5      $
��100* �6   6,   �� 5      $
��650* �6   6,   �� 5      $
��350* �6   6,   .+�5   
   � 
���   � J� Opacity$	 0.8Q�6"   $��0��60   .+�5      �� $��Publishing houses��6   $	��Top=�61   $��Center=�6I   
�������$��Italic$�6z   $��Aharoni)�6�   $��22(�6�   5      � 5   
   $��1��6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ tabela��5         AutoGenerateColumns .     
IsReadOnly .    RowBackground
 �����   CanUserAddRows .  $	��500��6  ���#87CEFA$	 0.8Q�62  ��6p   ��  �� Path$ PublishingHouses� �� UpdateSourceTrigger$ PropertyChanged=�	 �� SelectedItem	 6�   �� *SelectedPublishingHouse+
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6O   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *ID_PH+6O     5      $
 Name��6)   $ * 6S    67   �� *	NAME_PH+6S     5      $ Address��6)   $ * 6U    6:   �� *ADR_PH+6U   5      5       �� 5!   
    �� Orientation$ 
Horizontal=�6   $��2��6.   ���	#993E3EE8.+�5"      �� $��2��6   .+�5#      �� 
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   Name:6h   �� 5%      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   City:6h   �� 5&      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   Street name:6o   �� 5'      
�������$��Italic$�6+   $��Aharoni)�6>   $��18(�6S   .��6a   	Number:6j   5(      �� 5)      $��2��6   .+�5*      �� ���#87CEFA$	��100��60   $��3q�6d   $	��200��6o   ��6?   �� *NameTB+ �� Mode$ TwoWay=�6�   �� 5+      -   5         ��Name$ CityCB��5+   C   ���#87CEFA �� 
IsEditable . $��4q�6S   $	��150��6^   ��6j   �� *CityStrings+$ TwoWay=�$ PropertyChanged=�	 6�   �� *SelectedCity+6�   �� 5,      ���#87CEFA$	��100��60   $��3q�6e   $	��200��6p   ��6?   �� *
StreetTB+$ TwoWay=�6�   �� 5-      -   5        ���#87CEFA$	��100��5-   0   $��3q�6e   $	��200��6p   ��6?   �� *
NumberTB+$ TwoWay=�6�   50      52      �� 53   
   -   5         ��Name$ AddBtn��53      $��Add new��6!   $
��Left=�63   $��	372,9,0,0q�6N   $��2��6a   $	��Top=�6n   $	��133��6�   ���	#993E3EE8���	#FFFAFB79��6�   �� *
AddCommand+�� 54   
   -   5        $ 	ModifyBtn��54      $��Modify selected��6$   $
��Left=�6>   $��
372,44,0,0q�6Y   $��2��6m   $	��Top=�6z   $	��133��6�   ���	#993E3EE8���	#FFFAFB79��6�   �� *ModifyCommand+�� 55   
   -   5        $ 	DeleteBtn��55      $��Delete selected��6$   $
��Left=�6>   $��
372,79,0,0q�6Y   $��2��6m   $	��Top=�6z   $	��133��6�   ���	#993E3EE8���	#FFFAFB79��6�   �� *DeleteCommand+56      57      