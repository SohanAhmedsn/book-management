using BookManagement.Models;
using BookManagement.Repositories.Interfaces;

namespace BookManagement.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookManagementDbContext db;
        public UnitOfWork(BookManagementDbContext db)
        {
            this.db = db;
        }
        public IGenericRepository<T> GetRepo<T>() where T : class, new()
        {
            return new GenericRepo<T>(db);
        }

        public async Task<bool> SaveAsync()
        {
            int rowsEffected = await db.SaveChangesAsync();
            return rowsEffected > 0;
        }

   
    }
}

