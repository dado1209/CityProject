using CityProject.Repository.Common;

namespace CityProject.DAL.Common
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }

        ICityParkRepository CityParkRepository { get; }
        Task<bool> SaveAsync();
    }
}
