﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.PizzaApp.DataAccess
{
    public interface IRepository<T>
    {
        //CRUD Methods
        List<T> GetAll();
        T GetById(int id);
        int Insert(T entity);
        void UpdateEntity(T entity);
        void DeleteById(int id);
    }
}
