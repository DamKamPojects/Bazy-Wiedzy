using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW_PrototypWPF
{
    public class Ingredient
    {
        public int ID { get; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int CategoryID { get; }
        public bool IsMeat { get; }
        private CategoryHandler CH = new CategoryHandler();

        #region Stare konstruktory
        public Ingredient(List<Ingredient> list, string name, string category, string ismeat)
        {
            ID = list.Count;
            Name = name;
            Category = category;
            SetCategoryID();
            IsMeat = (ismeat == "false") ? false : true;
        }

        public Ingredient(ObservableCollection<Ingredient> list, string name, string category, string ismeat)
        {
            ID = list.Count;
            Name = name;
            Category = category;
            SetCategoryID();
            IsMeat = (ismeat == "false") ? false : true;
        }

        public Ingredient()
        {

        }

        #endregion

        public Ingredient(string name, string category, string ismeat, int id)
        {
            ID = id;
            Name = name;
            Category = category;
            //SetCategoryID();
            CategoryID = CH.CatList.Find(x => x.Name == category).ID;
            IsMeat = (ismeat == "False") ? false : true;
        }

        //wstępne, do kategorii pewnie będzie inny plik
        int SetCategoryID()
        {
            if (Category == "Owoce i warzywa") return 0;
            if (Category == "Nabiał") return 1;
            if (Category == "Pieczywo") return 2;
            //itp., itd.
            return 666;
        }
    }
}
