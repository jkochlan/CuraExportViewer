   M S B A M L   `   `   `  ����  CuraExportViewer!    CuraExportViewer.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
CSV Viewer��5   	    � 	Resources 5         UnchangedColor �� Color  ChangedColor  
AddedColor  RemovedColor  UnchangedRowStyle �� 
TargetType �� Property �� Value  ChangedRowStyle  AddedRowStyle  RemovedRowStyle��%�  &	        &	      &	 B     &	 f     &	 �     7   &	 �     7  &	     7  &	 V    7  �� 5   
   $	 Red��61   �� 5   
   $ LightYellow��6/   �� 5	   
   $ 
LightGreen��6-   �� 5
   
   $ 
LightCoral��6/   �� 5   
   " ��6*   .�5      ��  � ��8   6+   5      �� 5   
   " ��6(   .�5      ��  � ��8   6+   5      �� 5   
   " ��6&   .�5      ��  � ��8   6+   5      �� 5   
   " ��6(   .�5      ��  � ��8   6+   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5       $
��Auto �6   �� 5!      $
��Auto �6   �� 5"      $
��Auto �6   .+�5$   
   �� $��0��6   .+�5%      c� -   5         c�Name$ toolbar��5%   -   $y�	Collapsed=�6   .@�5&      ��  c� OverflowMode$ AsNeeded=�6   .��5'      �� $ ��/Resources/compareIcon.png��6   5(      5)      �� 5*      -   5        	 ��Name$	 
csvListBox��5*      $��10q�6*   
 �� SelectionMode$
 Extended=�66   5,      5-      &   System.Windows.Controls.DataGrid  5/   
   -   5          Name$ csvDataGrid��5/      $��1��6(      AutoGenerateColumns$ False��68      Columns 50      0  *System.Windows.Controls.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn   Header$ Extruder��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *
Extruder+  52      $	 Key��6%    62   �� *Key+  53      $
 Type��6%    63   �� *Type+  54      $ Value��6%    64   �� *Value+ �� 
GroupStyle 56      (  "System.Windows.Controls.GroupStyle  57         ContainerStyle 58      �� 59      " ��6    .�5:      ��  � �� 5;   "   �� 5<   &    �� 
TargetType" ��66   .R�5=   *   9�  9� 
IsExpanded$
 True��63   ��5>   .   d� 5?   2    d� 	IsChecked 6?   ��  �� Path$ 
IsSelected� �� Mode$ TwoWay=� �� RelativeSource �� *TemplatedParent+.��5@   6   �� $
��Bold"�6@   ��6R   �� *Name+5A   3   ��5C   .   �� 5D   2   5F   +   5G   '   5I      5J      5L      5N        5P   
   -   5        $ csvDataGrid2��5P      $��2��6)   $ False��69   $y�	Collapsed=�6U    5Q        5R      $ Extruder��6%    67   �� *
Extruder+  5S      $	 Key��6%    62   �� *Key+  5T      $
 Type��6%    63   �� *Type+  5U      $ Value��6%    64   �� *Value+   RowStyle 5W      �� 5X      )  #System.Windows.Controls.DataGridRow"  6    �� Triggers 5Y      �� 5Z       �� Binding 6&   �� *ComparisonStatus+ �� Value 	Unchanged6K   .a�5[      ��  � ��
 �����6;   5\      �� 5]       6&   �� *ComparisonStatus+ Changed6K   .a�5^      ��  � ��
 � ���6;   5_      �� 5`       6&   �� *ComparisonStatus+	 Added6K   .a�5a      ��  � ��
 ����6;   5b      �� 5c       6&   �� *ComparisonStatus+ Removed6K   .a�5d      ��  � ��
 �����6;   5e      5g       5i        5j       5k      �� 5l      " ��6    .�5m      ��  � �� 5n   "   �� 5o   &   " ��66   .R�5p   *   9� $
 True��63   ��5q   .   d� 5r   2    6?   �� $ 
IsSelected�$ TwoWay=� �� *TemplatedParent+.��5s   6   �� $
��Bold"�6@   ��6R   �� *Name+5t   3   ��5v   .   �� 5w   2   5y   +   5z   '   5|      5}      5      5�      5�      5�      