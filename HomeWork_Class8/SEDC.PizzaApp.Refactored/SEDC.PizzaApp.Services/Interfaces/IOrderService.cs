using System;
using System.Collections.Generic;
using System.Text;
using SEDC.PizzaApp.ViewModels.Order;

namespace SEDC.PizzaApp.Services.Interfaces
{
    public interface IOrderService
    {
        List<OrderDetailsViewModel> GetAllOrders();
        OrderDetailsViewModel GetOrderById(int id);
        void CreateOrder(OrderViewModel orderViewModel);
        void AddPizzaToOrder(PizzaOrderViewModel pizzaOrderViewModel);
        OrderViewModel GetOrderForEditing(int id);
        void EditOrder(OrderViewModel orderViewModel);
        void DeleteOrder(int id);
        int GetOrderCount();
    }
}
