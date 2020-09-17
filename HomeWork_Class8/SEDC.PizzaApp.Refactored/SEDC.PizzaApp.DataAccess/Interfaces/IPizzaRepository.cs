using System;
using System.Collections.Generic;
using System.Text;
using SEDC.PizzaApp.Domain.Models;

namespace SEDC.PizzaApp.DataAccess.Interfaces
{
    public interface IPizzaRepository : IRepository<Pizza> // has to implement the CRUD operations
    {
        //we need additional methods for manipulating pizza db entities
        Pizza GetPizzaOnPromotion();
    }
}
