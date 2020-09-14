using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SEDC.PizzaApp.Domain.Enums;

namespace SEDC.PizzaApp.ViewModels.Order
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "The name of the pizza")]
        public List<string> PizzaNames { get; set; }
        public string UserFullName { get; set; }
        public string PizzaStore { get; set; }
        public double Price { get; set; }
        public bool Delivered { get; set; }
    }
}
