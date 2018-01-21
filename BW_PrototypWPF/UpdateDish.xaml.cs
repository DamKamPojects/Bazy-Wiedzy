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
    /// Logika interakcji dla klasy UpdateDish.xaml
    /// </summary>
    public partial class UpdateDish : Window
    {
        //DishHandler DH;
        //IngredientHandler IH;
        //CategoryHandler CH = new CategoryHandler();
        //List<Amount> SubList = new List<Amount>();
        //ObservableCollection<Amount> SubList2 = new ObservableCollection<Amount>();

        public UpdateDish()
        {
            InitializeComponent();
        }
        //private void btn_Add_Click(object sender, RoutedEventArgs e)
        //{
        //    DH.AddEntryDish("Dish.txt", new Dish(SubList2, tb_Name.Text, tb_Time.Text, DH.GetIDToWrite()));
        //    if (DH.Close) Close();
        //}

        //private void btn_AddIng_Click(object sender, RoutedEventArgs e)
        //{
        //    var Hh = new AddProduct(IH);
        //    Hh.ShowDialog();
        //    if (Hh.Refresh) lv_Ingredients.ItemsSource = IH.IngList;

        //}

        //private void btn_Close_Click(object sender, RoutedEventArgs e)
        //{
        //    Close();
        //}

        //private void btn_AddIngToDish_Click(object sender, RoutedEventArgs e)
        //{
        //    if (lv_Ingredients.SelectedItem != null)
        //    {
        //        var Hh = lv_Ingredients.SelectedIndex;
        //        SubList2.Add(new Amount(IH.IngList[Hh], tb_Amount.Text));
        //        SubList2.OrderBy(x => x.Ingredient.Name);
        //        tb_Amount.Text = null;
        //    }
        //}

        //private void btn_RemoveIngToDish_Click(object sender, RoutedEventArgs e)
        //{
        //    if (lv_ToDish.SelectedItem != null)
        //    {
        //        SubList2.RemoveAt(lv_ToDish.SelectedIndex);
        //    }
        //}
    }
}
