using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SEDC.PizzaApp.Domain.Enums;

namespace SEDC.PizzaApp.ViewModels.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string PizzaStore { get; set; }
        public bool Delivered { get; set; }
        [Display(Name = "User")]
        public int UserId { get; set; }
    }
}
