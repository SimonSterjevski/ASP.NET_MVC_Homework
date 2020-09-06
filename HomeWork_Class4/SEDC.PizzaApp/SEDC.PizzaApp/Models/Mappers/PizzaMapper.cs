using SEDC.PizzaApp.Models.Domain;
using SEDC.PizzaApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.PizzaApp.Models.Mappers
{
    public static class PizzaMapper
    {
        public static List<PizzaViewModel> PizzasToViewModel(this List<Pizza> pizzas)
        {
            List<PizzaViewModel> pizzasViewModel = new List<PizzaViewModel>();
            foreach (Pizza pizza in pizzas)
            {
                decimal price = pizza.Price;
                if (pizza.HasExtras)
                {
                    price += 10;
                }
                pizzasViewModel.Add(new PizzaViewModel()
                {
                    Id = pizza.Id,
                    Pizza = pizza.Name,
                    Price = price,
                    PizzaSize = pizza.PizzaSize,
                    IsOnPromotion = pizza.IsOnPromotion
                }); 
            };
            return pizzasViewModel;
        }

        //public static PizzaViewModel PizzaToViewModel(this Pizza pizza)
        //{

        //    return new PizzaViewModel()
        //    {
        //        Id = pizza.Id,
        //        Pizza = pizza.Name,
        //        Price = pizza.Price,
        //        PizzaSize = pizza.PizzaSize
        //        IsOnPromotion = pizza.IsOnPromotion
        //    };
        //}
    }
}
