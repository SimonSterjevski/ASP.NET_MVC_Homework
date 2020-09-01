using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEDC.PizzaApp.Models;

namespace SEDC.PizzaApp
{
    public class StaticDb
    {
        public static List<Pizza> Pizzas = new List<Pizza>
            {
                new Pizza()
                {
                    Id=1,
                    IsOnPromotion = true,
                    Name="Margarita"
                },
                new Pizza()
                {
                    Id=2,
                    IsOnPromotion = false,
                    Name="Capri"
                }
            };

        public static List<Order> Orders = new List<Order>
        {
            new Order()
            {
                ID = 2,
                Type = "Online"
            },
            new Order()
            {
                ID = 3,
                Type = "Mobile"
            },
            new Order()
            {
                ID = 4,
                Type = "Restaurant"
            },
            new Order()
            {
                ID = 5,
                Type = "Online"
            },
        };
    }
}
