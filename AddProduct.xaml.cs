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
using System.Windows.Shapes;

namespace BW_PrototypWPF
{
    /// <summary>
    /// Logika interakcji dla klasy AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        CategoryHandler CH = new CategoryHandler();
        IngredientHandler IH;
        public bool Refresh;
        int State;
        int id;
        public bool Close = false;

        public AddProduct(IngredientHandler ih)
        {
            InitializeComponent();
            IH = ih;
            lv_Category.ItemsSource = CH.CatList;
            //cbox_Meat.IsThreeState = true;
        }
        public AddProduct()
        {
            InitializeComponent();
            IH = new IngredientHandler();
            lv_Category.ItemsSource = CH.CatList;
            State = 0;
            Title = "Dodaj składnik";
        }

        public AddProduct(Ingredient ing)
        {
            //State = state;
            InitializeComponent();
            IH = new IngredientHandler();

            tb_Name.Text = ing.Name;
            id = ing.ID;
            lv_Category.ItemsSource = CH.CatList;
            lv_Category.SelectedIndex = CH.GetIndex(ing);
            State = 1;
            Title = "Aktualizacja składnika";
            btn_AddIng.Content = "Aktualizuj składnik";
            if (ing.IsMeat) cbox_Meat.IsChecked = false;
            else cbox_Meat.IsChecked = true;
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Refresh = false;
        }

        private void btn_AddIng_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name.Text!=null && lv_Category.SelectedItem!=null)
            {
                switch (State)
                {
                    case 0:
                        string Meat;
                        if (cbox_Meat.IsChecked == false) Meat = "True";
                        else Meat = "False";
                        var Hh = lv_Category.SelectedIndex;
                        var IDToWrite = IH.GetMaxIDToWrite();
                        IH.AddEntryIngredient("Ingredients.txt", new Ingredient(tb_Name.Text, CH.CatList[Hh].Name, Meat, IDToWrite));
                        Close = true;
                        if (IH.Close)
                        {
                            this.Close();
                            Refresh = true;
                        }
                        break;
                    case 1:
                        string Meat2;
                        if (cbox_Meat.IsChecked == false) Meat2 = "True";
                        else Meat2 = "False";
                        var Hh2 = lv_Category.SelectedIndex;
                        IH.UpdateFile("Ingredients.txt", new Ingredient(tb_Name.Text, CH.CatList[Hh2].Name, Meat2, id), id);
                        Close = true;
                        if (IH.Close)
                        {
                            this.Close();
                            Refresh = true;
                        }
                        break;
                    default:
                        break;
                }

            }
            else MessageBox.Show("Produkt musi posiadać nazwę oraz kategorię", " ", MessageBoxButton.OK);
            //IH.AddEntryIngredient
        }

        private void btn_AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var Hh = new AddCategory(CH);
            Hh.ShowDialog();
            if (Hh.Refresh == true) lv_Category.ItemsSource = CH.CatList;
        }

        private void tb_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Search.Text != null) lv_Category.ItemsSource = CH.GetCategoriesWithSetNames(tb_Search.Text);
            else lv_Category.ItemsSource = CH.CatList;
        }
    }
}
