using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NationalNeon.Business.Interfaces;

namespace NationalNeon.Web.Controllers
{
    public class ArchivedController : Controller
    {

        private readonly IJobBusiness ijobBusiness;

        public ArchivedController(IJobBusiness ijobBusiness)
        {
            this.ijobBusiness = ijobBusiness;
        }
        [Route("Archived")]
        public IActionResult Index()
        {
            return View();
        }
    }
}