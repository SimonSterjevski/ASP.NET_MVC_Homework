using System;
using System.Collections.Generic;
using System.Text;
using SEDC.PizzaApp.Domain.Enums;

namespace SEDC.PizzaApp.ViewModels.Order
{
    public class PizzaOrderViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PizzaId { get; set; }
        public PizzaSize PizzaSize { get; set; }
        public double Price { get; set; }
    }
}
