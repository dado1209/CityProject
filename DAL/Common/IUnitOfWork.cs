using ExampleProject.Repository.Common;

namespace ExampleProject.DAL.Common
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }

        ICityParkRepository CityParkRepository { get; }
        Task<bool> SaveAsync();
    }
}
