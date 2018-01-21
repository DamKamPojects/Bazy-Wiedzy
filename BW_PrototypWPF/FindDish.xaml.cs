using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BW_PrototypWPF
{
    /// <summary>
    /// Logika interakcji dla klasy FindDish.xaml
    /// </summary>
    public partial class FindDish : Window
    {
        MainWindow MainReference;
        DishHandler DH;
        IngredientHandler IH;
        CategoryHandler CH;
        ObservableCollection<Dish> DList = new ObservableCollection<Dish>();
        ObservableCollection<Ingredient> DIng = new ObservableCollection<Ingredient>();
        ObservableCollection<Categories> DCat = new ObservableCollection<Categories>();
        int State;
       
        public FindDish(MainWindow main, int state)
        {
            MainReference = main;
            InitializeComponent();
            IH = new IngredientHandler();
            //DH = new DishHandler(IH.IngDict);
            DH = new DishHandler();
            CH = new CategoryHandler();
            DList = DH.GetObservableList(DH.DishList);
            lv_Dishes.ItemsSource = DList;
            State = state;
            if (State == 3 || State == 4)
            {
                DIng = IH.GetObservableList(IH.IngList);
                lv_Dishes.ItemsSource = DIng;
                l_wyszukaj.Content = "Wyszukaj składnik po nazwie";
                l_lista.Content = "Lista składników";
                //l_danie.Content = "Składnik wegetariański?";
                l_danie.Visibility = Visibility.Hidden;
                cbox_Meat.Visibility = Visibility.Hidden;
            }
            if (State == 5)
            {
                DCat = CH.GetObservableList(CH.CatList);
                lv_Dishes.ItemsSource = DCat;
                l_wyszukaj.Content = "Wyszukaj kategorię po nazwie";
                l_lista.Content = "Lista kategorii";
                //l_danie.Content = "Składnik wegetariański?";
                l_danie.Visibility = Visibility.Hidden;
                cbox_Meat.Visibility = Visibility.Hidden;
            }
        }
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainReference.Show();
        }

        private void tb_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (tb_Name.Text != null) lv_Dishes.ItemsSource = DH.GetListOfDishesWithSetNames(tb_Name.Text);
            //else lv_Dishes.ItemsSource = DH.DishList;
            Refresh();
        }

        private void lv_Dishes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Hh = lv_Dishes.SelectedIndex;
            switch (State)
            {
                case 0:
                    var Dd = new DishDetails(DList[Hh].ID, DH);
                    Dd.ShowDialog();
                    break;
                case 1:
                    var UD = new AddDish(DList[Hh]);
                    UD.ShowDialog();
                    break;
                case 2:
                    var DDel = MessageBox.Show("Czy jesteś pewien, że chcesz usunąć danie " + DList[Hh].Name + "?", " ", MessageBoxButton.YesNo);
                    if (DDel == MessageBoxResult.Yes)
                    {
                        DH.DeleteFromFile("Dish.txt", DList[Hh].ID);
                        if (DH.Close == true) Close();
                    }
                    break;
                case 3:
                    var UIng = new AddProduct(DIng[Hh]);
                    UIng.ShowDialog();
                    break;
                case 4:
                    var IDel = MessageBox.Show("Czy jesteś pewien, że chcesz usunąć składnik " + DIng[Hh].Name + "?", " ", MessageBoxButton.YesNo);
                    if (IDel == MessageBoxResult.Yes)
                    {
                        IH.DeleteFromFile("Ingredients.txt", DIng[Hh].ID);
                        if (IH.Close == true) Close();
                    }
                    break;
                case 5:
                    var UCat = new AddCategory(DCat[Hh]);
                    UCat.ShowDialog();
                    if (UCat.Close) Close();
                    break;
                default:
                    break;
            }
        }

        private void Refresh()
        {
            if (State != 3)
            {
                if (tb_Name.Text != null)
                {
                    if (cbox_Meat.IsChecked == false)
                    {
                        DList = DH.GetObservableList(DH.GetListOfDishesWithSetNames(tb_Name.Text));
                        lv_Dishes.ItemsSource = DList;
                    }
                    else
                    {
                        DList = DH.GetObservableList(DH.GetVeganDishesWithSetNames(tb_Name.Text));
                        lv_Dishes.ItemsSource = DList;
                    }
                }
                else
                {
                    if (cbox_Meat.IsChecked == false)
                    {
                        DList = DH.GetObservableList(DH.DishList);
                        lv_Dishes.ItemsSource = DList;
                    }
                    else
                    {
                        DList = DH.GetObservableList(DH.GetVeganDishes());
                        lv_Dishes.ItemsSource = DList;
                    }
                }
            }
            if (State==3 || State==4)
            {
                DIng = IH.GetObservableList(IH.GetListOfIngredientsWithSetName(tb_Name.Text));
                lv_Dishes.ItemsSource = DIng;
            }
            if (State==5)
            {
                DCat = CH.GetObservableList(CH.GetCategoriesWithSetNames(tb_Name.Text));

                lv_Dishes.ItemsSource = DCat;
            }
        }

        private void cbox_Meat_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
