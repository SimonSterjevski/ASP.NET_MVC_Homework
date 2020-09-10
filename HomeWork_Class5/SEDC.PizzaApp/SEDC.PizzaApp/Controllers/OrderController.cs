using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEDC.PizzaApp.Models.Domain;
using SEDC.PizzaApp.Models.Mappers;
using SEDC.PizzaApp.Models.ViewModels;

namespace SEDC.PizzaApp.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "List of orders";
            ViewData.Add("Message",$"The numbers of orders is: {StaticDb.Orders.Count}");
            ViewData["User"] = StaticDb.Users.First();
            List<Order> orders = StaticDb.Orders;
            List<OrderDetailsViewModel> orderDetailsViewModels = new List<OrderDetailsViewModel>();
            foreach (Order order in orders)
            {
                //orderDetailsViewModels.Add(new OrderDetailsViewModel
                //{
                //    Id = order.Id,
                //    PaymentMethod = order.PaymentMethod,
                //    PizzaName = order.Pizza.Name,
                //    UserFullName = $"{order.User.FirstName} {order.User.LastName}"
                //});
                orderDetailsViewModels.Add(OrderMapper.OrderToOrderDetailsViewModel(order));
            }
            return View(orderDetailsViewModels);
        }

        public IActionResult Details(int? id)
        {
            ViewBag.User = StaticDb.Users.First(); //if we want to return the user additionally
            ViewBag.Title = "Order Details";
            if (id == null)
            {
                //return new EmptyResult();
                return View("BadRequest");
            }

            Order order = StaticDb.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                // return new EmptyResult();
                return View("ResourceNotFound");
            }

            OrderDetailsViewModel orderDetailsViewModel = OrderMapper.OrderToOrderDetailsViewModel(order);

            return View(orderDetailsViewModel);
        }
        /// <summary>
        /// this is the method that returns the View with the form in which we will fill the data for a new order
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreateOrder() 
        {
            //we have to send an empty model so that the form data is packed into that kind of model when it is sent via post
            OrderViewModel orderViewModel = new OrderViewModel();
            //we have to send the users, so that <option> items are generated in the select list for users
            ViewBag.Users = StaticDb.Users.Select(x => UserMapper.ToUserDDViewModel(x));
            ViewBag.Pizzas = StaticDb.Pizzas.Select(x => PizzaMapper.ToPizzaDDViewModel(x));
            return View(orderViewModel);
        }
         //<summary>
         //this is the method that receives the form data packed in OrderViewModel
         //</summary>
         //<param name = "orderViewModel" > the data from the form packed in OrderViewModel</param>
         //<returns></returns>
        [HttpPost]
        public IActionResult CreateOrderPost(OrderViewModel orderViewModel)
        {
            //increment the id in the database
            orderViewModel.Id = ++StaticDb.OrderId;
            //validation for pizza, we have to validate if the pizza name is of an existing pizza
            //Pizza pizza = StaticDb.Pizzas.FirstOrDefault(x => x.Name.Equals(orderViewModel.PizzaName));
            //validation for user - to do
            if (orderViewModel.PizzaName == null)
                return View("BadRequest");
            //we add only domain models in the database
            StaticDb.Orders.Add(OrderMapper.ToOrder(orderViewModel));

            return RedirectToAction("Index");
        }
        /// <summary>
        /// This is the action that returns the view with the form filled with data for editing
        /// </summary>
        /// <param name="id">id of the order that we want to edit</param>
        /// <returns></returns>
        public IActionResult EditOrder(int? id)
        {
            if (id == null)
            {
                return View("BadRequest");
            }
            Order order = StaticDb.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return View("ResourceNotFound");
            }
            //we want to send view model to the view
            OrderViewModel orderViewModel = OrderMapper.OrderToOrderViewModel(order);
            //we have to send the users, so that <option> items are generated in the select list for users
            //the right user is bound by mapping the UserId from the view model with the value attribute from the <option> tags
            ViewBag.Users = StaticDb.Users.Select(x => UserMapper.ToUserDDViewModel(x));
            return View(orderViewModel);
        }
        /// <summary>
        /// This is the method that receives the edited data from the form
        /// </summary>
        /// <param name="orderViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditOrderPost(OrderViewModel orderViewModel)
        {
            //we have to validate if the order we want to edit exists
            Order order = StaticDb.Orders.FirstOrDefault(x => x.Id == orderViewModel.Id);
            if (order == null)
                return View("ResourceNotFound");

            Pizza pizza = StaticDb.Pizzas.FirstOrDefault(x => x.Name.Equals(orderViewModel.PizzaName));
            //validation for user - to do
            if (pizza == null)
                return View("BadRequest");

            //=====!!!!!!!!!!!!Way of update that won't work!!!!!!!!!=================================
            //We map the order viewmodel in a domain model
            //Order editedOrder = OrderMapper.ToOrder(orderViewModel);

            //We take by reference the order we want to edit
            //Order orderDb = StaticDb.Orders.First(x => x.Id == orderViewModel.Id);

            //we update the reference to point at editedOrder
            //orderDb = editedOrder; =========== this is not correct because you are just changing the reference of orderDb
            //==========================================================

            //=================First way of update===================
            //Take the index of the order you want to update
            //var index = StaticDb.Orders.FindIndex(x => x.Id == orderViewModel.Id);
            
            //Map the view model to domain model
            //Order editedOrder = OrderMapper.ToOrder(orderViewModel);
            
            //Assign the new domain model to the member of the list with that index
            //StaticDb.Orders[index] = editedOrder;
            //=======================================================================

            //=================Second way of update=======================
            //Take the order by reference and update each property
            //Order orderDb = StaticDb.Orders.First(x => x.Id == orderViewModel.Id);
            //orderDb.PizzaStore= orderViewModel.PizzaStore; 
            //orderDb.PaymentMethod= orderViewModel.PaymentMethod; etc..
            //=============================================================


            //===============Third way====================
            //Look for the order for each property update and update the property
            StaticDb.Orders.First(x => x.Id == orderViewModel.Id).PizzaStore = orderViewModel.PizzaStore;
            StaticDb.Orders.First(x => x.Id == orderViewModel.Id).PaymentMethod = orderViewModel.PaymentMethod;
            StaticDb.Orders.First(x => x.Id == orderViewModel.Id).Delivered = orderViewModel.Delivered;

            User user = StaticDb.Users.First(x => x.Id == orderViewModel.UserId);
            StaticDb.Orders.First(x => x.Id == orderViewModel.Id).Pizza = pizza;
            StaticDb.Orders.First(x => x.Id == orderViewModel.Id).User = user;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This is the action that returns the confirmation view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DeleteOrder(int? id)
        {
            if (id == null)
                return View("BadRequest");
            //we have to validate if the order we want to delete exists
            Order order = StaticDb.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return View("ResourceNotFound");
            }
            //we are sending view model to the view
            OrderDetailsViewModel orderDetailsViewModel = OrderMapper.OrderToOrderDetailsViewModel(order);
            return View(orderDetailsViewModel);
        }
        /// <summary>
        /// the action we would hit by using POST and send the id in the body of the request
        /// </summary>
        /// <param name="orderDetailsViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteOrderPost(OrderDetailsViewModel orderDetailsViewModel)
        {
            //find the index of the order
            var index = StaticDb.Orders.FindIndex(x => x.Id == orderDetailsViewModel.Id);
            //check if the order exists
            if (index == -1)
                return View("ResourceNotFound");
            StaticDb.Orders.RemoveAt(index);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// action that we would hit with GET method from the view and send the id in the route
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            //find the index of the order
            var index = StaticDb.Orders.FindIndex(x => x.Id == id);
            //check if the order exists
            if (index == -1)
                return View("ResourceNotFound");
            StaticDb.Orders.RemoveAt(index);
            return RedirectToAction("Index");
        }
    }
}
