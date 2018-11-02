using NationalNeon.Repository.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Repository.Interface
{
   public interface IUnitOfWork
    {
        ApplicationDbContext DB { get; }
    }
}
