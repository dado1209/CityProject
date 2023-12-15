using ExampleProject.Repository;
using ExampleProject.Repository.Common;
using ExampleProject.DAL.Common;

namespace ExampleProject.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dc;
        public ICityRepository CityRepository { get; }
        public ICityParkRepository CityParkRepository { get; }

        public UnitOfWork(DataContext dc, ICityRepository cityRepository, ICityParkRepository cityParkRepository)
        {
            this.dc = dc;
            CityRepository = cityRepository;
            CityParkRepository = cityParkRepository;
        }
        // Saves all the changes that we have queued up. If one change fails then all of them fail
        public async Task<bool> SaveAsync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
}
