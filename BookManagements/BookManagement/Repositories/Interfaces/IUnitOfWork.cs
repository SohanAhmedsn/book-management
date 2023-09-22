namespace BookManagement.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GetRepo<T>() where T : class, new();
        Task<bool> SaveAsync();
    }
}
