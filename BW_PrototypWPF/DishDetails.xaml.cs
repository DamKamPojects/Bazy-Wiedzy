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
    /// Logika interakcji dla klasy DishDetails.xaml
    /// </summary>
    public partial class DishDetails : Window
    {

        List<Amount> lv2 = new List<Amount>();
        
        public DishDetails()
        {
            InitializeComponent();
        }

        public DishDetails(Dish input, DishHandler dh)
        {
            
            InitializeComponent();
            tb_Name.Text = input.Name;
            tb_Time.Text = input.Time;
            
            var Hh = dh.DishList.IndexOf(input);
            lv2 = dh.GetSelectedDishAmountAsList(Hh);
            Ingredients.ItemsSource = lv2;
        }

        public DishDetails(int id, DishHandler dh)
        {

            InitializeComponent();
            var input = dh.GetDish(id);
            tb_Name.Text = input.Name;
            tb_Time.Text = input.Time;

            var Hh = dh.DishList.IndexOf(input);
            lv2 = dh.GetSelectedDishAmountAsList(Hh);
            Ingredients.ItemsSource = lv2;
        }

        private void Btn_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
