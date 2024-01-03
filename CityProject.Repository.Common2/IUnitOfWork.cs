using CityProject.Repository.Common;

namespace CityProject.Repository.Common
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }

        ICityParkRepository CityParkRepository { get; }
        Task<bool> SaveAsync();
    }
}
