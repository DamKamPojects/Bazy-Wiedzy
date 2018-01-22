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
    /// Logika interakcji dla klasy IngredientsToDishesWindow.xaml
    /// </summary>
    public partial class IngredientsToDishesWindow : Window
    {
        private MainWindow MainReference;
        DishHandler dishes;
        IngredientHandler ingredients;// = new IngredientHandler();
        bool All;
        ObservableCollection<Dish> DObs = new ObservableCollection<Dish>();
        ObservableCollection<Ingredient> IObs = new ObservableCollection<Ingredient>();

        public IngredientsToDishesWindow()
        {
            InitializeComponent();
        }
        public IngredientsToDishesWindow(MainWindow main, bool all)
        {
            
            MainReference = main;
            InitializeComponent();
            if (all) Title = "Dania zawierające wszystkie składniki";
            else Title = "Wszystkie dania zawierające składniki";
            //ingredients = main.ingredients;
            //dishes = main.dishes;
            ingredients = new IngredientHandler();
            dishes = new DishHandler(ingredients.IngDict);
            //DObs = dishes.GetObservableList(dishes.DishList);
            IObs = ingredients.GetObservableList(ingredients.IngList);
            All = all;
            IngredientsListView.ItemsSource = IObs;
            RefreshListView();
        }

        

        private void RefreshListView()
        {
            //var Hh = IngredientsListView.SelectedIndex;
            ObservableCollection<Ingredient> Hh2 = new ObservableCollection<Ingredient>();
            foreach (var item in IngredientsListView.SelectedItems)
            {
                Hh2.Add((Ingredient)item);
            }
            UpdateDishes(Hh2,All);
        }
        


        private void IngredientsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshListView();
        }

        private void Wybrane_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Hh = Wybrane.SelectedIndex;
            var Dd = new DishDetails(DObs[Hh], dishes);
            Dd.ShowDialog();
        }

        #region Closing
        private void Btn_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainReference.Show();
        }
        #endregion

        private void tb_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Search.Text != null)
            {
                //if (cbox_Vegan.IsChecked==false) 
                IngredientsListView.ItemsSource = ingredients.GetListOfIngredientsWithSetName(tb_Search.Text);
              // else IngredientsListView.ItemsSource=ingredients.
            }
            else IngredientsListView.ItemsSource = ingredients.IngList;

        }

        private void cbox_Vegan_Click(object sender, RoutedEventArgs e)
        {
            RefreshListView();
        }

        private void UpdateDishes(ObservableCollection<Ingredient> input, bool all)
        {
            if (all == true)
            {
                if (cbox_Vegan.IsChecked == false)
                {
                    DObs = dishes.GetObservableList(dishes.GetDishesWithAllSelectedIngredients(input.ToList()));
                    Wybrane.ItemsSource = DObs;
                }
                else
                {
                    DObs = dishes.GetObservableList(dishes.GetVeganDishesWithAllSelectedIngredients(input.ToList()));
                    Wybrane.ItemsSource = DObs;
                }
            }
            else
            {
                if (cbox_Vegan.IsChecked == false)
                {
                    DObs = dishes.GetObservableList(dishes.GetDishesWithSelectedIngredients(input.ToList()));
                    Wybrane.ItemsSource = DObs;
                }
                else
                {
                    DObs = dishes.GetObservableList(dishes.GetVeganDishesWithSelectedIngredients(input.ToList()));
                    Wybrane.ItemsSource = DObs;
                }
            }
        }
    }
}
