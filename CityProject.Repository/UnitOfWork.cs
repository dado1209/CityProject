using CityProject.DAL;
using CityProject.Repository.Common;

namespace CityProject.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dc;

        public UnitOfWork(DataContext dc)
        {
            _dc = dc;
        }
        // Saves all the changes that we have queued up. If one change fails then all of them fail
        public async Task<bool> SaveAsync()
        {
            return await _dc.SaveChangesAsync() > 0;
        }
    }
}
