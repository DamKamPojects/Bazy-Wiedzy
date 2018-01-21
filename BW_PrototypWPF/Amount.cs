using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW_PrototypWPF
{
    public class Amount
    {
        public Ingredient Ingredient { get; set; }
        public string Quantity { get; set; }

        public Amount(Ingredient ingredient, string quantity)
        {
            Quantity = quantity;
            Ingredient = ingredient;
        }
    }
}
