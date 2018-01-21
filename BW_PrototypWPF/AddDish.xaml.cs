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
        int State;
        int ID;

        public AddDish()
        {
            InitializeComponent();
            IH = new IngredientHandler();
            DH = new DishHandler(IH.IngDict);
            State = 1;

            InitializeComponent();
            lv_Ingredients.ItemsSource = IH.IngList;
            lv_ToDish.ItemsSource = SubList2;
        }

        public AddDish(Dish updated)
        {
            InitializeComponent();
            IH = new IngredientHandler();
            DH = new DishHandler(IH.IngDict);
            State = 2;
            ID = updated.ID;

            InitializeComponent();
            btn_Add.Content = "Zaktualiuj danie";
            lv_Ingredients.ItemsSource = IH.IngList;
            foreach (var item in updated.Amount)
            {
                SubList2.Add(new Amount(item.Key, item.Value));
            }
            lv_ToDish.ItemsSource = SubList2;//.OrderBy(x=>x.Ingredient.Name);
            tb_Name.Text = updated.Name;
            tb_Time.Text = updated.Time;
        }

        public AddDish(DishHandler dh, IngredientHandler ih)
        {
            DH = dh;
            IH = ih;
            InitializeComponent();
            lv_Ingredients.ItemsSource = ih.IngList;
            lv_ToDish.ItemsSource = SubList2;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
           if(State==1) DH.AddEntryDish("Dish.txt", new Dish(SubList2, tb_Name.Text, tb_Time.Text, DH.GetIDToWrite()));
            if (State == 2) DH.UpdateFile("Dish.txt", new Dish(SubList2, tb_Name.Text, tb_Time.Text, DH.GetIDToWrite()),ID);
            if (DH.Close) Close();
        }

        private void btn_AddIng_Click(object sender, RoutedEventArgs e)
        {
            var Hh = new AddProduct(IH);
            Hh.ShowDialog();
            if (Hh.Refresh) lv_Ingredients.ItemsSource = IH.IngList;

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
                SubList2.Add(new Amount(IH.IngList[Hh],tb_Amount.Text));
                SubList2.OrderBy(x => x.Ingredient.Name);
                tb_Amount.Text = null;
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
    }
}
