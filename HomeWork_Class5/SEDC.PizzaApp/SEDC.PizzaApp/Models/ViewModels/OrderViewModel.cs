using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SEDC.PizzaApp.Models.Enums;

namespace SEDC.PizzaApp.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string PizzaStore { get; set; }
        public bool Delivered { get; set; }
        [Display(Name="User")]
        public int UserId { get; set; }
        public string PizzaName { get; set; }
    }
}
