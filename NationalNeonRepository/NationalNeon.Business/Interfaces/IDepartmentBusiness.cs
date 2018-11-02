using NationalNeon.Domain.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Business.Interfaces
{
    public interface IDepartmentBusiness
    {
        DepartmentModel AddDepartment(DepartmentModel model);
        List<DepartmentModel> GetAll();

        DepartmentModel GetById(int Id);

        void Update(DepartmentModel model);

        void DeleteDepartment(int departmentId);
    }
}
