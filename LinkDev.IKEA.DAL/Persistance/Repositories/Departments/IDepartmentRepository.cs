using LinkDev.IKEA.DAL.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories.Departments
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool withAsNoTracking = true);

        Department? GetById(int id);

        int Add(Department entity);

        int Update(Department entity);

        int Delete(Department entity);

    }
}
