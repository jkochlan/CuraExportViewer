   M S B A M L   `   `   `  ����   CuraExportViewer!    CuraExportViewer.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  
CSV Viewer��.���  � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��.+�c� .@���  c� OverflowMode$ AsNeeded=�.���� $ ��/Resources/compareIcon.png���� -    ��Name$ 
csvListBox��$��10q� �� SelectionMode$ Extended=�&   System.Windows.Controls.DataGrid  -     Name$ csvDataGrid��$��1��   AutoGenerateColumns$ False��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Extruder��1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *
Extruder+  $	 Key��	 �� *Key+  $
 Type��	 �� *Type+  $ Value��	 �� *Value+
 �� 
GroupStyle
 (  "System.Windows.Controls.GroupStyle     ContainerStyle ��  �� 
TargetType" ��.���  �� Property � �� �� Value ��  �� 
TargetType" ��.R�9�  9� 
IsExpanded$
 True����d�  d� 	IsChecked ��  �� Path$ 
IsSelected� �� Mode$ TwoWay=� �� RelativeSource �� *TemplatedParent+.���� $
��Bold"����� *Name+����   -   $ csvDataGrid2��$��2��$ False��$y�	Collapsed=�   $ Extruder��	 �� *
Extruder+  $	 Key��	 �� *Key+  $
 Type��	 �� *Type+  $ Value��	 �� *Value+
    �� " ��.���  � �� �� " ��.R�9� $
 True����d�  �� $ 
IsSelected�$ TwoWay=� �� *TemplatedParent+.���� $
��Bold"����� *Name+���� 