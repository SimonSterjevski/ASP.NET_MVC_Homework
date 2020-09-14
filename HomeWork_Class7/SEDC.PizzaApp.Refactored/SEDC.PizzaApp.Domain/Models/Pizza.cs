using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.PizzaApp.Domain.Models
{
    public class Pizza
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOnPromotion { get; set; }
        public List<PizzaOrder> PizzaOrders { get; set; }
    }
}
