namespace ExampleProject.Repository.Common
{
    public interface IGenericRepository<T, V> where T : class where V : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(V entityDto, int id);
    }
}
