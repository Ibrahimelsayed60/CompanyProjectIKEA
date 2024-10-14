using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(_dbContext);
            
        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_dbContext);

        public UnitOfWork(ApplicationDbContext dbContext) // Ask CLR fpr creationg object from class "ApplicationDbContext" Implicitly
        {
            //EmployeeRepository = new EmployeeRepository(dbContext);
            //DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }


        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
