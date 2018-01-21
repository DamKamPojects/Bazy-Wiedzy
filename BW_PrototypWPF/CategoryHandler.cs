using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Collections.ObjectModel;

namespace BW_PrototypWPF
{
    public class CategoryHandler : IFileHandler
    {
        public List<Categories> CatList = new List<Categories>();
        //public ObservableCollection<Categories> CatObs = new ObservableCollection<Categories>();
        public bool Close;

        public CategoryHandler()
        {
            LoadData();
        }

        

        public void DeleteFromFile(string filename, int ID)
        {
            throw new NotImplementedException();
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
                        CatList.Add(new Categories(line[1], Convert.ToInt32(line[0])));

                    }

                }
            }
            catch (Exception e) // jeśli nie znajdzie pliku, wyswietli komunikat
            {
                var msg = "Nie udało się odczytać pliku Categories.txt";
                MessageBox.Show(msg + Environment.NewLine + e.Message, " ", MessageBoxButton.OK);
            }
        }

        public void AddEntryCategory(string filename, Categories insert)
        {
            Close = false;
            if (VerifyExistanceOfAddedCat(insert))
            {
                string Path = filename;
                string InsertedString;
                using (StreamReader sr = new StreamReader(Path))
                {
                    InsertedString = sr.ReadToEnd();
                }

                InsertLine(insert, InsertedString, Path);
                RefreshCollection("Categories.txt");
                Close = true;
            }
            else MessageBox.Show("Kategoria o podanej nazwie już istnieje!", "Błąd dodawania kategorii", MessageBoxButton.OK);

        }

        private bool VerifyExistanceOfAddedCat(Categories category)
        {
            foreach (var cat in CatList)
            {
                if (cat.Name.Equals(category.Name)) return false;
            }
            return true;
        }

        private void InsertLine(Categories insert, string previousString, string Path)
        {
            previousString += Environment.NewLine + insert.ID + "," + insert.Name;

            using (StreamWriter sw = File.CreateText(Path))
            {
                sw.Write(previousString);
                sw.Flush();
            }
        }

        private void InsertLine(Categories insert, string previousText, string Path, string RestOfTheFile, int id)
        {
            var ToWrite = previousText + insert.ID + "," + insert.Name + Environment.NewLine + RestOfTheFile;


            //previousText += "," + id + Environment.NewLine + RestOfTheFile;
            using (StreamWriter sw = File.CreateText(Path))
            {
                sw.Write(ToWrite);
                sw.Flush();
            }
        }

        public int GetIDToWrite()
        {
            int output = 0;
            foreach (var cat in CatList)
            {
                if (cat.ID > output) output = cat.ID;
            }
            return output + 1;
        }

        public void RefreshCollection(string filename)
        {
            //CatList.RemoveAll(x => x.ID >= 0);
            CatList = new List<Categories>();
            LoadData();
        }

        public void LoadData()
        {
            OpenFile("Categories.txt");
            CatList.OrderBy(x => x.Name);
           // CatList = CatObs.ToList();

        }

        public int GetIndex(Ingredient input)
        {
            int output = -1;
            for (int i = 0; i < CatList.Capacity; i++)
            {
                if (input.Category == CatList[i].Name) return i;
            }
            return output;
        }

        public void UpdateFile(string filename, Categories update, int id)
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
                        if (line[0] == id.ToString())
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

        public ObservableCollection<Categories> GetObservableList (List<Categories> input)
        {
            var output = new ObservableCollection<Categories>();
            foreach (var cat in input)
            {
                output.Add(cat);
            }
            return output;
        }

        public List<Categories> GetCategoriesWithSetNames(string input)
        {
            var output = new List<Categories>();

            foreach (var cat in CatList)
            {
                if (cat.Name.Contains(input)) output.Add(cat);

            }

            return output;
        }

    }
}
