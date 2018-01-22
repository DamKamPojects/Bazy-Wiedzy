using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW_PrototypWPF
{
    public interface IFileHandler
    {

        void OpenFile(string filename);

        //void UpdateFileDish(string filename, Dish update, int ID);

        //void DeleteFromFile(string filename, int ID);

        void RefreshCollection(string filename);

        void LoadData();

    }
}
