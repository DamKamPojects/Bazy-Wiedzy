   M S B A M L   `   `   `  ����I  EBW_PrototypWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:BW_PrototypWPFBW_PrototypWPF  5   	   .    (BW_PrototypWPF.IngredientsToDishesWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:BW_PrototypWPF     5   	   -   5          � Title$  IngredientsToDishesWindow��5   	   $	��500��6+   $	��500��68    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      $��* �6   �� 5      $	��200 �6    � RowDefinitions 5   	   �� 5      $��25 �6   �� 5      $��25 �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��35 �6   .+�5   
   �� -   5        $��Zamknij��5      $��5,0,5,10q�5      $��1��5      $��4��5      �� 5   
   -   5         ��Name$ 	tb_Search��5      $��1��5      $��0��5      $��20,0,0,0q�5!      �� 5$   
   $��Szukaj po nazwie��6   $��0��5%      $��0��5&      $��20,0,0,0q�5'      �� 5)   
   $"��Szukaj dań wegetariańskich��6   $��2��65   �� 5*   
   -   5         ��Name$ 
cbox_Vegan��5*      $��1��6'   $��2��64   $��20,0,0,0q�6D   6V   �� 5,   
   -   5         ��Name$ lbl_Ingredients��5,      $��
Składniki��5-      $��2��5.      $��0��5/      $��20,0,0,0q�50      �� 51   
   -   5         ��Name$ IngredientsListView��51      $��2��52      $��0��53      $��2��54      $��1��55       �� SelectionMode$ Multiple=�56      $��
20,35,0,20q�57      	 �� View	 59      �� 5:      .P�5;      �� $��Nazwa��6%   
 �� Width$

 auto��64    �� DisplayMemberBinding 5<   %   �� *Name+5=      5?      �� 5A   
   -   5        $, &lbl_DishesWithSelectedIngredientsList1��5A      $��Lista dań zawierających��5B      $��2��5C      $��2��5D      �� 5E   
   -   5        $, &lbl_DishesWithSelectedIngredientsList2��5E      $��wybrane składniki��5F      $��2��5G      $��2��5H      $��0,12,0,0q�5I      �� 5J   
   -	   5        $ Wybrane��5J      $��2��5K      $��2��5L      $��2��5M      $��1��5N      $��
0,35,20,20q�5O      	 5Q      �� 5R      .P�5S      �� $��Nazwa składnika��6%   $

 auto��6>    5T   $   �� *Name+�� 5U      $��Ilość��6%   $
 60��64    5V   %   �� *Time+5W      5Y      5Z      5]      