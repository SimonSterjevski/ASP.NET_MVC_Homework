using Microsoft.EntityFrameworkCore;
using SEDC.PizzaApp.DataAccess.Interfaces;
using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.PizzaApp.DataAccess.EFImplementations
{
    public class PizzaEfRepository : IPizzaRepository
    {

        private PizzaAppContextDB _contextDB;

        public PizzaEfRepository(PizzaAppContextDB efRepository)
        {
            _contextDB = efRepository;
        }
        public void DeleteById(int id)
        {
            Pizza pizza = _contextDB.Pizzas.FirstOrDefault(x => x.Id == id);
            if (pizza == null)
            {
                throw new Exception($"Feedback with id {id} does not exist!");
            }
            _contextDB.Pizzas.Remove(pizza);
            _contextDB.SaveChanges();
        }

        public List<Pizza> GetAll()
        {
            return _contextDB.Pizzas
                .Include(x => x.PizzaOrders)
                .ThenInclude(x => x.Order)
                .ThenInclude(x => x.User)
                .ToList();
        }

        public Pizza GetById(int id)
        {
            return _contextDB.Pizzas
                .Include(x => x.PizzaOrders)
                .ThenInclude(x => x.Order)
                .ThenInclude(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public Pizza GetPizzaOnPromotion()
        {
            return _contextDB.Pizzas.FirstOrDefault(x => x.IsOnPromotion);
        }

        public int Insert(Pizza entity)
        {
            _contextDB.Pizzas.Add(entity);
            return _contextDB.SaveChanges();
        }

        public void UpdateEntity(Pizza entity)
        {
            Pizza pizza = _contextDB.Pizzas.FirstOrDefault(x => x.Id == entity.Id);
            if (pizza == null)
            {
                throw new Exception($"Feedback with id {entity.Id} does not exist!");
            }
            _contextDB.Entry(pizza).CurrentValues.SetValues(entity);
            //_contextDB.Pizzas.Update(entity);
            _contextDB.SaveChanges();
        }
    }
}
