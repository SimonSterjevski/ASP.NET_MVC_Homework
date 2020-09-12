using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public static PizzaDDViewModel ToPizzaDDViewModel(Pizza pizza)
        {
            return new PizzaDDViewModel
            {
                Id = pizza.Id,
                PizzaName = pizza.Name
            };
        }

        public static PizzaViewModel ToPizzaViewModel(this Pizza pizza)
        {
            return new PizzaViewModel
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Price = pizza.Price
            };
        }

        public static Pizza ToPizzaDomain(this PizzaViewModel pizza)
        {
            return new Pizza
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Price = pizza.Price
            };
        }
    }
}
