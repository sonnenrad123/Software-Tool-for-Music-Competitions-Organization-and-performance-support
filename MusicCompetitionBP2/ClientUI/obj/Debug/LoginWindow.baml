   M S B A M L   `   `   `  ����C  ?ClientUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ClientUIClientUI  5   	       ClientUI.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:ClientUI     5   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          �Name$  LoginMainWindow��5   	    � Title$ LoginWindow��5	   	   $	��700��6   $	��550��6*    � 
ResizeMode$ NoResize=�66    � WindowStartupLocation$ CenterScreen=�6L   $��Segoe UI Emoji)�6r    � DataContext 5
      #   ClientUI.LoginWindowViewModel  5   
   .��5      � .+�5   
   �� $	��480��6   $	��Top=�6!   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ 	#FF2281D1��6#   � 5      $ 	#FF34268A��6#    � Offset$ 1Q�65   � 5      $ 	#FFE2792C��6#   $	 0.5Q�65   5      5      �� 5   
   $��Resources/Images/mic2.png��6   $	��200��63   $	��200��6?   $	��Top=�6L   $��10q�6d   �� 5   
   $	��380��6   $	��340��6    
�������$��Bottom=�6:   $��0,100q�6U   	 J� Effect	 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      
   Color$
 	#FFBBBBBB��6#      	Direction$ 0Q�65      
BlurRadius$ 15Q�6C      RenderingBias$ Quality=�6S      ShadowDepth$ 1Q�6k   5      � 5   
   $	��380��6   $	��340��6   $��Bottom=�6(   $��0,100q�6C   .+�5      �� $��LOGIN��6   $��Center=�6%   $	��Top=�6B   $��5q�6Z   
�������$��18(�6w   �� 5      $��Center=�6   $��10q�64   .+�5       �� $
��0,10q�6   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ EMAIL��6(    %  !MaterialDesignFloatingHintTextBox#��[  6O   $��Champagne & Limousines)�6�   $��18(�6�   ��6�   �� *	EmailTB+ �� Mode$ TwoWay=�G� 5!      -   5         G�Name$ PasswordBox��5!      $
��0,10q�63   $ PASSWORD��6A    ) %MaterialDesignFloatingHintPasswordBox#��[ 6k   $��Champagne & Limousines)�6�   $��18(�6�   G� 5"      -   5        $ PasswordBoxReenter��5"      $
��0,10q�6:   $ RE ENTER THE PASSWORD��6H   #��[ 6   $��Champagne & Limousines)�6�   $��18(�6�   y�6�   �� *ReenterPasswordVisible+   BooleanToVisibilityConverter �� 	Converter# [ 5#      5$      �� 5&   
   $��Bottom=�6   $
��0,85q�6,   $	��200��6:   $��LOGIN��6F   ��6V   �� *LoginCommand+��6w   ��  �� ElementName$ LoginMainWindow��5*      5+      