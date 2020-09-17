using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.PizzaApp.DataAccess.Interfaces
{
    public interface IPizzaRepository: IRepository<Pizza>
    {
        List<Pizza> FindPizzasOnPromotion();
    }
}
