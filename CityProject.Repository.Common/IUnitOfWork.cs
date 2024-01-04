using CityProject.Repository.Common;

namespace CityProject.Repository.Common
{
    public interface IUnitOfWork
    {
        Task<bool> SaveAsync();
    }
}
