using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistance.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories.Employees
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        
    }
}
