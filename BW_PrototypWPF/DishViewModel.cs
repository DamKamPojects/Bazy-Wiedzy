using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW_PrototypWPF
{
    class DishViewModel
    {
        public ObservableCollection<Dish> DishListVM { get; set; }
        public Dish SelectedDish { get; set; }

        public DishViewModel(ObservableCollection<Dish> dishListVM)
        {
            DishListVM = new ObservableCollection<Dish>(dishListVM);
        }
        
    }
}
