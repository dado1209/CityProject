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
        private DataContext _dc;

        private DbSet<T> table;


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

        public IQueryable<T> GetAllAsync()
        {
            return table.AsQueryable();
        }

        public async Task<T> GetAsync(int id)
        {
            return await table.FindAsync(id);
        }
    }
}
