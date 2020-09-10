using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEDC.PizzaApp.Models.Domain;
using SEDC.PizzaApp.Models.ViewModels;

namespace SEDC.PizzaApp.Models.Mappers
{
    public static class OrderMapper
    {
        /// <summary>
        /// Mapper from domain to view model
        /// </summary>
        /// <param name="order">the domain order model</param>
        /// <returns></returns>
        public static OrderDetailsViewModel OrderToOrderDetailsViewModel(Order order)
        {
            // decimal price = order.Price + order.Price*0.2
            return new OrderDetailsViewModel
            {
                Id = order.Id,
                PaymentMethod = order.PaymentMethod,
                PizzaName = order.Pizza.Name,
                UserFullName = $"{order.User.FirstName} {order.User.LastName}",
                Price = order.Pizza.Price,
                Delivered = order.Delivered,
                PizzaStore = order.PizzaStore
            };
        }

        public static Order ToOrder(OrderViewModel orderViewModel)
        {
            //find the Pizza domain model with that pizza name
            Pizza pizza = StaticDb.Pizzas.FirstOrDefault(x => x.Name.Equals(orderViewModel.PizzaName));
            //find the user with the right user id
            User user = StaticDb.Users.FirstOrDefault(x => x.Id == orderViewModel.UserId);

            return new Order
            {
                Id = orderViewModel.Id,
                Delivered = orderViewModel.Delivered,
                PaymentMethod = orderViewModel.PaymentMethod,
                PizzaStore = orderViewModel.PizzaStore,
                User = user,
                Pizza = pizza
            };
        }

        public static OrderViewModel OrderToOrderViewModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                Delivered = order.Delivered,
                PaymentMethod = order.PaymentMethod,
                PizzaStore = order.PizzaStore,
                PizzaName = order.Pizza.Name,
                UserId = order.User.Id
            };
        }
    }
}
