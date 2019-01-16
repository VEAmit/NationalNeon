using NationalNeon.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalNeon.Domain.Department;
using NationalNeon.Repository.Interface;
using NationalNeon.Repository;
using NationalNeon.Repository.DB;
using ExpressMapper;

namespace NationalNeon.Business.Concrete
{
    public class DepartmentBusiness : IDepartmentBusiness
    {
        private readonly DepartmentRepository departmentRepository;

        public DepartmentBusiness(IUnitOfWork unit)
        {
            departmentRepository = new DepartmentRepository(unit);
        }
        public DepartmentModel AddDepartment(DepartmentModel model)
        {
            Department user = new Department();
            Mapper.Map(model, user);
            departmentRepository.Insert(user);
            Mapper.Map(user, model);
            return model;
        }

        public List<DepartmentModel> GetAll()
        {
            var model = new List<DepartmentModel>();
            var data=departmentRepository.GetAll();
            Mapper.Map(data, model);
            return model;
        }

        public DepartmentModel GetById(int Id)
        {
            var model = new DepartmentModel();
            var data = departmentRepository.FindBy(x => x.departmentId == Id);
            Mapper.Map(data, model);
            return model;
        }

        public void Update(DepartmentModel model)
        {
            var data = departmentRepository.FindBy(x => x.departmentId == model.departmentId);
            if (data != null)
            {
                data.departmentname = model.departmentname;
                data.description = model.description;
                data.updated_on = DateTime.Now;
                data.VisibleOnDashboard = model.VisibleOnDashboard;
                departmentRepository.Update(data);
            }
        }

        public void DeleteDepartment(int Id)
        {
            departmentRepository.Delete(u => u.departmentId == Id);

        }
    }
}
