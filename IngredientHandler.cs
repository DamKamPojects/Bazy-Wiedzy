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
    public class IngredientHandler : IFileHandler
    {
        public List<Ingredient> IngList = new List<Ingredient>();
        public Dictionary<string, Ingredient> IngDict = new Dictionary<string, Ingredient>();
        private CategoryHandler CH = new CategoryHandler();
        public bool Close;

        public IngredientHandler()
        {
            LoadData();
        }

        public int GetMaxIDToWrite()
        {
            int output = 0;
            foreach (var ing in IngList)
            {
                if (ing.ID > output) output = ing.ID;
            }
            return output+1;
        }

       

        public void AddEntryIngredient(string filename, Ingredient insert)
        {
            Close = false;
            if (VerifyExistanceOfAddedIng(insert))
            {
                string Path = filename;
                string InsertedString;
                using (StreamReader sr = new StreamReader(Path, Encoding.Default))
                {
                    InsertedString = sr.ReadToEnd();

                }

                InsertLine(insert, InsertedString, Path);

                RefreshCollection("Ingredients.txt");
                //IngList.Add(insert);
                //IngDict.Add(insert.Name, insert);
                Close = true;
            }
            else MessageBox.Show("Składnik o podanej nazwie już istnieje!", "Błąd wprowadzania składnika", MessageBoxButton.OK);
            
        }

        private bool VerifyExistanceOfAddedIng(Ingredient ingredient)
        {
            foreach (var ing in IngList)
            {
                if (ing.Name.Equals(ingredient.Name)) return false;
            }
            return true;
        }

        private void InsertLine(Ingredient insert, string previousString, string Path)
        {
            previousString += Environment.NewLine + insert.Name + "," + insert.CategoryID + "," + insert.IsMeat.ToString() + "," + insert.ID;

            using (StreamWriter sw = new StreamWriter(Path, false, Encoding.Default)) /*File.CreateText(Path)*/
            {
                sw.Write(previousString, Encoding.Default);
                sw.Flush();
            }
        }

        private void InsertLine(Ingredient insert, string previousText, string Path, string RestOfTheFile, int id)
        {
            var ToWrite = previousText + Environment.NewLine + insert.Name + "," + insert.CategoryID + "," + insert.IsMeat.ToString() + "," + insert.ID + RestOfTheFile;


            //previousText += "," + id + Environment.NewLine + RestOfTheFile;
            using (StreamWriter sw = new StreamWriter(Path, false, Encoding.Default))
            {
                sw.Write(ToWrite, Encoding.Default);
                sw.Flush();
            }
        }

        private void InsertLine(string previousText, string Path, string RestOfTheFile)
        {
            string ToWrite;
            if (RestOfTheFile != null) ToWrite = previousText + RestOfTheFile;
            else ToWrite = previousText;
            using (StreamWriter sw = new StreamWriter(Path, false, Encoding.Default))
            {
                sw.Write(ToWrite, Encoding.Default);
                sw.Flush();
            }
        }

        public void OpenFile(string filename)
        {
            
            try
            {
                using (StreamReader sr = new StreamReader(filename, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine().Split(',');
                        var Cat = CH.CatList.Find(x => x.ID == Convert.ToInt32(line[1])).Name;
                        IngList.Add(new Ingredient(line[0], Cat , line[2], Convert.ToInt32(line[3])));
                        IngDict.Add(line[0], IngList[IngList.Count - 1]);
                        
                    }
                }
            }
            catch (Exception e) // jeśli nie znajdzie pliku, wyswietli komunikat
            {
                var msg = "Nie udało się poprawnie odczytać pliku Ingredients.txt";
                MessageBox.Show(msg + Environment.NewLine + e.Message, " ", MessageBoxButton.OK);
            }
        }

        public void UpdateFile(string filename, Ingredient update, int id)
        {
            Close = false;
            var Hh = StringForUpdateAndDelete(filename, id);
            InsertLine(update, Hh[0], filename, Hh[1], id);
            Close = true;
        }

        private List<string> StringForUpdateAndDelete(string filename, int id)
        {
            string Path = filename;
            string previousString = null;
            string restOfTheFile = null;
            //string updatedString = null;
            bool SavePreviousLines = true;

            using (StreamReader srChecking = new StreamReader(Path, Encoding.Default))
            {
                bool StartedNextOne = false;
                int i = 0;
                while (!srChecking.EndOfStream)
                {
                    var wholeline = srChecking.ReadLine();
                    var line = wholeline.Split(',');
                    if (line[3] != id.ToString() && i!=0 && SavePreviousLines) previousString += Environment.NewLine;
                   // if (SavePreviousLines) previousString += Environment.NewLine;
                    //if (!SavePreviousLines && StartedNextOne) restOfTheFile += wholeline;
                    /*else*/ if (!SavePreviousLines/* && !StartedNextOne*/)
                    {
                        restOfTheFile += Environment.NewLine + wholeline;
                        StartedNextOne = true;
                    }
                    else
                    {
                        if (line[3] == id.ToString()) SavePreviousLines = false;

                        else previousString += wholeline;
                    }
                    //if (SavePreviousLines) previousString += Environment.NewLine;
                    i++;
                }


            }
            //InsertLine(update, previousString, Path, restOfTheFile, id);
            return new List<string> { previousString, restOfTheFile };

        }

        public void DeleteFromFile(string filename, int ID, List<Dish> DList)
        {
            try
            {
                var problemDishes = new List<Dish>();
                foreach (var dish in DList)
                {
                    foreach (var ing in dish.Amount)
                    {
                        if (ing.Key.ID == ID)
                        {
                            problemDishes.Add(dish);
                            break;
                        }
                    }
                }
                Close = false;
                if (problemDishes.Count > 0)
                {
                    string msg = null;
                    foreach (var item in problemDishes)
                    {
                        msg += item.Name + ", ";
                    }
                    var mbox = MessageBox.Show("Usunięcie tego składnika spowoduje problemy z następującymi daniami: " + msg + "czy chcesz kontynuować? Kliknięcie \"OK\" spowoduje usunięcie składnika z danego dania. Kliknięcie \"Anuluj\" spowoduje zaniechanie usunięcia składnika", "", MessageBoxButton.OKCancel);
                    if (mbox == MessageBoxResult.OK)
                    {
                        var DH = new DishHandler();
                        DH.RemoveIngredientsFromDishes(problemDishes, ID);
                        var Hh = StringForUpdateAndDelete(filename, ID);
                        InsertLine(Hh[0], filename, Hh[1]);
                        Close = true;
                    }
                }
                else
                {
                    var Hh = StringForUpdateAndDelete(filename, ID);
                    InsertLine(Hh[0], filename, Hh[1]);
                    Close = true;
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("Problem z usunięciem wpisu z pliku Ingredients.txt" + Environment.NewLine + e.Message, " ", MessageBoxButton.OK);
            }
        }

        public List<Ingredient> GetListOfIngredientsWithSetName(string input)
        {
            var output = new List<Ingredient>();

            foreach (var ing in IngList)
            {
                if (ing.Name.Contains(input)) output.Add(ing);
                
            }

            return output;
        }

        public void RefreshCollection(string filename)
        {
            //IngList.RemoveAll(x => x.ID >= 0);
            IngList = new List<Ingredient>();
            IngDict = new Dictionary<string, Ingredient>();
            LoadData();

        }

        public void LoadData()
        {
            OpenFile("Ingredients.txt");
            IngList = IngList.OrderBy(x => x.Name).ToList();
        }

        public ObservableCollection<Ingredient> GetObservableList(List<Ingredient> input)
        {
            var output = new ObservableCollection<Ingredient>();
            foreach (var ing in input)
            {
                output.Add(ing);
            }
            return output;
        }
    }
}
