using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEDC.PizzaApp.Models.Domain;
using SEDC.PizzaApp.Models.Enums;

namespace SEDC.PizzaApp
{
    public static class StaticDb
    {
        public static List<Pizza> Pizzas = new List<Pizza>
        {
            new Pizza
            {
                Id=1,
                Name="Kaprichioza",
                Price = 280,
                PizzaSize = PizzaSize.Family,
                HasExtras = true
            },
            new Pizza
            {
                Id =2,
                Name = "Pepperoni",
                Price = 310,
                PizzaSize = PizzaSize.Normal,
                HasExtras = false
            }
        };

        public static List<User> Users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Tanja",
                LastName = "Stojanovska",
                Address = "Address1"
            },
            new User
            {
                Id = 2,
                FirstName = "Kristina",
                LastName = "Spasevska",
                Address = "Address2"
            }
        };

        public static List<Order> Orders = new List<Order>
        {
            new Order
            {
                Id =1,
                PaymentMethod = PaymentMethod.Card,
                User = new User
                {
                    Id = 1,
                    FirstName = "Tanja",
                    LastName = "Stojanovska",
                    Address = "Address1"
                },
                Pizza = new Pizza
                {
                    Id=1,
                    Name="Kaprichioza"
                }
            },
            new Order
            {
                Id =2,
                PaymentMethod = PaymentMethod.Cash,
                User= Users.Last(),
               // User = Users.First(x=>x.Id == 2)
                Pizza = new Pizza
                {
                    Id =2,
                    Name = "Pepperoni"
                }
            }
        };
    }
}
