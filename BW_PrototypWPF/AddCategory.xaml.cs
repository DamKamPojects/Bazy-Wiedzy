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
    /// Logika interakcji dla klasy AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        CategoryHandler CH;
        public bool Refresh;
        Categories Cat;
        int State;
        public bool Close;

        public AddCategory(Categories cat)
        {
            InitializeComponent();
            CH = new CategoryHandler();
            btn_OK.Content = "Aktualizuj kategorię";
            Cat = cat;
            tb_Name.Text = Cat.Name;
            State = 1;
            Close = false;
        }

        public AddCategory(CategoryHandler ch)
        {
            CH = ch;
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if (State!=1)
            {
                CH.AddEntryCategory("Categories.txt", new Categories(tb_Name.Text, CH.GetIDToWrite()));
                if (CH.Close == true) Close();
                Refresh = true;
            }
            else
            {
                CH.UpdateFile("Categories.txt", new Categories(tb_Name.Text, Cat.ID), Cat.ID);
                if (CH.Close == true)
                {
                    Close = true;
                    Close();
                }
            }
        }

        private void btn_Anuluj_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Refresh = false;
        }
    }
}
