using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEDC.PizzaApp.DataAccess.Interfaces;
using SEDC.PizzaApp.Domain.Models;

namespace SEDC.PizzaApp.DataAccess.Implementations
{
    public class PizzaRepository : IPizzaRepository
    {
        public List<Pizza> GetAll()
        {
            //return domain models (all records from the table in DB)
            return StaticDb.Pizzas;
        }

        public Pizza GetById(int id)
        {
            //returns one record from a table in DB (by id)
            return StaticDb.Pizzas.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Pizza entity)
        {
           //increment the id
           StaticDb.PizzaId++;
           entity.Id = StaticDb.PizzaId;
           //send the record to the DB
           StaticDb.Pizzas.Add(entity);
           return entity.Id;
        }

        public void Update(Pizza entity)
        {
            //check if the record with the id of the updated entity exists
            Pizza pizza = StaticDb.Pizzas.FirstOrDefault(x => x.Id == entity.Id);
            if (pizza == null)
            {
                throw new Exception($"Pizza with id {entity.Id} was not found");
            }
            //update the record in DB
            int index = StaticDb.Pizzas.IndexOf(pizza);
            StaticDb.Pizzas[index] = entity;
        }

        public void DeleteById(int id)
        {
            //check if a record with the given id exists
            Pizza pizza = StaticDb.Pizzas.FirstOrDefault(x => x.Id == id);
            if (pizza == null)
            {
                throw new Exception($"Pizza with id {id} was not found");
            }
            //delete record from DB
            int index = StaticDb.Pizzas.IndexOf(pizza);
            StaticDb.Pizzas.RemoveAt(index);
        }

        public Pizza GetPizzaOnPromotion()
        {
            return StaticDb.Pizzas.FirstOrDefault(x => x.IsOnPromotion);
        }
    }
}
