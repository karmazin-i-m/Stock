using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DB.Repositories
{
    public interface IRepository<T>
        where T : class 
    {
        Task<IEnumerable<T>> GetListAsync();
        Task<T> GetAsync(int id);
        Task<Int32> CreateAsync(T item);

        IEnumerable<T> GetList();
        T Get(int id);
        Int32 Create(T item);
        Boolean Update(T item);
        Boolean Delete(int id);
    }
}
