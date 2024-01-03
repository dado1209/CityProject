using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProject.Repository.Common
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int id);
        IQueryable<T> GetAllAsync();
        Task AddAsync(T entity);
        void Delete(T entity);
    }
}
