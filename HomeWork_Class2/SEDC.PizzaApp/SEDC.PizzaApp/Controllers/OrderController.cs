using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEDC.PizzaApp.Models;

namespace SEDC.PizzaApp.Controllers
{
    public class OrderController : Controller
    {

        [Route("Orders")]
        public IActionResult Index()
        {
            var orders = StaticDb.Orders;
            return View(orders);
        }

        public IActionResult Details(int? id)
        {
            if (id != null)
            {
                Order order = StaticDb.Orders.FirstOrDefault(o => o.ID == id);
                if (order != null)
                {
                    return View(order);
                }
               else
                {
                    return View("WrongId");
                }
            }
            else
            {
                return new EmptyResult();
            }
        }

        public IActionResult Jsondata()
        {
            Order order = new Order()
            {
                ID = 1,
                Type = "Online"
            }; 
            return new JsonResult(order);
        }

        public IActionResult Redirect()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
