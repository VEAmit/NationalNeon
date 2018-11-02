using Microsoft.Extensions.DependencyInjection;
using NationalNeon.Repository.Concrete;
using NationalNeon.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalNeon.Business.Interfaces;
using NationalNeon.Business.Concrete;

namespace NationalNeon.Business.Intrastructure
{
   public class Bindings
    {
        public static void AddBindings(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserBusiness, UserBusiness>();
            services.AddTransient<IDepartmentBusiness, DepartmentBusiness>();
            services.AddTransient<IJobBusiness, JobBusiness>();
            services.AddTransient<ITaskBusiness,TaskBusiness>();
            services.AddTransient<IGraphReport, GraphReport>();
        }
    }
}
