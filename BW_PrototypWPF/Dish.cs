using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW_PrototypWPF
{
    public class Dish
    {
        public int ID { get; }
        public string Name { get; set; }
        public Dictionary<Ingredient, string> Amount { get; set; }
        public string Time { get; set; }
        public bool Vegan;

        #region Stare konstruktory
        public Dish(List<Dish> list, string name, Dictionary<Ingredient, string> amount, string time, int id)
        {
            ID = id;
            Name = name;
            Amount = new Dictionary<Ingredient, string>();
            foreach (var item in amount)
            {
                Amount.Add(item.Key, item.Value);
            }
            Time = time;
        }

        public Dish(ObservableCollection<Amount> list, string name, string time, int id)
        {
            ID = id;
            Name = name;
            Amount = new Dictionary<Ingredient, string>();
            foreach (var item in list)
            {
                if (item.Ingredient.Name!=name) Amount.Add(item.Ingredient, item.Quantity);
            }
            Time = time;
        }
        #endregion
        public Dish(List<Amount> list, string name, string time, int id)
        {
            ID = id;
            Name = name;
            Amount = new Dictionary<Ingredient, string>();
            foreach (var item in list)
            {
                if (item.Ingredient.Name != name) Amount.Add(item.Ingredient, item.Quantity);
            }
            Time = time;
        }

        public Dish(string name, Dictionary<Ingredient, string> amount, string time, int id)
        {
            ID = id;
            Name = name;
            Amount = new Dictionary<Ingredient, string>();
            foreach (var item in amount)
            {
                Amount.Add(item.Key, item.Value);
            }
            Time = time;
            Vegan = CheckVegan();
        }

        private bool CheckVegan()
        {
            foreach (var item in Amount)
            {
                if (item.Key.IsMeat) return false;
            }
            return true;
        }
    }
}
