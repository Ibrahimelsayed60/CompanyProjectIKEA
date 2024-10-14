using LinkDev.IKEA.DAL.Entities;
//using LinkDev.IKEA.DAL.Entities.T;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll(bool withAsNoTracking = true);

        IQueryable<T> GetIQueryable();

        IEnumerable<T> GetIEnumerable();

        T? Get(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
