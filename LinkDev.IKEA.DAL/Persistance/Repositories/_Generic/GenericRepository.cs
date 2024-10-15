using LinkDev.IKEA.DAL.Entities;
//using LinkDev.IKEA.DAL.Entities.T;
using LinkDev.IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.DAL.Persistance.Repositories._Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public IEnumerable<T> GetAll(bool withAsNoTracking = false, [FromServices] ApplicationDbContext dbContext) // when I need object from dbcontext only in this method
        public async Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
                return await _dbContext.Set<T>().Where(X => !X.IsDeleted).AsNoTracking().ToListAsync();

            return await _dbContext.Set<T>().Where(X => !X.IsDeleted).ToListAsync();
        }

        public IQueryable<T> GetIQueryable()
        {
            return _dbContext.Set<T>();
        }

        public IEnumerable<T> GetIEnumerable()
        {
            return _dbContext.Set<T>();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
            //var T = _dbContext.Ts.Local.FirstOrDefault(x => x.Id == id);

            //if (T == null)
            //    T = _dbContext.Ts.FirstOrDefault(x => x.Id == id);
            //return T;
        }

        public  void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            //return _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            //return _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
            //_dbContext.Set<T>().Remove(entity);
            //return _dbContext.SaveChanges();
        }

        
    }
}
