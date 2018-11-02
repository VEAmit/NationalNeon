using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalNeon.Business.Interfaces;
using NationalNeon.Domain.Player;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NationalNeon.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IPlayerBusiness iplayerBusiness;
        private readonly ICountryBusiness icountryBusiness;      
        public AdminController(IPlayerBusiness iplayerBusiness, ICountryBusiness icountryBusiness)
        {
            this.iplayerBusiness = iplayerBusiness;
            this.icountryBusiness = icountryBusiness;
        }
        public IActionResult Index()
        {

            List<PlayerModel> playerList = iplayerBusiness.GetAllPlayer();
            return View(playerList);
        }
       
    }
}