   M S B A M L   `   `   `  ���� C  ?HAPPYWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:HAPPYWPF.ViewHAPPYWPF.View  -clr-namespace:HAPPYWPF.ViewHAPPYWPF.View      HAPPYWPF.View.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ,viewclr-namespace:HAPPYWPF.View     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:HAPPYWPF.View     $	��800��$	��600��  � 
ResizeMode$  NoResize=�$	��800�� � WindowStartupLocation$ CenterScreen=����#181818 � Title$ PENTAGO��.��� ���#21201f � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto � � ColumnDefinitions �� $	��240 ��� $��* �.+�� -    �Name$ GridTopoNavegacao��$��0��$��0��$��2�����#C7C7C7 �� $
��Auto � �� $	��240 ��� $��* �.+��� $��/Imagens/logo.png��$��10q�$��0��$��0���� $��0 20 0 0q�$��1��.+��� 
���   �$��24(�$��Normal&�$��Verdana)�$��
Frases Top��$
��Left=�$��20 0 0 0q��� 
������$��14(�$��Verdana)�$	��App��$
��Left=�$��20 0 0 0q�� $��1��$��0�� �� $
��Auto � �� $��* ��� $��* �.+��� $��0��$��0��$��	CONFIRMAR�� � Cursor$
 Hand��   AddButtonStyle#��[  ���� *AddFuncionarioCommand+�� $��Center=�$��0��$��1��$��REMOVER��$
 Hand��  RemoveButtonStyleMenu#��[ ���� *RemoverFuncionarioCommand+2   ,HAPPYWPF.View.NavigationMenuFuncionariosView  $��1��$��1�� � DataContext �� *LoadMenuFuncionariosViewModel+   HAPPYWPF.View.Mensagem  $��2��$��2��$��0��