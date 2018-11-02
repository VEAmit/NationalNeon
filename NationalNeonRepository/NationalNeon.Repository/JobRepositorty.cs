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
    public class JobRepositorty : BaseRepository<Job>
    {
        public JobRepositorty(IUnitOfWork unit) : base(unit)
        { }

    }
}
