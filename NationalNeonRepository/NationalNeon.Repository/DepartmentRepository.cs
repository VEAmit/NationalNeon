using NationalNeon.Repository.Concrete;
using NationalNeon.Repository.DB;
using NationalNeon.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Repository
{
    public class DepartmentRepository : BaseRepository<Department>
    {
        public DepartmentRepository(IUnitOfWork unit) : base(unit)
        { }
    }
}
