using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SEDC.PizzaApp.DataAccess;
using SEDC.PizzaApp.DataAccess.Implementations;
using SEDC.PizzaApp.Domain.Models;
using SEDC.PizzaApp.Mappers.Order;
using SEDC.PizzaApp.Mappers.Pizza;
using SEDC.PizzaApp.Services.Interfaces;
using SEDC.PizzaApp.ViewModels.Order;

namespace SEDC.PizzaApp.Services.Implementations
{
    public class OrderService : IOrderService
    {
        //only used by the service for its logic
        private IRepository<Order> _orderRepository;
        private IRepository<User> _userRepository;
        private IRepository<Pizza> _pizzaRepository;

        public OrderService(IRepository<Order> orderRepository, IRepository<User> userRepository, IRepository<Pizza> pizzaRepository) // in order for the service to be instantiated, the repository is needed 
        {
            //implementation of the IRepository<Order> interface
            //_orderRepository = new OrderRepository();

            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _pizzaRepository = pizzaRepository;
        }
        public List<OrderDetailsViewModel> GetAllOrders()
        {
            //call to data access layer
            List<Order> orders = _orderRepository.GetAll();
            List<OrderDetailsViewModel> viewModels = new List<OrderDetailsViewModel>();
            foreach (Order order in orders)
            {
                viewModels.Add(order.ToOrderDetailsViewModel());
            }

            return viewModels;
        }

        public OrderDetailsViewModel GetOrderById(int id)
        {
            Order order = _orderRepository.GetById(id);
            if (order == null)
            {
                //log the exception
                throw new Exception($"Order with id {id} does not exist!");
            }

            return order.ToOrderDetailsViewModel();
        }

        public void CreateOrder(OrderViewModel orderViewModel)
        {
            Order order = orderViewModel.ToOrder();
            User user = _userRepository.GetById(order.UserId);
            if (user == null)
            {
                //log exception
                throw new Exception($"User with id {order.UserId} was not found!");
            }

            order.User = user;
            int newOrderId = _orderRepository.Insert(order);
            if (newOrderId <= 0)
            {
                throw new Exception("Something went wrong while saving the new order");
            }
        }

        public void AddPizzaToOrder(PizzaOrderViewModel pizzaOrderViewModel)
        {
            //get the order
            Order order = _orderRepository.GetById(pizzaOrderViewModel.OrderId);
            if (order == null)
            {
                //log
                throw new Exception($"Order with id {pizzaOrderViewModel.OrderId} was not found!");
            }
            //get the pizza 
            Pizza pizza = _pizzaRepository.GetById(pizzaOrderViewModel.PizzaId);
            if (pizza == null)
            {
                //log
                throw new Exception($"Pizza with id {pizzaOrderViewModel.PizzaId} was not found!");
            }
            order.PizzaOrders.Add(new PizzaOrder
            {
                Id = StaticDb.PizzaOrderId++,
                OrderId = order.Id,
                PizzaId = pizza.Id,
                Pizza = pizza,
                PizzaSize = pizzaOrderViewModel.PizzaSize,
                Price = pizzaOrderViewModel.Price
            }); ;
            _orderRepository.Update(order);
        }

        public PizzaOrderViewModel GetPizzaOrder(int id, int idpizza)
        {
            Order order = _orderRepository.GetById(id);
            PizzaOrderViewModel pizzaOrder = order.PizzaOrders.FirstOrDefault(x => x.PizzaId == idpizza).ToPizzaOrderViewModel();
            return pizzaOrder;
        }

       public void RemovePizzaFromOrder(PizzaOrderViewModel pizzaOrderViewModel)
        {
            Order order = _orderRepository.GetById(pizzaOrderViewModel.OrderId);
            if (order == null)
            {
                throw new Exception("Pizza was not found");
            }
            PizzaOrder pizzaOrder = order.PizzaOrders.FirstOrDefault(x => x.Id == pizzaOrderViewModel.Pk);   /*??????*/
            order.PizzaOrders.Remove(pizzaOrder);
            _orderRepository.Update(order);
        }

    }
}
