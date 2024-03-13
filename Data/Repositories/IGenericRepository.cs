namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        Task AddAsync(T entity);
        void Add(T entity);
        Task UpdateAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(T entity);
        void Delete(T entity);
    }
}
