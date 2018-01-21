using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections.ObjectModel;

namespace BW_PrototypWPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ObservableCollection<Ingredient> IngList = new ObservableCollection<Ingredient>();
        //List<Ingredient> IngList2 = new List<Ingredient>();
        //Dictionary<string, Ingredient> IngDict = new Dictionary<string, Ingredient>();
        //List<Dish> DishList = new List<Dish>();
        ////ObservableCollection<Dish> DishList = new ObservableCollection<Dish>();
        //List<string> _items = new List<string>();
        //private DishViewModel _dishViewModel;
        //public DishHandler dishes;//= new DishHandler();
        //public IngredientHandler ingredients = new IngredientHandler();

        public MainWindow()
        {
            InitializeComponent();
            //dishes = new DishHandler(ingredients.IngDict);
            //dishes = new DishHandler();
            //IngredientsListView.ItemsSource = ingredients.IngList;
            //DishesListView.ItemsSource = dishes.DishList;
            //RefreshMiddleListView();

        }
        #region openfiles
        //void OpenFiles()
        //{   //otwieranie składników
        //    try
        //    {
        //        using (StreamReader sr = new StreamReader("Ingredients.txt"))
        //        {

        //            while (!sr.EndOfStream)
        //            {
        //                var line = sr.ReadLine().Split(',');
        //                if (IngList.Count == 0)
        //                {
        //                    IngList.Add(new Ingredient(line[0], line[1], line[2]));
        //                    IngDict.Add(line[0], IngList[IngList.Count - 1]);
        //                }
        //                else
        //                {
        //                    IngList.Add(new Ingredient(IngList, line[0], line[1], line[2]));
        //                    IngDict.Add(line[0], IngList[IngList.Count - 1]);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e) // jeśli nie znajdzie pliku, wyswietli komunikat
        //    {
        //        var msg = "Nie udało się odczytać pliku Ingredients.txt";
        //        MessageBox.Show(msg, e.Message, MessageBoxButton.OK);
        //    }
        //    //otwieranie dań
        //    try
        //    {
        //        using (StreamReader sr = new StreamReader("Dish.txt"))
        //        {

        //            while (!sr.EndOfStream)
        //            {
        //                var line = sr.ReadLine().Split(',');
        //                var name = new Ingredient();
        //                string measure;
        //                int quant;
        //                var SubDict = new Dictionary<Ingredient, Amount>();
        //                if (DishList.Count == 0)
        //                {
        //                    var am = line[2].Split(' ');
        //                    foreach (var item in am)
        //                    {
        //                        var ToAmount = item.Split('|');
        //                        name = IngDict[ToAmount[0]];
        //                        quant = Convert.ToInt32(ToAmount[1]);
        //                        measure = ToAmount[2];
        //                        var amount = new Amount(quant, measure);
        //                        SubDict.Add(name, amount);
        //                    }
        //                    var time = line[1].Split('\'');
        //                    var dish = new Dish(line[0], SubDict, time[0] + " " + time[1]);
        //                    DishList.Add(dish);
        //                }
        //                else
        //                {
        //                    var am = line[2].Split(' ');
        //                    foreach (var item in am)
        //                    {
        //                        var ToAmount = item.Split('|');
        //                        name = IngDict[ToAmount[0]];
        //                        quant = Convert.ToInt32(ToAmount[1]);
        //                        measure = ToAmount[2];
        //                        var amount = new Amount(quant, measure);
        //                        SubDict.Add(name, amount);
        //                    }
        //                    var time = line[1].Split('\'');
        //                    //var dish = new Dish(line[0], SubDict, time[0] + " " + time[1]);
        //                    var dish = new Dish(DishList, line[0], SubDict, time[0] + " " + time[1]);
        //                    DishList.Add(dish);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e) // jeśli nie znajdzie pliku, wyswietli komunikat
        //    {
        //        var msg = "Nie udało się odczytać pliku Dish.txt";
        //        MessageBox.Show(msg + Environment.NewLine + e.Message , " ", MessageBoxButton.OK);
        //    }
        //}
        #endregion
        //private void DishesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var Hh = DishesListView.SelectedIndex;
        //    Wybrane.ItemsSource = dishes.GetSelectedDishAmountAsList(Hh);
        //}

        //private void IngredientsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    RefreshMiddleListView();
        //}

        //private void RefreshMiddleListView()
        //{
        //    var Hh = IngredientsListView.SelectedIndex;
        //    List<Ingredient> Hh2 = new List<Ingredient>();
        //    foreach (var item in IngredientsListView.SelectedItems)
        //    {
        //        Hh2.Add((Ingredient)item);
        //    }
        //    Wybrane.ItemsSource = dishes.GetDishesWithAllSelectedIngredients(Hh2);
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var Hh = DishesListView.SelectedIndex;
        //    dishes.AddEntryDish("Dish.txt", dishes.DishList[Hh]);
        //}

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    var Hh = IngredientsListView.SelectedIndex;
        //    ingredients.AddEntryIngredient("Ingredients.txt", ingredients.IngList[Hh]);
        //}

        private void TEMP_CLICK(object sender, RoutedEventArgs e)
        {
            IngredientsToDishesWindow temp = new IngredientsToDishesWindow(this,true);
            temp.Show();
            this.Hide();
        }

        private void menu_AddIng(object sender, RoutedEventArgs e)
        {
            AddProduct AP = new AddProduct();
            AP.Show();
        }

        private void menu_AddCat(object sender, RoutedEventArgs e)
        {
            new AddCategory(new CategoryHandler()).Show();
        }

        private void menu_AddDish(object sender, RoutedEventArgs e)
        {
            new AddDish().Show();
        }

        private void btn_ViewDishesWithAllIngs_Click(object sender, RoutedEventArgs e)
        {
            IngredientsToDishesWindow func1 = new IngredientsToDishesWindow(this,true);
            func1.Show();
            this.Hide();
        }

        private void btn_ViewAllDishesWithSelectedIngs_Click(object sender, RoutedEventArgs e)
        {
            IngredientsToDishesWindow func2 = new IngredientsToDishesWindow(this, false);
            func2.Show();
            this.Hide();
        }

        private void btn_FindDishes_Click(object sender, RoutedEventArgs e)
        {
            FindDish func3 = new FindDish(this,0);
            func3.Show();
            Hide();
        }

        private void menu_UpdateDish(object sender, RoutedEventArgs e)
        {
            FindDish func4 = new FindDish(this, 1);
            func4.ShowDialog();
        }

        private void menu_DeleteDish(object sender, RoutedEventArgs e)
        {
            FindDish func5 = new FindDish(this, 2);
            func5.ShowDialog();
        }

        private void menu_UpdateIng(object sender, RoutedEventArgs e)
        {
            FindDish func6 = new FindDish(this, 3);
            func6.ShowDialog();
        }

        private void menu_DeleteIng(object sender, RoutedEventArgs e)
        {
            FindDish func7 = new FindDish(this, 4);
            func7.ShowDialog();
        }

        private void menu_UpdateCat(object sender, RoutedEventArgs e)
        {
            FindDish func8 = new FindDish(this, 5);
            func8.ShowDialog();
        }
    }
}
