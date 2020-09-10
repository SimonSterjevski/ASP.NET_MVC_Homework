using SEDC.PizzaApp.Models.Domain;
using SEDC.PizzaApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.PizzaApp.Models.Mappers
{
    public class PizzaMapper
    {
        public static PizzaDDViewModel ToPizzaDDViewModel(Pizza pizza)
        {
            return new PizzaDDViewModel
            {
                Id = pizza.Id,
                PizzaName = pizza.Name
            };
        }
    }
}
