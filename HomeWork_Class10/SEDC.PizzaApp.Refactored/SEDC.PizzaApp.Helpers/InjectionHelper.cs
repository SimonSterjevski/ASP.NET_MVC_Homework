using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.PizzaApp.DataAccess;
using SEDC.PizzaApp.DataAccess.EFImplementations;
using SEDC.PizzaApp.DataAccess.Implementations;
using SEDC.PizzaApp.DataAccess.Interfaces;
using SEDC.PizzaApp.Domain.Models;
using SEDC.PizzaApp.Services.Implementations;
using SEDC.PizzaApp.Services.Interfaces;

namespace SEDC.PizzaApp.Helpers
{
    public static class InjectionHelper
    {
        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Order>, OrderEfRepository>();
            // services.AddTransient<IRepository<Pizza>, PizzaRepository>();
            services.AddTransient<IPizzaRepository, PizzaEfRepository>();
            services.AddTransient<IRepository<User>, UserEfRepository>();
            services.AddTransient<IFeedbackRepository, FeedbackEfRepository>();
        }
        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPizzaService, PizzaService>();
            services.AddTransient<IFeedbackService, FeedbackService>();
        }
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<PizzaAppContextDB>(options =>
            {
                options.UseSqlServer("Server=.;Database=PizzaAppDB_Home;Trusted_Connection=True");
            });

        }
    }
}
