   M S B A M L   `   `   `  ����C  ?ClientUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ClientUI.ViewsClientUI.Views  5      %    ClientUI.Views.ControlPanelView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:ClientUI.Views     5      _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      $��Segoe UI Emoji)�5	   7   .��5
      � 
������   � RowDefinitions  5   
   �� 5      $��50 �6   �� 5      $��* �6   �� 5      $��80 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   � 
���   � J� Opacity$	 0.8Q�6"   $��0��60   $��2��6=   .+�5      �� $��View and edit profile��6   $	��Top=�65   $��Center=�6M   
�������$��Italic$�6~   $��Aharoni)�6�   $��22(�6�   5      �� 5   
   $��1��6   ���	#7F000000.+�5      �� $��JMBG:��6   
�������$��22(�68   6H   �� 5      $��First name:��6   
�������$��22(�6=   6M   �� 5      $��
Last name:��6   
�������$��22(�6<   6L   �� 5      $��Email:��6   
�������$��22(�68   6H   �� 5      $��
Phone No.:��6   
�������$��22(�6<   6L   �� 5      $��Date of birth:��6   
�������$��22(�6@   6P   �� 5      $��Address��6   $��Center=�6&   
�������$��Italic$�6V   $��22(�6i   6y   �� 5      $��City:��6   
�������$��22(�67   6G   �� 5       $��Street:��6   
�������$��22(�69   6I   �� 5!      $��Street number:��6   
�������$��22(�6@   6P   �� 5"      $��Password��6   $��Center=�6'   
�������$��Italic$�6W   $��22(�6j   6z   �� 5#      $��	Password:��6   
�������$��22(�6;   6K   �� 5$      $��My publishing house:��6   
�������$��22(�6F   y�6T   �� *IsEventOrganizer+    BooleanToVisibilityConverter �� 	Converter# [  6�   5%      �� 5&   
   $��1��6   $��1��6"   ���	#7F000000.+�5'      �� ���#87CEFA$	��200��6,   $��7q�6a   $	��400��6l    �� 
IsReadOnly . ��6;   �� *JmbgTB+ �� Mode$ TwoWay=�6�   �� 5(      ���#87CEFA$	��200��6,   $��7q�6f   $	��400��6q   ��6;   �� *FirstNameTB+$ TwoWay=�6�   �� 5)      ���#87CEFA$	��200��6,   $��7q�6e   $	��400��6p   ��6;   �� *
LastNameTB+$ TwoWay=�6�   �� 5*      ���#87CEFA$	��200��6,   $��7q�6b   $	��400��6m   ��6;   �� *	EmailTB+$ TwoWay=�6~   �� 5+      ���#87CEFA$	��200��6,   $��7q�6d   $	��400��6o   ��6;   �� *	PhoneNoTB+$ TwoWay=�6�   (  "System.Windows.Controls.DatePicker  5,      $��4q�6      SelectedDate 6&   �� *	BirthDP+$ TwoWay=� � 	Resources 5-      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType	 �� Property
 �� Value��%[   '        �� 5.      "  6   .�5/      �� 	 �@��
IsReadOnly
 . 67   50      51      52      �� 54      -   5         ��Name$ CityCB��54   @   ���#87CEFA �� 
IsEditable . $��7,40,7,7q�6P   $	��380��6b   ��6n   �� *CityStrings+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedItem 6�   �� *SelectedCity+6�   �� 55      ���#87CEFA$	��200��6,   $��7q�6c   $	��400��6n   ��6;   �� *
StreetTB+$ TwoWay=�6   �� 56      ���#87CEFA$	��200��6,   $��7q�6c   $	��400��6n   ��6;   �� *
NumberTB+$ TwoWay=�6   G� 57      -   5         G�Name$ 
passwordPB��57       G� Password$ NoChange��6.   ���#87CEFA$	��200��6X   $��7,40,7,7q�6g   $	��400��6y   6�   �� 58       . ���#87CEFA$	��200��6=   $��7q�6r   $	��400��6}   ��6L   �� *MyPHTB+$ TwoWay=�y�6�   �� *IsEventOrganizer+# [  6�   59      �� 5:   
   $��2��6   $��2��6"   .+�5;      �� $��Edit Profile��6   ��6,   �� *ModifyCommand+��6N   ��  �� ElementName$ 
passwordPB��6�   5<      5>      5?      