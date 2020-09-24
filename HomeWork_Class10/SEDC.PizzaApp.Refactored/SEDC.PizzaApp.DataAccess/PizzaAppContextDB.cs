using Microsoft.EntityFrameworkCore;
using SEDC.PizzaApp.Domain.Enums;
using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SEDC.PizzaApp.DataAccess
{
    public class PizzaAppContextDB: DbContext
    {
        public PizzaAppContextDB(DbContextOptions options): base(options)
        {

        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>()
                .HasData(new Pizza
                {
                    Id = 1,
                    Name = "Kaprichioza",
                    IsOnPromotion = true,
                },
                new Pizza
                {
                    Id = 2,
                    Name = "Pepperoni",
                    IsOnPromotion = false,
                },
                new Pizza
                {
                    Id = 3,
                    Name = "Margarita",
                    IsOnPromotion = false,
                }
                );

            modelBuilder.Entity<Feedback>()
                .HasData(new Feedback
                {
                    Id = 1,
                    Name = "John",
                    Email = "john@123.com",
                    Message = "mmmmmmmm delicious"
                },
                new Feedback
                {
                    Id = 2,
                    Name = "Johnny",
                    Email = "johnny@123.com",
                    Message = "Service was awful"
                }
                );

            modelBuilder.Entity<User>()
                .HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Tanja",
                    LastName = "Stojanovska",
                    Address = "Address1",
                },
                new User
                {
                    Id = 2,
                    FirstName = "Kristina",
                    LastName = "Spasevska",
                    Address = "Address2",
                }
                );

            modelBuilder.Entity<Order>()
                .HasData(
                new Order
                {
                    Id = 1,
                    PaymentMethod = PaymentMethod.Card,
                    Delivered = true,
                    PizzaStore = "Store1",
                    UserId = 1
                },
                new Order
                {
                    Id = 2,
                    PaymentMethod = PaymentMethod.Cash,
                    Delivered = false,
                    PizzaStore = "Store2",
                    UserId = 2
                }
                );
            modelBuilder.Entity<PizzaOrder>()
                .HasData(new PizzaOrder
                {
                    Id = 1,
                    OrderId = 1,
                    PizzaId = 1,
                    Price = 300,
                    PizzaSize = PizzaSize.Normal
                },
                    new PizzaOrder
                    {
                        Id = 2,
                        OrderId = 1,
                        PizzaId = 2,
                        Price = 350,
                        PizzaSize = PizzaSize.Normal
                    },
                    new PizzaOrder
                    {
                        Id = 3,
                        OrderId = 2,
                        PizzaId = 3,
                        Price = 400,
                        PizzaSize = PizzaSize.Family
                    });

            modelBuilder.Entity<Pizza>()
                .HasMany(x => x.PizzaOrders)
                .WithOne(x => x.Pizza)
                .HasForeignKey(x => x.PizzaId);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.PizzaOrders)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
      
        }

    }
    }

