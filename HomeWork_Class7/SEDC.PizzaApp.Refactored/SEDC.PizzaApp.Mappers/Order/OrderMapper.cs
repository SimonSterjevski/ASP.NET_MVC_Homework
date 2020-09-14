using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEDC.PizzaApp.ViewModels.Order;
using SEDC.PizzaApp.Domain.Models;

namespace SEDC.PizzaApp.Mappers.Order
{
    public static class OrderMapper
    {
        public static OrderDetailsViewModel ToOrderDetailsViewModel(this Domain.Models.Order order)
        {
            double price = 0;
            foreach (var pizzaOrder in order.PizzaOrders)
            {
                price += pizzaOrder.Price;
            }
            return new OrderDetailsViewModel
            {
                Id = order.Id,
                Delivered = order.Delivered,
                PaymentMethod = order.PaymentMethod,
                PizzaStore = order.PizzaStore,
                UserFullName = $"{order.User.FirstName} {order.User.LastName}",
                PizzaNames = order.PizzaOrders.Select(x=>x.Pizza.Name).ToList(),
                Price = price
            };
        }

        public static Domain.Models.Order ToOrder(this OrderViewModel orderViewModel)
        {
            return new Domain.Models.Order
            {
                Id = orderViewModel.Id,
                Delivered = orderViewModel.Delivered,
                PaymentMethod = orderViewModel.PaymentMethod,
                PizzaStore = orderViewModel.PizzaStore,
                PizzaOrders = new List<PizzaOrder> { },
                UserId = orderViewModel.UserId
            };
        }
    }
}
