using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BW_PrototypWPF
{
    public class DishHandler : IFileHandler
    {
        public List<Dish> DishList = new List<Dish>();
        Dictionary<string, Ingredient> IngDict;
        public List<Amount> SelectedDishIngredientList;
        public bool Close;

        public DishHandler(Dictionary<string, Ingredient> ingDict)
        {
            IngDict = ingDict;
            OpenFile("Dish.txt");
            DishList = DishList.OrderBy(x => x.Name).ToList();
        }

        public DishHandler()
        {
            LoadData();
        }
        public int GetIDToWrite()
        {
            int output = 0;
            foreach (var ing in DishList)
            {
                if (ing.ID > output) output = ing.ID;
            }
            return output + 1;
        }

        public void OpenFile(string filename)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {

                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine().Split(',');
                        var name = new Ingredient();
                        var SubDict = new Dictionary<Ingredient, string>();

                        char[] separator = new char[] { '-' };
                            var am = line[2].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var item in am)
                            {
                                var ToAmount = item.Split('|');
                                name = IngDict[ToAmount[0]];
                                SubDict.Add(name, ToAmount[1] + " " + ToAmount[2]);
                            }
                            //var time = line[1].Split('\'');
                            //var dish = new Dish(line[0], SubDict, time[0] + " " + time[1], Convert.ToInt32(line[3]));
                            var dish = new Dish(line[0], SubDict, line[1], Convert.ToInt32(line[3]));
                        DishList.Add(dish);
                       
                    }

                }
            }
            catch (Exception e) // jeśli nie znajdzie pliku, wyswietli komunikat
            {
                var msg = "Nie udało się odczytać pliku Dish.txt";
                MessageBox.Show(msg + Environment.NewLine + e.Message, " ", MessageBoxButton.OK);
            }
        }

        public List<Amount> GetSelectedDishAmountAsList(int index)
        {          
            SelectedDishIngredientList = new List<Amount>();
            foreach (var item in DishList[index].Amount)
            {
                SelectedDishIngredientList.Add(new Amount(item.Key,item.Value));
            }
            return SelectedDishIngredientList;
        }

        //wszystkie dania zawierające składnik
        public List<Dish> GetDishesWithSelectedIngredients(List<Ingredient> ListOfIngredients)
        {
            var output = new List<Dish>();
            
            foreach (var ingredient in ListOfIngredients)
            {
                foreach (var dish in DishList)
                {
                    foreach (var ing in dish.Amount)
                    {
                        if (ing.Key == ingredient && !output.Contains(dish))
                        {
                            output.Add(dish);
                        }
                    }
                }
            }

            return output;
        }

        public List<Dish> GetVeganDishesWithSelectedIngredients(List<Ingredient> input)
        {
            var output = new List<Dish>();

            foreach (var ingredient in input)
            {
                foreach (var dish in DishList)
                {
                    if (dish.Vegan)
                    {
                        foreach (var ing in dish.Amount)
                        {

                            if (ing.Key == ingredient && !output.Contains(dish) && ing.Key.IsMeat == false)
                            {
                                output.Add(dish);
                            }

                        }
                    }
                }
            }

            return output;
        }

        //dania zawierające wszystkie składniki
        public List<Dish> GetDishesWithAllSelectedIngredients(List<Ingredient> ListOfIngredients)
        {
            var output = new List<Dish>();

            if (ListOfIngredients.Count == 0) return output;

            foreach (var dish in DishList)
            {
                var counter = 0;
                foreach (var ingredient in ListOfIngredients)
                {
                    bool ContainsIngredient = false;
                    foreach (var dishIngredient in dish.Amount)
                    {

                        if (dishIngredient.Key == ingredient)
                        {
                            ContainsIngredient = true;
                        }
                    }
                    if (ContainsIngredient == true) { counter++; }
                }
                if (counter==ListOfIngredients.Count) { output.Add(dish); }
            }

            return output;
        }

        public List<Dish> GetVeganDishesWithAllSelectedIngredients(List<Ingredient> input)
        {
            var output = new List<Dish>();

            if (input.Count == 0) return output;

            foreach (var dish in DishList)
            {
                if (dish.Vegan)
                {
                    var counter = 0;
                    foreach (var ingredient in input)
                    {

                        bool ContainsIngredient = false;
                        foreach (var dishIngredient in dish.Amount)
                        {

                            if (dishIngredient.Key == ingredient && dishIngredient.Key.IsMeat == false)
                            {
                                ContainsIngredient = true;
                            }
                        }
                        if (ContainsIngredient == true) counter++;

                    }
                    if (counter == input.Count) { output.Add(dish); }
                }
            }

            return output;
        }

        public List<Dish> GetListOfDishesWithSetNames(string input)
        {
            var output = new List<Dish>();

            foreach (var dish in DishList)
            {
                if (dish.Name.Contains(input)) output.Add(dish);

            }

            return output;
        }

        public List<Dish> GetVeganDishesWithSetNames(string input)
        {
            
            List<Dish> output = new List<Dish>();
            
            foreach (var dish in DishList)
            {
                bool Meat = false;
                foreach (var ing in dish.Amount)
                {
                    if (ing.Key.IsMeat) Meat = true;
                }
                if (!Meat && dish.Name.Contains(input)) output.Add(dish);
            }
            return output;
        }

        public List<Dish> GetVeganDishes()
        {
            List<Dish> output = new List<Dish>();

            foreach (var dish in DishList)
            {
                bool Meat = false;
                foreach (var ing in dish.Amount)
                {
                    if (ing.Key.IsMeat) Meat = true;
                }
                if (!Meat) output.Add(dish);
            }
            return output;
        }

        public void UpdateFile(string filename, Dish update, int id)
        {

            Close = false;
            var Hh = StringForUpdateAndDelete(filename, id);
            InsertLine(update, Hh[0], filename, Hh[1], id);
            Close = true;
        }

        public void DeleteFromFile(string filename, int ID)
        {
            try
            {
                Close = false;
                var Hh = StringForUpdateAndDelete(filename, ID);
                InsertLine(Hh[0], filename, Hh[1]);
                Close = true;
            }
            catch (Exception e)
            {
                
                MessageBox.Show("Problem z usunięciem wpisu z pliku Dish.txt" + Environment.NewLine + e.Message, " ", MessageBoxButton.OK);
            }
        }

        private List<string> StringForUpdateAndDelete(string filename, int id)
        {
            string Path = filename;
            string previousString = null;
            string restOfTheFile = null;
            //string updatedString = null;
            bool SavePreviousLines = true;
            
            using (StreamReader srChecking = new StreamReader(Path))
            {
                bool StartedNextOne = false;
                while (!srChecking.EndOfStream)
                {
                    var wholeline = srChecking.ReadLine();
                    var line = wholeline.Split(',');
                    
                    if (!SavePreviousLines && !StartedNextOne) restOfTheFile += wholeline;
                    else if (!SavePreviousLines && StartedNextOne)
                    {
                        restOfTheFile += Environment.NewLine + wholeline;
                        StartedNextOne = true;
                    }
                    else
                    {
                        if (line[3] == id.ToString())
                        {

                            SavePreviousLines = false;
                        }
                        else previousString += wholeline + Environment.NewLine;
                    }
                }


            }
            //InsertLine(update, previousString, Path, restOfTheFile, id);
            return new List<string> { previousString, restOfTheFile };
            
        }

        //bardzo wstępnie, będzie do poprawy pewnie
        public void AddEntryDish(string filename, Dish insert)
        {
            Close = false;
            if (VerifyExistanceOfAddedDish(insert))
            {
                string Path = filename;
                string InsertedString;
                using (StreamReader sr = new StreamReader(Path))
                {
                    InsertedString = sr.ReadToEnd();

                }
                InsertLine(insert, InsertedString, Path);
                RefreshCollection("Dish.txt");
                Close = true;
                
            }
            else MessageBox.Show("Danie o podanej nazwie już istnieje!", "Błąd wprowadzania dania", MessageBoxButton.OK);
        }

        private void InsertLine(Dish insert, string previousText, string Path)
        {
            previousText += Environment.NewLine + insert.Name + "," + insert.Time + ",";
            foreach (var ingredient in insert.Amount)
            {
                var Hh = ingredient.Value.Split(' ');
                if (Hh.Count() == 2) previousText += ingredient.Key.Name + "|" + Hh[0] + "|" + Hh[1] + "-";
                else previousText += ingredient.Key.Name + "||" + Hh[0] + "-";
            }

            previousText += "," + insert.ID;
            using (StreamWriter sw = File.CreateText(Path))
            {
                sw.Write(previousText);
                sw.Flush();
            }
        }

        private void InsertLine(Dish insert, string previousText, string Path, string RestOfTheFile, int id)
        {
            previousText += insert.Name + "," + insert.Time + ",";
            foreach (var ingredient in insert.Amount)
            {
                var Hh = ingredient.Value.Split(' ');
                if (Hh.Count() == 2) previousText += ingredient.Key.Name + "|" + Hh[0] + "|" + Hh[1] + "-";
                else previousText += ingredient.Key.Name + "||" + Hh[0] + "-";
            }

            previousText += "," + id + Environment.NewLine + RestOfTheFile;
            using (StreamWriter sw = File.CreateText(Path))
            {
                sw.Write(previousText);
                sw.Flush();
            }
        }

        private void InsertLine(string previousText, string Path, string RestOfTheFile)
        {
            
            var ToWrite = previousText + RestOfTheFile;
            using (StreamWriter sw = File.CreateText(Path))
            {
                sw.Write(ToWrite);
                sw.Flush();
            }
        }

        private bool VerifyExistanceOfAddedDish(Dish dish)
        {
            foreach (var d in DishList)
            {
                if (d.Name.Equals(dish.Name)) return false;
            }
            return true;
        }

        public void RefreshCollection(string filename)
        {
            DishList = new List<Dish>();
            IngDict = new Dictionary<string, Ingredient>();
            LoadData();
        }

        public void LoadData()
        {
            var Hh = new IngredientHandler();
            IngDict = Hh.IngDict;
            OpenFile("Dish.txt");
            DishList = DishList.OrderBy(x => x.Name).ToList();
        }

        public Dish GetDish(int id)
        {
            foreach (var dish in DishList)
            {
                if (dish.ID == id) return dish;
            }
            return null;
        }

        public ObservableCollection<Dish> GetObservableList(List<Dish> input)
        {
            var output = new ObservableCollection<Dish>();
            foreach (var item in input)
            {
                output.Add(item);
            }
            return output;
        }

        
    }
}
