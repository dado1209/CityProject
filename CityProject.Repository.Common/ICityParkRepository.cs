using CityProject.DAL.Entities;


namespace CityProject.Repository.Common
{
    public interface ICityParkRepository : IGenericRepository<CityParkEntity>
    {
        IEnumerable<CityParkEntity> GetParksByCity(CityEntity city);
    }
}
