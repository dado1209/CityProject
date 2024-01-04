using CityProject.DAL.Entities;


namespace CityProject.Repository.Common
{
    public interface ICityParkRepository : IGenericRepository<CityParkEntity>
    {
        IQueryable<CityParkEntity> GetParksByCity(CityEntity city);
    }
}
