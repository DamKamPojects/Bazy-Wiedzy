using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW_PrototypWPF
{
    public class AmountForListView
    {
        public Ingredient Ing;
        public string Amount;

        public AmountForListView(Ingredient ing, string amount)
        {
            Ing = ing;
            Amount = amount;
        }
    }
}
