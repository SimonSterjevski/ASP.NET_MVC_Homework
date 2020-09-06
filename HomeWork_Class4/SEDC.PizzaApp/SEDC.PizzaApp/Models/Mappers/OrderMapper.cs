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
        public static OrderDetailsViewModel OrderToViewModel(Order order)
        {
            // decimal price = order.Price + order.Price*0.2
            return new OrderDetailsViewModel
            {
                Id = order.Id,
                PaymentMethod = order.PaymentMethod,
                PizzaName = order.Pizza.Name,
                UserFullName = $"{order.User.FirstName} {order.User.LastName}",
                UserAddress = order.User.Address
            };
        }
    }
}
