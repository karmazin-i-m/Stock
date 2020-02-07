using Microsoft.EntityFrameworkCore;
using Stock.DB.Models;
using Stock.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.DB
{
    class StockContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public StockContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            InitialDB();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StockDB;Trusted_Connection=True;");
        }

        public void InitialDB()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                IRepository<User> users = unitOfWork.Users;
                IRepository<Product> products = unitOfWork.Products;
                IRepository<Order> orders = unitOfWork.Orders;

                Product product1 = new Product() { ProductName = "Яблоко", Price = 21.3, Weight = 13.3 };
                Product product2 = new Product() { ProductName = "Арбуз", Price = 21.3, Weight = 1.4 };
                Product product3 = new Product() { ProductName = "Дыня", Price = 21.3, Weight = 12 };

                Product product4 = new Product() { ProductName = "Апельсин", Price = 21.3, Weight = 13.3 };
                Product product5 = new Product() { ProductName = "Мандарин", Price = 21.3, Weight = 13.3 };
                Product product6 = new Product() { ProductName = "Виноград", Price = 21.3, Weight = 13.3 };

                Product product7 = new Product() { ProductName = "Курица", Price = 21.3, Weight = 13.3 };
                Product product8 = new Product() { ProductName = "Свинина", Price = 21.3, Weight = 13.3 };
                Product product9 = new Product() { ProductName = "Говядина", Price = 21.3, Weight = 13.3 };

                products.Create(product1);
                products.Create(product2);
                products.Create(product3);

                products.Create(product4);
                products.Create(product5);
                products.Create(product6);

                products.Create(product7);
                products.Create(product8);
                products.Create(product9);

                Order order1 = new Order() { Date = DateTime.Now, Products = new List<Product>() { product1, product2, product3 } };
                Order order2 = new Order() { Date = DateTime.Now, Products = new List<Product>() { product4, product5, product6 } };
                Order order3 = new Order() { Date = DateTime.Now, Products = new List<Product>() { product7, product8, product9 } };

                orders.Create(order1);
                orders.Create(order2);
                orders.Create(order3);

                User user1 = new User() { Name = "Illya", Email = "illya@gmail.com", Login = "illya_ua", Password = "qwerty", Role = Role.Admin, Orders = new List<Order>() { order1, order2 } };
                User user2 = new User() { Name = "Lilia", Email = "lilia@gmail.com", Login = "lilia_ua", Password = "qwerty", Role = Role.User, Orders = new List<Order>() { order3 } };
                User user3 = new User() { Name = "Oleg", Email = "oleg@gmail.com", Login = "oleg_ua", Password = "qwerty", Role = Role.User };

                users.Create(user1);
                users.Create(user2);
                users.Create(user3);

                unitOfWork.Save();
            }
        }
    }
}
