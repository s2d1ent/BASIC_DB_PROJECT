   M S B A M L   `   `   `  ����D  @DMS MySql, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DMS_MySql	DMS_MySql  5   	       DMS_MySql.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:DMS_MySql     5   	   -   5          � Title$  	DMS MySql��5   	   $	��550��6   $	��450��6(    � 
ResizeMode$ NoResize=�64    � WindowStartupLocation$ CenterScreen=�6J    � ShowInTaskbar$ False��6o    � SizeToContent$ Manual=�6�   
����������	#FFF2F2F2 � WindowStyle$
 None=�6�   .��5	      �  � RowDefinitions 5
   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      $��0* �6   6,   �� 5      $	��11* �6   �� 5      $	��64* �6   �� 5      $	��75* �6   6-   �� 5      $	��75* �6   6-   �� 5      $	��75* �6   6-   �� 5      $	��75* �6   6-   �� 5      $	��75* �6   6-   .+�5   
   o� $��8��6   $
��Left=�6#   $��20��6>   $��0,0,0,0q�6J   $��1��6[   $	��Top=�6l   $	��450��6�   
�������.@�5      m� $
��File��6   .@�5      m� $
��Open��6   6+   m� 5      $
��Save��6   6,   m� 5       -   5         m�Name$ Menu_Item_Settings��5       $��Settings��67   6K   m� 5!      -   5        $ Menu_Item_Exit��5!      $
��Exit��63   6D   5"      m� 5#      $
��Edit��6   6(   m� 5$      $��Connect��6   .@�5%      m� -   5        $ Menu_Item_Save��5%      $��Save config��63   6J   m� 5&      -   5        $ Menu_Item_Load��5&      $��Load config��63   6J   m� 5'      -   5        $ History_connection��5'      $��History��67   5)      5*      m� 5+      $��	Reference��6   .@�5,      m� $��About��6   6,   5-      5.      �� 5/   
   $��1��6   $��2��6"   $��7��62   $��Center=�6F   $	��24 (�6c   $"��Select the connection option��6r   
���   �6�   �� 50   
   -   5        	 ��Name$	 MainWindow_local_btn��50      $��Open SQL��6/   $��3��6B   $��3��6O   $��4��6_   $	��200��6s   $��75��6   $��26(�6�   $��Medium"�6�   ���#F2F2F2$��0q�6�   6�   �� 51   
   -   5        $	 MainWindow_network_btn��51      $��MySql��61   $��2��6A   $��3��6N   $��4��6^   $	��200��6r   $��75��6~   $��26(�6�   $��Medium"�6�   ���#F2F2F2$��0q�6�   6�   �� 52   
   $��5��6   $��2��6!   $��7��61   $��Center=�6E   $��Bottom=�6b   $��10(�6}   $=��7All rights and a little left are reserved by LazyCote©��6�   
���   �6�   53      54      