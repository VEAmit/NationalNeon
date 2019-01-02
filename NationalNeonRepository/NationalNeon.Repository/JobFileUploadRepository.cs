﻿using NationalNeon.Repository.Concrete;
using NationalNeon.Repository.DB;
using NationalNeon.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Repository
{
    public class JobFileUploadRepository : BaseRepository<JobFileUpload>
    {
        public JobFileUploadRepository(IUnitOfWork unit) : base(unit)
        { }
    }
}
