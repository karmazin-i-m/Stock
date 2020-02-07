using Microsoft.EntityFrameworkCore;
using Stock.DB.Models;
using Stock.DB.Repositories;
using Stock.DB.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Stock.DB
{
    class StockContext : DbContext
    {
        public static readonly ILoggerFactory FileLogger = new LoggerFactory(new List<FileLoggerProvider>() { new FileLoggerProvider() });

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public StockContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StockDB;Trusted_Connection=True;");
            optionsBuilder.UseLoggerFactory(FileLogger);
        }
    }
}
