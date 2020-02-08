using Stock.DB.Models;
using Stock.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DB.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
        Boolean Save();
        Task<Boolean> SaveAsync();
    }
}
