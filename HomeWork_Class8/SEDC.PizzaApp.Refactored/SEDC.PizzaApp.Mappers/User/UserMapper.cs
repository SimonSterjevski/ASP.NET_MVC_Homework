using System;
using System.Collections.Generic;
using System.Text;
using SEDC.PizzaApp.ViewModels.User;

namespace SEDC.PizzaApp.Mappers.User
{
    public static class UserMapper
    {
        public static UserDDViewModel ToUserDdViewModel(this Domain.Models.User user)
        {
            return new UserDDViewModel
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}"
            };
        }
    }
}
