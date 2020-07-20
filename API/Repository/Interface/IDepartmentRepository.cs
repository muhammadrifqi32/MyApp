using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> Get();
        //IEnumerable<Department> Get();
        Department Get(int Id);
        //Department Get(int Id);
        int Create(Department department);
        int Update(int Id, Department department);
        int Delete(int Id);
    }
}
