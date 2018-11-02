using NationalNeon.Repository.Concrete;
using NationalNeon.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalNeon.Repository.DB;

namespace NationalNeon.Repository
{
    public class TaskRepository : BaseRepository<Task>
    {
        public TaskRepository(IUnitOfWork unit) : base(unit)
        { }
    }
}

