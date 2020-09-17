using System;
using System.Collections.Generic;
using System.Text;
using SEDC.PizzaApp.ViewModels.User;

namespace SEDC.PizzaApp.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDDViewModel> GetUsersForDropDown();
    }
}
