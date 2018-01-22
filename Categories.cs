using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW_PrototypWPF
{
    public class Categories
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int ID { get; private set; }

        public Categories(string _name, int id)
        {
            Name = _name;
            ID = id;
        }
    }
}
