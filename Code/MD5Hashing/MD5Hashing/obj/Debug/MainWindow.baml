   M S B A M L   `   `   `  ����E  AMD5Hashing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MD5Hashing
MD5Hashing  5   	       MD5Hashing.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:MD5Hashing     5   	     � Title$  
MD5 Hasher��5   	   $��255.848��6   $	��525��6-   .��5	      � .+�5
   
   �� -   5         ��Name$ inputGroupBox��5
      $��Input��6*   $
��Left=�69   $��	10,10,0,0q�6T   $	��Top=�6g   $	��126��6   $	��497��6�   .��5      � $
��Left=�6   $	��108��6.   $	��Top=�6;   $	��487��6S   $��0,0,-2,0q�6_   .+�5      �� -   5         ��Name$ 	textLabel��5      $��Text:��6+   $
��Left=�6;   $��	10,10,0,0q�6V   $	��Top=�6i   $��70��6�   �� 5      -   5         ��Name$ inputTextBox��5      $
��Left=�60   $��26��6K   $��	85,10,0,0q�6W    �� TextWrapping$
 Wrap=�6j   $	��Top=�6~   $	��312��6�   �� 5      -   5        $ 	saltLabel��5      $��Salt:��6+   $
��Left=�6;   $��	10,41,0,0q�6V   $	��Top=�6i   $��70��6�   �� 5      -   5        $ saltTextBox��5      $
��Left=�6/   $��26��6J   $��	85,41,0,0q�6V   $
 Wrap=�6i   $	��Top=�6}   $	��312��6�   �� 5      -   5         ��Name$ generateSaltButton��5      $��Generate��65   $
��Left=�6H   $��
402,41,0,0q�6c   $	��Top=�6w   $��75��6�   $��26��6�   6�   �� 5      -   5        $ 
hashButton��5      $
��Hash��6-   $
��Left=�6<   $��
402,72,0,0q�6W   $	��Top=�6k   $��75��6�   $��26��6�   6�   �� 5      -   5         ��Name$ disableSaltCheckBox��5      $��Disable Salting��68   $
��Left=�6R   $��	85,72,0,0q�6m   $	��Top=�6�   6�   5      5      �� 5   
   -	   5        $ outputGroupBox��5      $��Output��6+   $
��Left=�6;   $��
10,136,0,0q�6V   $	��Top=�6j   $��76��6�   $	��497��6�   .��5      � $
��Left=�6   $��58��6.   $	��Top=�6:   $	��487��6R   $��0,0,-2,0q�6^   .+�5      �� -
   5        $ 	hashLabel��5      $��Hash:��6+   $
��Left=�6;   $��	10,10,0,0q�6V   $	��Top=�6i   $��70��6�   �� 5      -   5        $ hashTextBox��5      $
��Left=�6/   $��26��6J   $��	85,10,0,0q�6V   $
 Wrap=�6i   $	��Top=�6}   $	��312��6�   5      5      5      5      