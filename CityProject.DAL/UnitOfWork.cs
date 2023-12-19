using CityProject.DAL.Common;
using CityProject.Repository.Common;

namespace CityProject.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dc;
        public ICityRepository CityRepository { get; }
        public ICityParkRepository CityParkRepository { get; }

        public UnitOfWork(DataContext dc, ICityRepository cityRepository, ICityParkRepository cityParkRepository)
        {
            _dc = dc;
            CityRepository = cityRepository;
            CityParkRepository = cityParkRepository;
        }
        // Saves all the changes that we have queued up. If one change fails then all of them fail
        public async Task<bool> SaveAsync()
        {
            return await _dc.SaveChangesAsync() > 0;
        }
    }
}
