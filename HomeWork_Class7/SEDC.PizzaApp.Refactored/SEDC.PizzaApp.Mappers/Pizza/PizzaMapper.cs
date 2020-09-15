using System;
using System.Collections.Generic;
using System.Text;
using SEDC.PizzaApp.Domain.Models;
using SEDC.PizzaApp.ViewModels.Order;
using SEDC.PizzaApp.ViewModels.Pizza;

namespace SEDC.PizzaApp.Mappers.Pizza
{
    public static class PizzaMapper
    {
        public static PizzaDDViewModel ToPizzaDdViewModel(this Domain.Models.Pizza pizza)
        {
            return new PizzaDDViewModel
            {
                Id = pizza.Id,
                Name = pizza.Name
            };
        }

        public static PizzaViewModel ToPizzaViewModel(this Domain.Models.Pizza pizza)
        {
            return new PizzaViewModel
            {
                Id = pizza.Id,
                Name = pizza.Name,
                IsOnPromotion = pizza.IsOnPromotion
            };
        }

        public static Domain.Models.Pizza ToPizzaDomainModel(this PizzaViewModel pizza)
        {
            return new Domain.Models.Pizza
            {
                Id = pizza.Id,
                Name = pizza.Name,
                IsOnPromotion = pizza.IsOnPromotion,
                PizzaOrders = new List<Domain.Models.PizzaOrder>()
            };
        }

        public static PizzaOrderViewModel ToPizzaOrderViewModel(this PizzaOrder pizzaOrder)
        {
            return new PizzaOrderViewModel
            {
                Pk = pizzaOrder.Id,
                OrderId = pizzaOrder.OrderId,
                PizzaId = pizzaOrder.PizzaId,
                PizzaSize = pizzaOrder.PizzaSize,
                Price = pizzaOrder.Price
            };
        }
    }
}
