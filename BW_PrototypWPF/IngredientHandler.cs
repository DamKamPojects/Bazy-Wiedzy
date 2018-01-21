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
                using (StreamReader sr = new StreamReader(Path))
                {
                    InsertedString = sr.ReadToEnd();

                }

                InsertLine(insert, InsertedString, Path);

                RefreshCollection("Ingredients.txt");
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
            previousString += Environment.NewLine + insert.Name + "," + insert.Category + "," + insert.IsMeat.ToString() + "," + insert.ID;

            using (StreamWriter sw = File.CreateText(Path))
            {
                sw.Write(previousString);
                sw.Flush();
            }
        }

        private void InsertLine(Ingredient insert, string previousText, string Path, string RestOfTheFile, int id)
        {
            var ToWrite = previousText + insert.Name + "," + insert.Category + "," + insert.IsMeat.ToString() + "," + insert.ID + RestOfTheFile;


            //previousText += "," + id + Environment.NewLine + RestOfTheFile;
            using (StreamWriter sw = File.CreateText(Path))
            {
                sw.Write(ToWrite);
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

        public void OpenFile(string filename)
        {
            //using (StreamReader sread = new StreamReader(filename))
            //{
            //    var Bla = sread.ReadToEnd();
            //}
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {

                    while (!sr.EndOfStream)
                    {
                        // var LineBeforeFormatting = sr.ReadLine();//.Split(',');
                        //var utf8 = Encoding.UTF8;
                        //byte[] utfBytes = utf8.GetBytes(sr.ReadLine());
                        //var line = utf8.GetString(utfBytes, 0, utfBytes.Length).Split(',');
                        //var line = sr.ReadLine().Split(',');

                        //byte[] bytes = Encoding.UTF32.GetBytes(sr.ReadLine());
                        //var line = Encoding.UTF32.GetString(bytes).Split(',');

                        //string utf8String = sr.ReadLine(); ;
                        //string propEncodeString = string.Empty;

                        //byte[] utf8_Bytes = new byte[utf8String.Length];
                        //for (int i = 0; i < utf8String.Length; ++i)
                        //{
                        //    utf8_Bytes[i] = (byte)utf8String[i];
                        //}

                        //propEncodeString = Encoding.UTF8.GetString(utf8_Bytes, 0, utf8_Bytes.Length);
                        //var line = propEncodeString.Split(',');

                        UTF8Encoding encoder = new UTF8Encoding();
                        byte[] bytes = Encoding.UTF8.GetBytes(sr.ReadLine());
                        var bla = encoder.GetString(bytes);
                        var line = bla.Split(',');
                        IngList.Add(new Ingredient(line[0], line[1], line[2], Convert.ToInt32(line[3])));
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
