using CityProject.DAL;
using CityProject.Models;
using CityProject.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProject.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DataContext _dc = null;

        private DbSet<T> table = null;


        public GenericRepository(DataContext dc)
        {
            _dc = dc;
            table = _dc.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            table.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await table.FindAsync(id);
        }
    }
}
