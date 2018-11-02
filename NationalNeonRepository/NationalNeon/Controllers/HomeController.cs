using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressMapper;
using NationalNeon.Business.Interfaces;
using NationalNeon.Domain.Player;
using NationalNeon.Repository.DB;
using NationalNeon.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace NationalNeon.Web.Controllers
{ 
    public class HomeController : Controller
    {
        private readonly IPlayerBusiness iplayerBusiness;
        private readonly ICountryBusiness icountryBusiness;
        public HomeController(IPlayerBusiness iplayerBusiness, ICountryBusiness icountryBusiness)
        {
            this.iplayerBusiness = iplayerBusiness;
            this.icountryBusiness = icountryBusiness;
        }
        public IActionResult Index()
        {          
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
       
        public IActionResult UpdatePlayer()
        {
            return View();
        }
    }
}