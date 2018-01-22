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
    /// Logika interakcji dla klasy AddDish.xaml
    /// </summary>
    public partial class AddDish : Window
    {
        DishHandler DH;
        IngredientHandler IH;
        CategoryHandler CH = new CategoryHandler();
        List<Amount> SubList = new List<Amount>();
        ObservableCollection<Amount> SubList2 = new ObservableCollection<Amount>();
        ObservableCollection<Ingredient> ObsIng = new ObservableCollection<Ingredient>();
        int State;
        int ID;
        public bool Close = false;

        public AddDish()
        {
            InitializeComponent();
            IH = new IngredientHandler();
            DH = new DishHandler(IH.IngDict);
            State = 1;

            InitializeComponent();
            ObsIng = IH.GetObservableList(IH.IngList);
            lv_Ingredients.ItemsSource = ObsIng;
            lv_ToDish.ItemsSource = SubList2;
            Title = "Dodawanie dania";
        }

        public AddDish(Dish updated)
        {
            InitializeComponent();
            IH = new IngredientHandler();
            DH = new DishHandler(IH.IngDict);
            State = 2;
            ID = updated.ID;

            InitializeComponent();
            Title = "Aktualizacja dania";
            btn_Add.Content = "Zaktualiuj danie";
            lv_Ingredients.ItemsSource = ObsIng;
            foreach (var item in updated.Amount)
            {
                SubList2.Add(new Amount(item.Key, item.Value));
            }
            lv_ToDish.ItemsSource = SubList2;//.OrderBy(x=>x.Ingredient.Name);
            tb_Name.Text = updated.Name;
            tb_Time.Text = updated.Time;
        }

        //public AddDish(DishHandler dh, IngredientHandler ih)
        //{
        //    DH = dh;
        //    IH = ih;
        //    InitializeComponent();
        //    lv_Ingredients.ItemsSource = ih.IngList;
        //    lv_ToDish.ItemsSource = SubList2;
        //}

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            if (State == 1) DH.AddEntryDish("Dish.txt", new Dish(SubList2, tb_Name.Text, tb_Time.Text, DH.GetIDToWrite()));
                //if (DH.Close) Close();
            
            if (State == 2) DH.UpdateFile("Dish.txt", new Dish(SubList2, tb_Name.Text, tb_Time.Text, DH.GetIDToWrite()),ID);
            if (DH.Close)
            {
                Close = true;
                Close();
            }
        }

        private void btn_AddIng_Click(object sender, RoutedEventArgs e)
        {
            var Hh = new AddProduct();
            Hh.ShowDialog();
            IH = new IngredientHandler();
            RefreshObs();
           // if (Hh.Refresh) lv_Ingredients.ItemsSource = IH.IngList;

        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_AddIngToDish_Click(object sender, RoutedEventArgs e)
        {
            if (lv_Ingredients.SelectedItem!=null)
            {
                var Hh = lv_Ingredients.SelectedIndex;
                SubList2.Add(new Amount(ObsIng[Hh],tb_Amount.Text));
                SubList2.OrderBy(x => x.Ingredient.Name);
                tb_Amount.Text = null;
                tb_Saerch.Text = null;
                //lv_ToDish.ItemsSource = SubList2;
            }
        }

        private void btn_RemoveIngToDish_Click(object sender, RoutedEventArgs e)
        {
            if (lv_ToDish.SelectedItem!=null)
            {
                SubList2.RemoveAt(lv_ToDish.SelectedIndex);
            }
        }

        private void tb_Saerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshObs();

        }

        private void RefreshObs()
        {
            if (tb_Saerch.Text != null)
            {
                ObsIng = IH.GetObservableList(IH.GetListOfIngredientsWithSetName(tb_Saerch.Text));
                lv_Ingredients.ItemsSource = ObsIng;
            }
            else
            {
                ObsIng = IH.GetObservableList(IH.IngList);
                lv_Ingredients.ItemsSource = ObsIng;
            }
        }
    }
}
