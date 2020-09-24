using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.PizzaApp.ViewModels.Home
{
    public class HomeViewModel
    {
        public int OrderCount { get; set; }
        public string MostPopularPizza { get; set; }
        public string PizzaOnPromotion { get; set; }
    }
}
